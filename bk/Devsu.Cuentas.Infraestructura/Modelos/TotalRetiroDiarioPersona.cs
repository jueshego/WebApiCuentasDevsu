using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Dominio.Modelos
{
    public class TotalRetiroDiarioPersona
    {
        public TotalRetiroDiarioPersona()
        {
            Id = new Guid();
            Fecha = DateTime.Now;
        }

        public Guid Id { get; set; }

        public Guid PersonaId { get; set; }

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }
    }
}
