using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Dominio.Contratos
{
    public interface IUnitOfWork
    {
        Task<int> GuardarCambios();
    }
}
