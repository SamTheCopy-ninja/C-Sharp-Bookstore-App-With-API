using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreClient.Controllers
{
    public class ViewOrdersController : Controller
    {
        private readonly HttpClient _httpClient;

        public ViewOrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

            // IMPORTANT -> Before running the client on your device
            // Please update the address below based on the localhost address of the API running on your device
            // Run the API and then copy the URL from your browser

            _httpClient.BaseAddress = new Uri("https://localhost:7130/");
        }

        // Here we a return a view to allow admins to view all customer orders
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/ViewOrders");
            if (response.IsSuccessStatusCode)
            {
                var allCartItems = await response.Content.ReadFromJsonAsync<List<Carts>>();
                return View(allCartItems);
            }
            else
            {
                return View(new List<Carts>()); // Return an empty list if request fails
            }
        }

        // Here an admin can search for cart items that belong to a specific user
        public async Task<IActionResult> GetCartItemsByUser(string userId)
        {
            var response = await _httpClient.GetAsync($"api/ViewOrders/ByUser/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var userCartItems = await response.Content.ReadFromJsonAsync<List<Carts>>();
                return View(userCartItems);
            }
            else
            {
                return View(new List<Carts>()); // Return an empty list if request fails
            }
        }
    }
}
