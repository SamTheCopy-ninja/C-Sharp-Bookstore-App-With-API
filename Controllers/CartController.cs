using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookstoreClient.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();

            // IMPORTANT -> Before running the client on your device
            // Please update the address below based on the localhost address of the API running on your device
            // Run the API and then copy the URL from your browser

            _httpClient.BaseAddress = new Uri("https://localhost:7130/");
        }

        // Here a user can manage their cart

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
                // Fetch the userId of the current user
                string userId = HttpContext.Session.GetString("currentUser");

                // Make a GET request to the API endpoint to fetch cart items
                var response = await _httpClient.GetAsync($"/api/Cart?userId={userId}");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to a list of cart items
                    var cartItemsJson = await response.Content.ReadAsStringAsync();
                    var cartItems = JsonConvert.DeserializeObject<List<Carts>>(cartItemsJson);

                    // Return the list of cart items to the view
                    return View("ViewCart", cartItems);
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
                return StatusCode(500, "An error occurred while fetching cart items from the server.");
            }
        }

        // This allows a user to remove items from their cart
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                // Create the URL with the cartItemId 
                var apiUrl = $"api/Cart/RemoveFromCart/{cartItemId}";

                // Make a DELETE request to the API endpoint
                var response = await _httpClient.DeleteAsync(apiUrl);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Item removed from cart successfully
                     return Ok("Item removed from cart successfully");

                   

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
                return StatusCode(500, "An error occurred while removing the item from the cart");
            }
        }

    }
}
