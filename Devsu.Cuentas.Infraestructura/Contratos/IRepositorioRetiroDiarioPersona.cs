using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Dominio.Contratos
{
    public interface IRepositorioRetiroDiarioPersona
    {
        Task<TotalRetiroDiarioPersona> ObtenerRetiroDiario(Guid personaId, DateTime Fecha);

        void InsertarRetiroDiario(TotalRetiroDiarioPersona retiroDiaGuardar);

        void ActualizarRetiroDiario(TotalRetiroDiarioPersona retiroDiaGuardar);
    }
}
