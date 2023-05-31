using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Contratos
{
    public interface IServicioBorrar
    {
        Task Eliminar(Guid id);
    }
}
