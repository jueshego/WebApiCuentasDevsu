using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Aplicacion.Exceptions;
using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Servicios
{
    public class MovimientoServicio : IServicioListar<DTOMovimientoListado>,
        IServicioCrear<DTOGuardarMovimiento>,
        IServicioCrearMovimiento
    {
        private readonly IRepositorio<Movimiento> _repoMovimiento;
        private readonly IRepositorio<Cuenta> _repoCuenta;
        private readonly IRepositorioRetiroDiarioPersona _repoRetirosDia;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        private const string DEPOSITO = "Credito";
        private const string RETIRO = "Debito";

        private const string LIMITE_DIARIO = "LimiteRetiroDiario";

        public MovimientoServicio(IRepositorio<Movimiento> repoMovimiento, IRepositorioRetiroDiarioPersona repoRetirosDia,
            IRepositorio<Cuenta> repoCuenta, IConfiguration config, IUnitOfWork unitOfWork, ILogger logger)
        {
            _config = config;
            _repoMovimiento = repoMovimiento;
            _repoCuenta = repoCuenta;
            _repoRetirosDia = repoRetirosDia;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DTOGuardarMovimiento> Insertar(DTOGuardarMovimiento dtoMovimiento)
        {
            ValidarDto(dtoMovimiento);

            var cuenta = ObtenerCuenta(dtoMovimiento).Result;

            if (dtoMovimiento.Valor < 0)
            {
                _logger.LogDebug("MovimientoServicio.Insertar -> Validaciones para operacion debito");
                ValidarSaldoExistente(cuenta.SaldoInicial);
                ValidarSaldoSuficiente(dtoMovimiento, cuenta.SaldoInicial);
                var totalRetirosDia = ValidarLimiteDiario(dtoMovimiento, cuenta);

                _logger.LogDebug("MovimientoServicio.Insertar -> Pasa Validaciones, ActualizarRetiroDiario");
                ActualizarRetiroDiario(totalRetirosDia, dtoMovimiento, cuenta);
            }

            InsertarMovimiento(dtoMovimiento, cuenta);

            ActualizarSaldoCuenta(cuenta, dtoMovimiento.Valor);

            _logger.LogDebug("MovimientoServicio.Insertar -> Guardar cambios");
            await GuardarTodosLosCambios();

            return dtoMovimiento;
        }

        public void ActualizarRetiroDiario(TotalRetiroDiarioPersona totalRetirosDia, DTOGuardarMovimiento dtoMovimiento, Cuenta cuenta)
        {
            if (totalRetirosDia == null)
            {
                _logger.LogDebug("MovimientoServicio.ActualizarRetiroDiario -> totalRetirosDia null, se inserta");
                _repoRetirosDia.InsertarRetiroDiario(new TotalRetiroDiarioPersona { PersonaId = cuenta.PersonaId, Total = Math.Abs(dtoMovimiento.Valor) });
            }
            else
            {
                totalRetirosDia.Total += Math.Abs(dtoMovimiento.Valor);

                _logger.LogDebug("MovimientoServicio.ActualizarRetiroDiario -> totalRetirosDia existe, se actualiza");
                _repoRetirosDia.ActualizarRetiroDiario(totalRetirosDia);
            }
        }

        public void ValidarDto(DTOGuardarMovimiento dtoMovimiento)
        {
            if (dtoMovimiento == null)
            {
                _logger.LogWarning("MovimientoServicio.ValidarDto -> dtoMovimiento no llega, se genera excepcion");
                throw new ArgumentNullException("El Movimiento es requerido.");
            }
        }

        public async Task<Cuenta> ObtenerCuenta(DTOGuardarMovimiento dtoMovimiento)
        {
            return await _repoCuenta.ListarPorId(dtoMovimiento.CuentaId);
        }

        public void ValidarSaldoSuficiente(DTOGuardarMovimiento dtoMovimiento, decimal saldoInicial)
        {
            if (Math.Abs(dtoMovimiento.Valor) > saldoInicial)
            {
                _logger.LogWarning($"MovimientoServicio.ValidarSaldoSuficiente -> dtoMovimiento.Valor: { dtoMovimiento.Valor } - saldoInicial: { saldoInicial } - se genera excepcion");
                throw new BusinessException("Saldo insuficiente.");
            }
        }

        public TotalRetiroDiarioPersona ValidarLimiteDiario(DTOGuardarMovimiento dtoMovimiento, Cuenta cuenta)
        {
            decimal limiteDiario = Convert.ToDecimal(_config.GetSection(LIMITE_DIARIO).Value);

            var totalRetiroDia = _repoRetirosDia.ObtenerRetiroDiario(cuenta.PersonaId, DateTime.Now).Result;

            if (totalRetiroDia != null)
            {
                if (totalRetiroDia.Total + Math.Abs(dtoMovimiento.Valor) >= limiteDiario)
                {
                    _logger.LogWarning($"MovimientoServicio.ValidarLimiteDiario -> totalRetiroDia.Total: { totalRetiroDia.Total } - salddtoMovimiento.ValoroInicial: { dtoMovimiento.Valor } - se genera excepcion");
                    throw new BusinessException("Cupo diario excedido.");
                }
            }
            
            return totalRetiroDia;
        }

        public void ValidarSaldoExistente(decimal saldoInicial)
        {
            if (saldoInicial == 0)
            {
                throw new BusinessException("Saldo no disponible.");
            }
        }

        public void InsertarMovimiento(DTOGuardarMovimiento dtoMovimiento, Cuenta cuenta)
        {
            Movimiento movimiento = new Movimiento
            {
                Tipo = dtoMovimiento.Valor < 0 ? RETIRO : DEPOSITO,
                Valor = dtoMovimiento.Valor,
                Saldo = cuenta.SaldoInicial + dtoMovimiento.Valor,
                CuentaId = cuenta.CuentaId
            };

            _repoMovimiento.Insertar(movimiento);
        }

        public void ActualizarSaldoCuenta(Cuenta cuenta, decimal valor)
        {
            cuenta.SaldoInicial += valor;

            _repoCuenta.Editar(cuenta);
        }

        public async Task<IList<DTOMovimientoListado>> ListarTodo()
        {
            var res = await _repoMovimiento.ListarTodo();
            var movimientos = res.ToList().Select(c => new DTOMovimientoListado
            {
                Cuenta = c.Cuenta.Numero,
                TipoCuenta = c.Cuenta.Tipo,
                SaldoInicial = c.Saldo,
                EstadoCuenta = c.Cuenta.Estado,
                Movimiento = string.Format(c.Valor < 0 ? "Retiro de {0}" : "Deposito de {0}", Math.Abs(c.Valor))
            }).ToList();

            return movimientos;
        }

        public async Task<int> GuardarTodosLosCambios()
        {
            return await _unitOfWork.GuardarCambios();
        }
    }
}
