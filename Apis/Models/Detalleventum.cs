using System;
using System.Collections.Generic;

namespace Apis.Models;

public partial class Detalleventum
{
    public int IdDetalle { get; set; }

    public int IdProducto { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public bool Impuesto { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
