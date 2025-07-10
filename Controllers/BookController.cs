using LMS_UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace LMS_UI.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBase;

        public BookController(IHttpClientFactory factory, IConfiguration config)
        {
            _httpClient = factory.CreateClient("API");
            _apiBase = config["ApiSettings:BaseUrl"];
        }

        private void AddAuthToken()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // 1. GET all books
        public async Task<IActionResult> Index()
        {
            AddAuthToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/book");
            if (!response.IsSuccessStatusCode) return Unauthorized();
            var books = await response.Content.ReadFromJsonAsync<List<BookViewModel>>();
            return View(books);
        }

        // 2. POST: Add book (GET form)
        public IActionResult Create() => View();

        // 2. POST: Add book (SUBMIT)
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            AddAuthToken();
            var response = await _httpClient.PostAsJsonAsync($"{_apiBase}/api/book", book);
            if (!response.IsSuccessStatusCode) return View(book);
            return RedirectToAction("Index");
        }

        // 3. GET book by ID (details)
        public async Task<IActionResult> Details(int id)
        {
            AddAuthToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/book/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
            return View(book);
        }

        [HttpGet]
        // 4. PUT: Update book (GET form)
        public async Task<IActionResult> Edit(int id)
        {
            AddAuthToken();
            var response = await _httpClient.GetAsync($"{_apiBase}/api/book/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();
            var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
            return View(book);
        }

        // 4. PUT: Update book (SUBMIT)
        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel book)
        {
            AddAuthToken();
            var response = await _httpClient.PutAsJsonAsync($"{_apiBase}/api/book/{book.bookId}", book);
            if (!response.IsSuccessStatusCode) return View(book);
            return RedirectToAction("Index");
        }

        // 5. DELETE book
        // GET: Books/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBase}/api/book/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var book = await response.Content.ReadFromJsonAsync<BookViewModel>();
            return View(book); // Show confirmation view
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = HttpContext.Session.GetString("AccessToken");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiBase}/api/book/{id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Borrow() => View();
        // 6. POST: Borrow book
        [HttpPost]
        public async Task<IActionResult> Borrow(int id)
        {
            AddAuthToken();
            var response = await _httpClient.PostAsync($"{_apiBase}/api/book/borrow/{id}", null);
            if (!response.IsSuccessStatusCode) return BadRequest();
            return RedirectToAction("Index");
        }

        public IActionResult Return() => View();
        // 7. POST: Return book
        [HttpPost]
        public async Task<IActionResult> Return(int id)
        {
            //check
            AddAuthToken();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_apiBase}/api/book/return/{id}");
            var response = await _httpClient.SendAsync(request);
            return RedirectToAction("Index");
        }
    }
}
