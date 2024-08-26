using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Firebase.Auth;
using Newtonsoft.Json;

namespace BookstoreClient.Controllers
{
    // This is the authentication controller that handles regstration and login, using Firebase

    public class AuthController : Controller
    {
        FirebaseAuthProvider auth;

        public AuthController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("bookstore")));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Allow users to login using Firebase
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string currentUserId = fbAuthLink.User.LocalId;

                //version 2
                // Check if the user's email contains the word "admin"
                if (currentUserId != null && IsAdminEmail(login.Email))
                {

                    HttpContext.Session.SetString("currentUser", currentUserId);

                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Index", "Books");
                }

                //version 1
                //if (currentUserId != null)
                //{
                //    HttpContext.Session.SetString("currentUser", currentUserId);
                //    return RedirectToAction("Index", "Books");
                //}


            }
            catch (FirebaseAuthException ex)
            {

                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);

                Utils.AuthLogger.Instance.LogError(firebaseEx.error.message + " - User: " + login.Email + " - IP: " + HttpContext.Connection.RemoteIpAddress
     + " - Browser: " + Request.Headers.UserAgent);

                return View(login);
            }

            return View();
        }

        private bool IsAdminEmail(string email)
        {
            // Check if the email contains the word "admin" 
            return email.ToLowerInvariant().Contains("admin");
        }

        // LogOut the current user
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("currentUser");
            return RedirectToAction("Login");
        }


        // Register a new user
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Send registration info to Firebase for authentication
        [HttpPost]
        public async Task<IActionResult> Register(LoginModel login)
        {
            try
            {
                await auth.CreateUserWithEmailAndPasswordAsync(login.Email, login.Password);

                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string currentUserId = fbAuthLink.User.LocalId;

                if (currentUserId != null)
                {
                    HttpContext.Session.SetString("currentUser", currentUserId);
                    return RedirectToAction("Login", "Auth");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(login);
            }

            return View();
        }

    }
}
