using LMS_UI.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace LMS_UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public AuthController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _httpClient = clientFactory.CreateClient("API");
            _config = config;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var json = JsonSerializer.Serialize(loginDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }

            var responseData = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

            HttpContext.Session.SetString("JWToken", responseData.Token);
            HttpContext.Session.SetString("Role", responseData.Role);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDTO.userName),
                new Claim(ClaimTypes.Role, responseData.Role),
                new Claim("AccessToken", responseData.Token)

            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return responseData.Role == "Admin"
                ? RedirectToAction("Index", "AdminDashboard")
                : RedirectToAction("Index", "UserDashboard");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var json = JsonSerializer.Serialize(registerDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
