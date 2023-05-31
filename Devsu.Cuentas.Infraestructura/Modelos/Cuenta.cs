using System;
using System.Collections.Generic;

#nullable disable

namespace Devsu.Cuentas.Dominio.Modelos
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
            CuentaId = Guid.NewGuid();
            Estado = true;
        }

        public Guid CuentaId { get; private set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public bool Estado { get; set; }
        public decimal SaldoInicial { get; set; }
        public Guid PersonaId { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
