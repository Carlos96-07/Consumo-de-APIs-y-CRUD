using System;
using System.Collections.Generic;

namespace Apis.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Detalleventum> Detalleventa { get; set; } = new List<Detalleventum>();
}
