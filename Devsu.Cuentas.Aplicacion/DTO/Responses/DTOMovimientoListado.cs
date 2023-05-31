using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Responses
{
    public class DTOMovimientoListado
    {
        public string Cuenta { get; set; }

        public string TipoCuenta { get; set; }

        public decimal SaldoInicial { get; set; }

        public bool EstadoCuenta { get; set; }

        public string Movimiento { get; set; }
    }
}
