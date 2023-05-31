using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Devsu.Cuentas.Aplicacion.DTO.Requests
{
    public class DTOGuardarCliente
    {
        [Required(ErrorMessage = "La identificacion es requerida.")]
        [StringLength(20)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido.")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Genero es requerido.")]
        [StringLength(1)]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La Edad es requerida.")]
        [Range(18, 120, ErrorMessage = "La Edad minima debe ser de 18 años.")]
        public byte Edad { get; set; }

        [Required(ErrorMessage = "La Direccion es requerida.")]
        [StringLength(100)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El Telefono es requerido.")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida.")]
        [StringLength(12, MinimumLength = 8, 
            ErrorMessage = "La contraseña debe tener entre 8 y 12 caracteres")]
        public string Contrasena { get; set; }

        public bool Estado { get; set; }
    }
}
