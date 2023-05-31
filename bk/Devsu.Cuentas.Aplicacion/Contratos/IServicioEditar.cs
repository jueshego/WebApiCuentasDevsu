using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Contratos
{
    public interface IServicioEditar<T> where T : class
    {
        Task<T> Editar(T entidad, Guid id);
    }
}
