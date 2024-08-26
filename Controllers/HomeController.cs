using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Formatting;

// IMPORTANT -> PLEASE ENSURE THE API IS ALSO RUNNING WHEN USING THIS CLIENT

// The client does function WITHOUT the API
// however you will not be able to perform any tasks that require information from the database 

namespace BookstoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();

            // IMPORTANT -> Before running the client on your device
            // Please update the address below based on the localhost address of the API running on your device
            // Run the API and then copy the URL from your browser

            _httpClient.BaseAddress = new Uri("https://localhost:7130/");
        }

        // Here we display the home page for users
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                // Make a GET request to fetch all books
                var response = await _httpClient.GetAsync("api/books");
                if (response.IsSuccessStatusCode)
                {
                    var books = await response.Content.ReadAsAsync<List<Book>>();
                    return View(books);
                }
                else
                {
                    // Handle unsuccessful response
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _logger.LogError(errorMessage);
                    return View(new List<Book>()); // Return empty list of books
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books from the API.");
                return View(new List<Book>()); // Return empty list of books
            }
        }

        // Here we fetch books from the database, based on the user's search term
        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            try
            {
                // Make a GET request to search books
                var response = await _httpClient.GetAsync($"api/Home/search?searchString={searchString}");
                if (response.IsSuccessStatusCode)
                {
                    var books = await response.Content.ReadAsAsync<List<Book>>();
                    return View("Index", books); // Return Index view with searched books
                }
                else
                {
                    // Handle unsuccessful response
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    _logger.LogError(errorMessage);
                    return RedirectToAction("Index"); // Redirect back to Index view
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching books from the API.");
                return RedirectToAction("Index"); // Redirect back to Index view
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
