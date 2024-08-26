using Microsoft.AspNetCore.Mvc;

namespace BookstoreClient.Models
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;

    public class EditBooksController : Controller
    {
        private readonly HttpClient _httpClient;

        public EditBooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7130/"); // Replace with your API base URL
        }

        public async Task<IActionResult> AllBooks()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/EditBooks/AllBooks");
                if (response.IsSuccessStatusCode)
                {
                    var books = await response.Content.ReadFromJsonAsync<List<Book>>();
                    return View("~/Views/BookUpdates/AllBooks.cshtml", books);
                }
                else
                {
                    // Return an empty list if request fails
                    return View("~/Views/BookUpdates/AllBooks.cshtml", new List<Book>());
                }
            }
            catch (Exception ex)
            {
                // Log any errors and return an error view
                ViewBag.ErrorMessage = "An error occurred while fetching books: " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/EditBooks/Edit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var book = await response.Content.ReadFromJsonAsync<Book>();
                    return View("~/Views/BookUpdates/Edit.cshtml", book);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log any errors and return an error view
                ViewBag.ErrorMessage = "An error occurred while fetching the book: " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, int newQuantity)
        {
            try
            {
                // Log the received values
                Console.WriteLine($"Received ID: {id}");
                Console.WriteLine($"Received New Quantity: {newQuantity}");

                // Log the quantity value before sending the request
                Console.WriteLine($"Quantity value before sending request: {newQuantity}");

                var request = new HttpRequestMessage(HttpMethod.Post, $"api/EditBooks/Edit?bookId={id}&newQuantity={newQuantity}");

                // Log the URL of the request
                Console.WriteLine($"Request URL: {request.RequestUri}");

                var response = await _httpClient.SendAsync(request);

                // Log the status code of the response
                Console.WriteLine($"Response status code: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    // Log success message
                    Console.WriteLine("Update successful!");

                    return RedirectToAction("AllBooks");
                }
                else
                {
                    // Log error message
                    Console.WriteLine("Update failed!");

                    ViewBag.ErrorMessage = "An error occurred while updating the book quantity.";
                    return View("~/Views/BookUpdates/Edit.cshtml");
                }
            }
            catch (Exception ex)
            {
                // Log exception message
                Console.WriteLine($"An error occurred: {ex.Message}");

                ViewBag.ErrorMessage = "An error occurred while updating the book quantity: " + ex.Message;
                return View("~/Views/Shared/Error.cshtml");
            }
        }


    }

}
