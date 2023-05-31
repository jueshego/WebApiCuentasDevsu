using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Contratos
{
    public interface IServicioReportes
    {
        Task<IEnumerable<DTOMovimientoReporte>> MovimientosPorUsuarioFechas(string identificacion, DateTime fechaIni, DateTime fechaFin);
    }
}
