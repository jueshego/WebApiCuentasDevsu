using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Devsu.Cuentas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IServicioReportes _servicioreportes;

        public ReportesController(IServicioReportes servicioreportes) => _servicioreportes = servicioreportes;

        [HttpGet]
        [Route("Cliente/Identificacion/{identificacion}/Movimientos/desde/{fechaIni}/hasta/{fechaFin}")]
        public async Task<ActionResult<DTOMovimientoReporte>> Get(string identificacion, DateTime fechaIni, DateTime fechaFin)
        {
            return Ok(await _servicioreportes.MovimientosPorUsuarioFechas(identificacion, fechaIni, fechaFin));
        }
    }
}
