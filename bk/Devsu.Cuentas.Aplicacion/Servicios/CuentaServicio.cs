using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Servicios
{
    public class CuentaServicio : IServicioListar<DTOCuentaListado>,
        IServicioCrear<DTOGuardarCuenta>,
        IServicioEditar<DTOGuardarCuenta>,
        IServicioBorrar
    {

        private readonly IRepositorio<Cuenta> _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public CuentaServicio(IRepositorio<Cuenta> repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DTOCuentaListado>> ListarTodo()
        {
            var res = await _repositorio.ListarTodo();
            var cuentas = res.ToList().Select(c => new DTOCuentaListado
            {
                Numero = c.Numero,
                Tipo = c.Tipo,
                SaldoInicial = c.SaldoInicial,
                Estado = c.Estado,
                Cliente = c.Persona.Nombre
            }).ToList();

            return cuentas;
        }

        public async Task<DTOGuardarCuenta> Insertar(DTOGuardarCuenta dtoCuenta)
        {
            ValidarDto(dtoCuenta);

            Cuenta cuenta = new Cuenta
            {
                Numero = dtoCuenta.Numero,
                Tipo = dtoCuenta.Tipo,
                SaldoInicial = dtoCuenta.SaldoInicial,
                PersonaId = dtoCuenta.PersonaId
            };

            _repositorio.Insertar(cuenta);
            await _unitOfWork.GuardarCambios();
            return dtoCuenta;
        }

        public async Task<DTOGuardarCuenta> Editar(DTOGuardarCuenta dtoCuenta, Guid id)
        {
            ValidarId(id);

            ValidarDto(dtoCuenta);

            Cuenta cuenta = await _repositorio.ListarPorId(id);

            ValidarCuentaEditar(cuenta);

            cuenta.Numero = dtoCuenta.Numero;
            cuenta.Tipo = dtoCuenta.Tipo;
            cuenta.Estado = dtoCuenta.Estado;
            cuenta.SaldoInicial = dtoCuenta.SaldoInicial;

            _repositorio.Editar(cuenta);
            await _unitOfWork.GuardarCambios();
            return dtoCuenta;
        }

        public async Task Eliminar(Guid id)
        {
            await _repositorio.Eliminar(id);
            await _unitOfWork.GuardarCambios();
        }

        public void ValidarDto(DTOGuardarCuenta dtoCuenta)
        {
            if (dtoCuenta == null)
            {
                throw new ArgumentNullException("La Cuenta es requerida.");
            }
        }

        public void ValidarId(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("El Id de la Cuenta es requerida.");
            }
        }

        public void ValidarCuentaEditar(Cuenta cuenta)
        {
            if (cuenta == null)
            {
                throw new KeyNotFoundException("La cuenta no existe.");
            }
        }
    }
}
