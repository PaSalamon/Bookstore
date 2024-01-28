using BookstoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LoginButton(UserModel user)
        {
            user.Email = user.Email.ToLower();
            string partOfURL = user.Email + "/" + user.Password;

            var client = new HttpClient();

            var response = await client.GetAsync("https://localhost:7233/api/Users/" + partOfURL);
            Console.WriteLine("https://localhost:7233/api/Users" + partOfURL);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Posted");
                return RedirectToAction("BookstoreClient");
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
                return RedirectToAction("LoginError");
            }
        }

        public IActionResult LoginError()
        {
            return View();
        }

        public async Task<IActionResult> BookstoreClient()
        {
            List<BookModel> books = new List<BookModel>();
            var client = new HttpClient();
            await Task.Run(() =>
            {
                var response = client.GetAsync("https://localhost:7233/api/Books").Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<BookModel>>(dataObjects);

                    foreach( var item in result)
                    {
                        if ( item != null)
                        {
                            books.Add(item);
                        }
                    }
                }
            });
            return View(books);
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> RegisterButton(UserModel user)
        {
            user.UserId = Guid.NewGuid().ToString();
            user.Email = user.Email.ToLower();

            var client = new HttpClient();

            string json = JsonConvert.SerializeObject(user);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7233/api/Users/", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Posted");
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> AddBookButton(BookModel book)
        {
            book.BookId = Guid.NewGuid().ToString();

            var client = new HttpClient();

            string json = JsonConvert.SerializeObject(book);

            StringContent httpContent = new StringContent(json,System.Text.Encoding.UTF8,"application/json");

            var response = await client.PostAsync("https://localhost:7233/api/Books", httpContent);
            System.Console.WriteLine(json);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Posted");
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode,response.ReasonPhrase);
            }

            return RedirectToAction("BookstoreClient");
        }

        public async Task<IActionResult> RemoveBook(string id)
        {
            BookModel book = new BookModel();
            var client = new HttpClient();
            await Task.Run(() =>
            {
                var response = client.GetAsync("https://localhost:7233/api/Books/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BookModel>(dataObjects);

                    if (result.BookId == id)
                    {
                        book = result;
                    }
                }
            });
            return View(book);
        }

        public async Task<IActionResult> RemoveBookButton(BookModel book)
        {
            var client = new HttpClient();

            Console.WriteLine(book.BookId);
            var response = await client.DeleteAsync("https://localhost:7233/api/Books/" + book.BookId);
            Console.WriteLine(response);
            Console.WriteLine("https://localhost:7233/api/Books/" + book.BookId);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Posted");
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
            }

            return RedirectToAction("BookstoreClient");
        }

        public async Task<IActionResult> EditBook(string id)
        {
            BookModel book = new BookModel();
            var client = new HttpClient();
            await Task.Run(() =>
            {
                var response = client.GetAsync("https://localhost:7233/api/Books/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<BookModel>(dataObjects);

                        if (result.BookId == id)
                        { 
                            book = result;
                        }
                }
            });
            return View(book);
        }

        public async Task<IActionResult> EditBookButton(BookModel book)
        {
            System.Console.WriteLine(book.Price);
            var client = new HttpClient();

            string json = JsonConvert.SerializeObject(book);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7233/api/Books", httpContent);
            System.Console.WriteLine(json);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data Posted");
            }
            else
            {
                Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
            }

            return RedirectToAction("BookstoreClient");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
