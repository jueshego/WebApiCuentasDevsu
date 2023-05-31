using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devsu.Cuentas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IServicioListar<DTOMovimientoListado> _servicioListar;

        private readonly IServicioCrear<DTOGuardarMovimiento> _servicioCrear;

        public MovimientosController(IServicioListar<DTOMovimientoListado> servicioListar,
            IServicioCrear<DTOGuardarMovimiento> servicioCrear)
        {
            _servicioListar = servicioListar;
            _servicioCrear = servicioCrear;
        }

        // GET: api/<MovimientoController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOMovimientoListado>>> Get()
        {
            return Ok(await _servicioListar.ListarTodo());
        }

        // POST api/<MovimientoController>
        [HttpPost]
        public async Task<ActionResult<DTOGuardarMovimiento>> Post([FromBody] DTOGuardarMovimiento dtoMovimiento)
        {
            var movimiento = await _servicioCrear.Insertar(dtoMovimiento);
            return Created($"~api/movimientos/{movimiento.CuentaId}", movimiento);
        }
    }
}
