using FrontendProductosFacturacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FrontendProductosFacturacion.Controllers
{
    public class DetalleController : Controller
    {
        private readonly HttpClient _httpClient;

        public DetalleController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Detalleventums");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los detalles de venta.");
                return View(new List<DetalleVenta>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var detalles = JsonSerializer.Deserialize<List<DetalleVenta>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return View(detalles);
        }
        [HttpGet]
        public async Task<IActionResult> CrearDetalle()
        {
            var response = await _httpClient.GetAsync("api/Productoes"); 
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error al cargar los productos.");
                return View(new List<Producto>()); // Asegúrate de tener un modelo Producto
            }

            var jsonProductos = await response.Content.ReadAsStringAsync();
            var productos = JsonSerializer.Deserialize<List<Producto>>(jsonProductos, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(productos);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarDetalles(List<DetalleVenta> detalles)
        {
            if (detalles == null || !detalles.Any())
            {
                return Json(new { success = false, message = "No hay detalles para guardar." });
            }

            var json = JsonSerializer.Serialize(detalles);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Detalleventums/Lista", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Detalles de venta guardados correctamente." });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // Log del error para depuración
                Console.WriteLine($"Error al guardar detalles en la API: {response.StatusCode} - {errorContent}");
                return Json(new { success = false, message = $"Error al guardar los detalles: {response.StatusCode}" });
            }
        }

    }
}
