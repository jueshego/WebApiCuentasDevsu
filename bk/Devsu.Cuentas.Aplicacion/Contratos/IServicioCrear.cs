using Devsu.Cuentas.Dominio.Contratos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Cuentas.Aplicacion.Contratos
{
    public interface IServicioCrear<T> where T : class
    {
        Task<T> Insertar(T entidad); 
    }
}
