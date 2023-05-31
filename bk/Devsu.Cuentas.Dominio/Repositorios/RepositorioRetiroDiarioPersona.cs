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

        public void ActualizarRetiroDiario(TotalRetiroDiarioPersona retiroDiaGuardar)
        {
            var retiroDia = ObtenerRetiroDiario(retiroDiaGuardar.PersonaId, retiroDiaGuardar.Fecha).Result;

            if(retiroDia == null)
            {
                _contexto.Set<TotalRetiroDiarioPersona>().AddAsync(retiroDiaGuardar);
            }
            else
            {
                var retiroDiaEditar = _contexto.Set<TotalRetiroDiarioPersona>().FindAsync(retiroDia.Id).Result;

                retiroDiaEditar.Total += retiroDiaGuardar.Total;

                _contexto.Entry(retiroDiaEditar).State = EntityState.Modified;
            }
        }

        public Task<TotalRetiroDiarioPersona> ObtenerRetiroDiario(Guid personaId, DateTime Fecha)
        {
            return _contexto.Set<TotalRetiroDiarioPersona>().FirstOrDefaultAsync(r => r.PersonaId.Equals(personaId) 
                && r.Fecha.Date.Equals(Fecha.Date));
        }
    }
}
