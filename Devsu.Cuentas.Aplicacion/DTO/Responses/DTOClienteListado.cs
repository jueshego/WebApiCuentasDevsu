using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Responses
{
    public class DTOClienteListado
    {
        public Guid ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }
    }
}
