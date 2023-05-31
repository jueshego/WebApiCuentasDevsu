using System;
using System.Collections.Generic;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Responses
{
    public class GenericResponse<T> where T : class
    {
        public GenericResponse()
        {

        }

        public GenericResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public GenericResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }
    }
}
