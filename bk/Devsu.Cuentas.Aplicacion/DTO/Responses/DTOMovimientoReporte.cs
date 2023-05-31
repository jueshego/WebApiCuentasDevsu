using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Responses
{
    public class DTOMovimientoReporte
    {
        public DateTime Fecha { get; set; }

        public string Cliente { get; set; }

        public string Cuenta { get; set; }

        public string TipoCuenta { get; set; }

        public decimal SaldoInicial { get; set; }

        public bool EstadoCuenta { get; set; }

        public decimal Movimiento { get; set; }

        public decimal SaldoDisponible { get; set; }
    }
}
