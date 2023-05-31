using Devsu.Cuentas.Aplicacion.Contratos;
using Devsu.Cuentas.Aplicacion.DTO.Requests;
using Devsu.Cuentas.Aplicacion.DTO.Responses;
using Devsu.Cuentas.Dominio.Contratos;
using Devsu.Cuentas.Dominio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Servicios
{
    public class ClienteServicio : IServicioListar<DTOClienteListado>, 
        IServicioCrear<DTOGuardarCliente>,  
        IServicioEditar<DTOGuardarCliente>,
        IServicioBorrar
    {
        private readonly IRepositorio<Cliente> _repositorio;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteServicio(IRepositorio<Cliente> repositorio, IUnitOfWork unitOfWork)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<DTOClienteListado>> ListarTodo()
        {
            var res = await _repositorio.ListarTodo();
            var clientes = res.ToList().Select(c => new DTOClienteListado
            {
                ClienteId = c.PersonaId,
                Nombre = c.Nombre,
                Direccion = c.Direccion,
                Telefono = c.Telefono,
                Contrasena = c.Contrasena,
                Estado = (bool)c.Estado
            }).ToList();

            return clientes;
        }

        public async Task<DTOGuardarCliente> Insertar(DTOGuardarCliente dtoCliente)
        {
            if (dtoCliente == null)
            {
                throw new ArgumentNullException("El cliente es requerido.");
            }

            Cliente cliente = new Cliente
            {
                Identificacion = dtoCliente.Identificacion,
                Nombre = dtoCliente.Nombre,
                Genero = dtoCliente.Genero,
                Edad = dtoCliente.Edad,
                Direccion = dtoCliente.Direccion,
                Telefono = dtoCliente.Telefono,
                Contrasena = dtoCliente.Contrasena
            };

            _repositorio.Insertar(cliente);
            await _unitOfWork.GuardarCambios();
            return dtoCliente;
        }

        public async Task<DTOGuardarCliente> Editar(DTOGuardarCliente dtoCliente, Guid id)
        {
            ValidarId(id);

            ValidarDto(dtoCliente);

            Cliente cliente = await _repositorio.ListarPorId(id);

            ValidarClienteEditar(cliente);

            cliente.Identificacion = dtoCliente.Identificacion;
            cliente.Nombre = dtoCliente.Nombre;
            cliente.Genero = dtoCliente.Genero;
            cliente.Edad = dtoCliente.Edad;
            cliente.Direccion = dtoCliente.Direccion;
            cliente.Telefono = dtoCliente.Telefono;
            cliente.Contrasena = dtoCliente.Contrasena;
            cliente.Estado = dtoCliente.Estado;

            _repositorio.Editar(cliente);
            await _unitOfWork.GuardarCambios();
            return dtoCliente;
        }

        public async Task Eliminar(Guid id)
        {
            await _repositorio.Eliminar(id);
            await _unitOfWork.GuardarCambios();
        }

        public void ValidarDto(DTOGuardarCliente dtoCliente)
        {
            if (dtoCliente == null)
            {
                throw new ArgumentNullException("El cliente es requerido.");
            }
        }

        public void ValidarId(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("El Id del Cliente es requerido.");
            }
        }

        public void ValidarClienteEditar(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new KeyNotFoundException("El cliente no existe.");
            }
        }
    }
}
