using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Devsu.Cuentas.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IServicioListar<DTOClienteListado> _servicioListar;

        private readonly IServicioCrear<DTOGuardarCliente> _servicioCrear;

        private readonly IServicioEditar<DTOGuardarCliente> _servicioEditar;

        private readonly IServicioBorrar _servicioBorrar;

        public ClientesController(IServicioListar<DTOClienteListado> servicioListar,
            IServicioCrear<DTOGuardarCliente> servicioCrear,
            IServicioEditar<DTOGuardarCliente> servicioEditar,
            IServicioBorrar servicioBorrar)
        {
            _servicioListar = servicioListar;
            _servicioCrear = servicioCrear;
            _servicioEditar = servicioEditar;
            _servicioBorrar = servicioBorrar;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOClienteListado>>> Get()
        {
            return Ok(await _servicioListar.ListarTodo());
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<ActionResult<DTOGuardarCliente>> Post([FromBody] DTOGuardarCliente dtoCliente)
        {
            var cliente = await _servicioCrear.Insertar(dtoCliente);
            return Created($"~api/clientes/{cliente.Identificacion}", cliente);
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<DTOGuardarCliente>> Put(Guid id, [FromBody] DTOGuardarCliente dtoCliente)
        {
            return Ok(await _servicioEditar.Editar(dtoCliente, id));
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servicioBorrar.Eliminar(id);
            return new EmptyResult(); 
        }
    }
}
