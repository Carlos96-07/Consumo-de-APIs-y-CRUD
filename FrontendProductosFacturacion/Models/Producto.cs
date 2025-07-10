using System.ComponentModel.DataAnnotations;

namespace FrontendProductosFacturacion.Models
{
    public class Producto
    {
        
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El código es obligatorio.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no debe superar los 50 caracteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public decimal PrecioUnitario { get; set; }
        [StringLength(200, ErrorMessage = "La descripción no debe superar los 200 caracteres.")]
        public string Descripcion { get; set; }
    }
}
