using System;
using System.Collections.Generic;

#nullable disable

namespace Devsu.Cuentas.Dominio.Modelos
{
    public partial class Movimiento
    {
        public Movimiento()
        {
            MovimientoId = Guid.NewGuid();
            Fecha = DateTime.Now;
        }

        public Guid MovimientoId { get; private set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public Guid CuentaId { get; set; }

        public virtual Cuenta Cuenta { get; set; }
    }
}
