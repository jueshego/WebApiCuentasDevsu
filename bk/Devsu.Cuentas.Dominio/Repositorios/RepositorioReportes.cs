using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using Devsu.Cuentas.Infraestructura.Datos.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Infraestructura.Repositorios
{
    public class RepositorioReportes : IRepositorioReportes
    {
        private DbContext _contexto;

        private bool disposed = false;

        public RepositorioReportes(DbContext contexto) => _contexto = contexto;

        public async Task<IEnumerable<Movimiento>> MovimientosPorUsuarioFechas(string identificacion, DateTime fechaIni, DateTime fechaFin)
        {
            //var data = (from m in _contexto.Set<Movimiento>()
            //            where m.Cuenta.Persona.Identificacion == identificacion && 
            //                m.Fecha.Date >= fechaIni.Date && m.Fecha.Date <= fechaFin.Date
            //            select m).ToListAsync();

            var data = _contexto.Set<Movimiento>()
                            .Where(m => m.Cuenta.Persona.Identificacion.Equals(identificacion))
                            .Where(m => m.Fecha.Date >= fechaIni.Date && m.Fecha.Date <= fechaFin.Date)
                            .ToListAsync();

            return await data;
        }
    }
}
