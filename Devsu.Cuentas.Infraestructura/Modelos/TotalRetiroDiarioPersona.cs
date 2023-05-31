using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Dominio.Modelos
{
    public class TotalRetiroDiarioPersona
    {
        public TotalRetiroDiarioPersona()
        {
            Id = Guid.NewGuid();
            Fecha = DateTime.Now;
        }

        public Guid Id { get; private set; }

        public Guid PersonaId { get; set; }

        public DateTime Fecha { get; private set; }

        public decimal Total { get; set; }

        public virtual Persona Persona { get; set; }
    }
}
