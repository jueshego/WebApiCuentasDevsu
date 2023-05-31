using Devsu.Cuentas.Aplicacion.Contratos;
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
    public class ReporteServicio : IServicioReportes
    {
        private readonly IRepositorioReportes _repoReportes;

        public ReporteServicio(IRepositorioReportes repoReportes) => _repoReportes = repoReportes;

        public async Task<IEnumerable<DTOMovimientoReporte>> MovimientosPorUsuarioFechas(string identificacion, DateTime fechaIni, DateTime fechaFin)
        {
            var data = await _repoReportes.MovimientosPorUsuarioFechas(identificacion, fechaIni, fechaFin);

            var movimientos = data.Select(m => new DTOMovimientoReporte
            {
                Fecha = m.Fecha,
                Cliente = m.Cuenta.Persona.Nombre,
                Cuenta = m.Cuenta.Numero,
                TipoCuenta = m.Cuenta.Tipo,
                SaldoInicial = m.Saldo + m.Valor,
                EstadoCuenta = m.Cuenta.Estado,
                Movimiento = m.Valor,
                SaldoDisponible = m.Saldo
            });

            return movimientos;
        }
    }
}
