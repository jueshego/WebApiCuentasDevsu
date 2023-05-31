using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Responses
{
    public class DTOCuentaListado
    {
        public string Numero { get; set; }

        public string Tipo { get; set; }

        public decimal SaldoInicial { get; set; }

        public bool Estado { get; set; }

        public string Cliente { get; set; }
    }
}
