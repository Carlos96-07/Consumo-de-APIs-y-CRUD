using System.ComponentModel.DataAnnotations;

namespace FrontendProductosFacturacion.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
    }
}
