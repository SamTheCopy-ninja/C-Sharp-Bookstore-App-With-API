using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookstoreClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

            // IMPORTANT -> Before running the client on your device
            // Please update the address below based on the localhost address of the API running on your device
            // Run the API and then copy the URL from your browser

            _httpClient.BaseAddress = new Uri("https://localhost:7130/");
        }

        public async Task<IActionResult> Index()
        {
            // Check sessions, if user is not logged in redirect them to the login page
            var token = HttpContext.Session.GetString("currentUser");


            if (token == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            try
            {
                // Make a GET request to the API endpoint to fetch books
                var response = await _httpClient.GetAsync("api/Books");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of books to the user
                    var booksJson = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<List<Book>>(booksJson);

                    // Return the list of books to the view
                    return View(books);
                }
                else
                {
                    // Handle unsuccessful response
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                // Log errors
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while fetching books from the server.");
            }
        }


        // Here we add books to a cart
        public async Task<IActionResult> AddBookToCart(int bookId)
        {
            // Fetch the userId of the current user
            string userId = HttpContext.Session.GetString("currentUser");

            try
            {
                // Create the URL with the bookId and userId as query parameters
                var apiUrl = $"api/Books/addToCart?bookId={bookId}&userId={userId}";

                // Make a POST request to the API endpoint
                var response = await _httpClient.PostAsync(apiUrl, null);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Book added to cart successfully
                    return Ok("Book added to cart successfully");
                }
                else
                {
                    // Handle unsuccessful response
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorMessage);
                }
            }
            catch (Exception ex)
            {
                // Log errors
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while adding the book to the cart");
            }
        }



    }
}
