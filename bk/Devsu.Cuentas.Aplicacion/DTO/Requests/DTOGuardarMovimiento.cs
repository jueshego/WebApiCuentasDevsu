using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Requests
{
    public class DTOGuardarMovimiento
    {
        public decimal Valor { get; set; }
        public Guid CuentaId { get; set; }
    }
}
