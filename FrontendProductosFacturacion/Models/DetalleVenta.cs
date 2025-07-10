namespace FrontendProductosFacturacion.Models
{
    public class DetalleVenta
    {
        public int IdDetalle { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}
