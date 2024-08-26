using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace BookstoreClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

            // IMPORTANT -> Before running the client on your device
            // Please update the address below based on the localhost address of the API running on your device
            // Run the API and then copy the URL from your browser

            _httpClient.BaseAddress = new Uri("https://localhost:7130/");
        }

        // Here we fetch the view for the admin dashboard, after successfully authentication
        [HttpGet]
        public IActionResult AdminDashboard()
        {
            return View("~/Views/Dashboard/AdminDashboard.cshtml");
        }

        // Here an admin adds a book to the database, by performing a POST request tot the API
        // The API will then process the request and add the book the database
        [HttpPost]
        public async Task<IActionResult> AddBook(BookInputModel bookInput)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // First we serialize the book input model to JSON
                    var content = new StringContent(JsonConvert.SerializeObject(bookInput), Encoding.UTF8, "application/json");

                    // Make a POST request to the API endpoint
                    var response = await _httpClient.PostAsync("api/Admin/AddBook", content);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Then return back to the dashboard
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        // Handle unsuccessful response
                        ViewBag.ErrorMessage = "An error occurred while adding the book.";
                        return RedirectToAction("AdminDashboard");

                    }
                }
                catch (Exception ex)
                {
                    // Log errors
                    ViewBag.ErrorMessage = "An error occurred while adding the book: " + ex.Message;
                    return RedirectToAction("AdminDashboard");

                }
            }

            // If model state is not valid
            return RedirectToAction("AdminDashboard");

        }
    }
}
