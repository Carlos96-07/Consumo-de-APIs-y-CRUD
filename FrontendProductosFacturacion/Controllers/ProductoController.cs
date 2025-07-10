using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using FrontendProductosFacturacion.Models;

namespace FrontendProductosFacturacion.Controllers
{
    public class ProductoController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Productoes");
            var json = await response.Content.ReadAsStringAsync();
            var productos = JsonSerializer.Deserialize<List<Producto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(productos);
        }

        // CREAR - GET
        public IActionResult Create() => View();

        // CREAR - POST
        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Productoes", content);
            return RedirectToAction("Index");
        }

        // EDITAR - GET
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
                return NotFound();

            var response = await _httpClient.GetAsync($"api/Productoes/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();

            var producto = JsonSerializer.Deserialize<Producto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (producto == null)
                return NotFound();

            return View(producto);
        }


        // EDITAR - POST
        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            // Verifica si el producto tiene un ID válido antes de enviarlo
            if (producto.IdProducto == 0)
            {
                ModelState.AddModelError("", "Producto no encontrado");
                return View(producto);
            }

            // Serializa el producto a JSON
            var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");

            // Envia la solicitud PUT a la API para actualizar el producto
            var response = await _httpClient.PutAsync($"api/Productoes/{producto.IdProducto}", content);

            // Si la respuesta no es exitosa, muestra un error
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error al actualizar el producto");
                return View(producto);
            }

            // Redirige al listado de productos
            return RedirectToAction("Index");
        }



        // ELIMINAR - GET (Confirmación)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/Productoes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<Producto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(producto);
        }

        // ELIMINAR - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"api/Productoes/{id}");
            return RedirectToAction("Index");
        }

        // DETALLES (opcional)
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/producto/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var producto = JsonSerializer.Deserialize<Producto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(producto);
        }
    }
}
