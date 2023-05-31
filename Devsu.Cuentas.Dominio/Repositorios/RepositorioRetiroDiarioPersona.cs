using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Infraestructura.Repositorios
{
    public class RepositorioRetiroDiarioPersona : IRepositorioRetiroDiarioPersona
    {
        private DbContext _contexto;

        public RepositorioRetiroDiarioPersona(DbContext contexto) => _contexto = contexto;

        public Task<TotalRetiroDiarioPersona> ObtenerRetiroDiario(Guid personaId, DateTime Fecha)
        {
            return _contexto.Set<TotalRetiroDiarioPersona>().FirstOrDefaultAsync(r => r.PersonaId.Equals(personaId)
                && r.Fecha.Date.Equals(Fecha.Date));
        }

        public void InsertarRetiroDiario(TotalRetiroDiarioPersona retiroDiaGuardar)
        {
            _contexto.Set<TotalRetiroDiarioPersona>().AddAsync(retiroDiaGuardar);
        }

        public void ActualizarRetiroDiario(TotalRetiroDiarioPersona retirosDia)
        {
            _contexto.Entry(retirosDia).State = EntityState.Modified;
        }
    }
}
