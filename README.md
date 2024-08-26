# RS Bookstore Client

Welcome to the RS Bookstore client

## Description

RS Bookstore is a web application that allows users to explore and manage an online bookstore.   
Users can view and purchase books, as well as manage their shopping carts.  

The app also allows Administrators to add, edit, and view inventory details.  
## This client app works alongside an API. Please download the API using this link: [Bookstore API](https://github.com/SamTheCopy-ninja/C-Sharp-Bookstore-API.git)  
### Please ensure the API is running, on your device, whenever you run this client app

## Features

- User authentication and authorization, using Firebase
- Browsing and searching for books
- Shopping cart management for users
- Admin dashboard for inventory management, adding more admins, viewing customer orders

## Installation

To install the RS Bookstore client, download the .zip file or clone the repository:

```bash
# Clone the repository
git clone https://github.com/SamTheCopy-ninja/C-Sharp-Bookstore-App-With-API.git
```  

## Configuration  

### Connecting to the API  
- Download and Run the API (using the link provied) on your device and copy the `https://localhost:1111/` address in your browser.
  
- Open the client app, and replace the existing `_httpClient.BaseAddress = new Uri("https://localhost:1111/");` URL in the `HomeController`, `BooksController`, `CartController` and `ViewOrdersController` files with your own localhost URL.  

- Your URL should be placed in all the sections indicated as follows, within each controller:  
> // IMPORTANT -> Before running the client on your device  
// Please update the address below based on the localhost address of the API running on your device  
// Run the API and then copy the URL from your browser

Ensure all the controllers indicated above are pointing to your own localhost API URL before moving on to the `Integrate Firebase` section below.   

### Integrate Firebase  

1. **Go to the Firebase Console:**
   - Visit [Firebase Console](https://console.firebase.google.com/u/0/).

2. **Create a New WebApp Project:**
   - Click on "Add Project" to create a new project.
   - Name your project and then follow the setup instructions.

3. **Copy API Key:**
   - Once the project is created, navigate to the `project settings`.
   - In the configuration section, find and copy the API key.

4. **Add System-wide Environment Variable (Windows):**
   - Open the `Settings` menu on your Windows machine.
   - Click on the `System` tab
   - Go to the "Advanced System Setting" tab and click on "Environment Variables."
   - Under the "System variables" section, click "New."
   - Set the variable name to `bookstore` and paste the API key as the variable `value`.

5. **Save Environment Variable:**
   - Click "OK" to save the new environment variable.

6. **Reboot Your Machine:**
   - To apply the changes, reboot your VM/PC/laptop.
  
```diff
- After all the configurations above have been completed,
- the client app should be able to run on your device and communicate with the API
```

## Usage  
- Users can do the following:
1. **User Registration and Profile:**
   - Customers are able to register and create a profile.

2. **Inventory Viewing and Search:**
   - Customers are able to view items, details, and search the inventory.

3. **Cart Management:**
   - Customers are able to add multiple items to their cart.
   - Cart items are persisted between devices.

# Please Note
```diff
- To test and access Admin features:
- Please Register using an admin email address that contains the word `admin`. For example: admin@gmail.com.
- Then Login using that same admin email and password.
```
- Admin users can do the following:  
 **Admin Functionality:**
   - An admin can log in.
   - An admin can add other admins to the system.
   - An admin can view orders made by a customer.
   - An admin can add an item to the inventory.
   - An admin can update an item's available quantity.

**Authentication:**
   - Authentication has been implemented using Firebase Authentication.

**Login Logging:**
   - Any unsuccessful login attempts are logged.


## Authors  
- This app has been built by:
  `Samkelo Tshabalala`

  ## Technologies and Languages Used

- **Frontend:**
  - [HTML](https://developer.mozilla.org/en-US/docs/Web/HTML)
  - [CSS](https://developer.mozilla.org/en-US/docs/Web/CSS)
  - [JavaScript](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
  - [Bootstrap](https://getbootstrap.com/)
  - [Font Awesome](https://fontawesome.com/)

- **Backend:**
  - [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) programming language

- **Frameworks and Libraries:**
  - [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
  - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

- **Development Environment:**
  - [Visual Studio](https://visualstudio.microsoft.com/)

- **Version Control:**
  - [Git](https://git-scm.com/)

- **Authentication:**
  - [Firebase Authentication](https://firebase.google.com/docs/auth)

- **Other Tools:**
  - [Firebase Console](https://console.firebase.google.com/)
