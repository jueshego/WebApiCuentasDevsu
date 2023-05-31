using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Contratos
{
    public interface IServicioCrearMovimiento
    {
        public void ValidarDto(DTOGuardarMovimiento dtoMovimiento);

        public Task<Cuenta> ObtenerCuenta(DTOGuardarMovimiento dtoMovimiento);

        public void ValidarSaldoExistente(decimal saldoInicial);

        public void ValidarLimiteDiario(DTOGuardarMovimiento dtoMovimiento, Cuenta cuenta);

        public void ValidarSaldoSuficiente(DTOGuardarMovimiento dtoMovimiento, decimal saldoInicial);

        public void ActualizarRetiroDiario(TotalRetiroDiarioPersona totalRetirosDia);

        public void InsertarMovimiento(DTOGuardarMovimiento dtoMovimiento, Cuenta cuenta);

        public void ActualizarSaldoCuenta(Cuenta cuenta, decimal valor);

        public Task<int> GuardarTodosLosCambios();
    }
}
