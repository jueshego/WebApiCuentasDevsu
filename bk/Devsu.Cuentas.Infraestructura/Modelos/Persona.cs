using System;
using System.Collections.Generic;

#nullable disable

namespace Devsu.Cuentas.Dominio.Modelos
{
    public partial class Persona
    {
        public Persona()
        {
            Cuenta = new HashSet<Cuenta>();
            PersonaId = new Guid();
            Discriminator = "Cliente";
        }

        public Guid PersonaId { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public byte Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Discriminator { get; private set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
