using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Dominio.Modelos
{
    public class Cliente : Persona
    {
        public Cliente()
        {
            Estado = true;
        }

        public bool? Estado { get; set; }
        public string Contrasena { get; set; }
    }
}
