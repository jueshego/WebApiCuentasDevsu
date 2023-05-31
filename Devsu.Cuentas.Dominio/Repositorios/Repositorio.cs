using Devsu.Cuentas.Dominio.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Infraestructura.Datos.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private DbContext _contexto;

        public Repositorio(DbContext contexto) => _contexto = contexto;

        public async Task<T> ListarPorId(Guid id) =>
            await _contexto.Set<T>().FindAsync(id);


        public async Task<IList<T>> ListarTodo() =>
            await _contexto.Set<T>().ToListAsync();

        public void Insertar(T entidad) =>
            _contexto.Set<T>().Add(entidad);


        public void Editar(T entidad) =>
            _contexto.Entry(entidad).State = EntityState.Modified;

        public async Task Eliminar(Guid id)
        {
            T entidad = await _contexto.Set<T>().FindAsync(id);
            _contexto.Set<T>().Remove(entidad);
        }
    }
}
