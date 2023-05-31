using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Dominio.Contratos
{
    public interface IRepositorio<T> where T : class
    {
        Task<IList<T>> ListarTodo();

        Task<T> ListarPorId(Guid id);

        void Insertar(T entidad);

        void Editar(T entidad);

        Task Eliminar(Guid id);
    }
}
