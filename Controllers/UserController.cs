using LMS_UI.DTOs;
using LMS_UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;

namespace LMS_UI.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBase;

        public UserController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient("API");
            _apiBase = config["ApiSettings:BaseUrl"];
        }

        private void AddAdminToken()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // 1. GET: All Users
        public async Task<IActionResult> Index()
        {
            AddAdminToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/user");
            if (!response.IsSuccessStatusCode) return Unauthorized();
            var users = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
            return View(users);
        }

       /* // 2. GET: Add form
        public IActionResult Create() => View();

        // 2. POST: Create User
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            AddAdminToken();
            var response = await _httpClient.PostAsJsonAsync($"{_apiBase}/api/user", user);
            if (!response.IsSuccessStatusCode) return View(user);
            return RedirectToAction("Index");
        } */

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(RegisterDTO registerDTO)
        {
            var json = JsonSerializer.Serialize(registerDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBase}/api/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Registration failed.");
                return View();
            }

            return RedirectToAction("Index");
        }

        // 3. GET: User Details by ID
        public async Task<IActionResult> Details(int id)
        {
            AddAdminToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/user/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return View(user);
        }

        [HttpGet]
        // 4. GET: Edit Form
        public async Task<IActionResult> Edit(int id)
        {
            AddAdminToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/user/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return View(user);
        }

        // 4. PUT: Update User
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            AddAdminToken();
            var response = await _httpClient.PutAsJsonAsync($"{_apiBase}/api/user/{user.userID}", user);
            if (!response.IsSuccessStatusCode) return View(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AddAdminToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/user/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
            return View(user); // Show confirmation view
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = HttpContext.Session.GetString("AccessToken");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"{_apiBase}/api/user/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete user.");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
