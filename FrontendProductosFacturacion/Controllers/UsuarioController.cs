using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using FrontendProductosFacturacion.Models;


namespace FrontendProductosFacturacion.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly HttpClient _httpClient;

        public UsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }


        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Usuarios/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var userJson = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<Usuario>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Puedes guardar la sesión o cookie
                HttpContext.Session.SetString("Usuario", usuario.Email);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ViewBag.Error = "Credenciales inválidas.";
                return View(model);
            }
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Usuarios", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ViewBag.Error = "El correo ya está registrado.";
                return View(model);
            }
            else if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "No se pudo registrar el usuario.";
                return View(model);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Elimina todos los datos de la sesión
            return RedirectToAction("Login", "Usuario");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
