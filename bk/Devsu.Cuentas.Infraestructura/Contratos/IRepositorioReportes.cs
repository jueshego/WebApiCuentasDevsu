using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Dominio.Contratos
{
    public interface IRepositorioReportes
    {
        Task<IEnumerable<Movimiento>> MovimientosPorUsuarioFechas(string identificacion, DateTime fechaIni, DateTime fechaFin);
    }
}
