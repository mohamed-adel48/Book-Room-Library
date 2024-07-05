using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BookStore.Models;
namespace BookStore.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult account()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string username, string password)
        {
            StoreEntities entities = new StoreEntities();
            var user = entities.Users.Where(u => u.Username == username).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == password)
                {
                    HttpCookie cookie = new HttpCookie("username");
                    cookie.Value = username;
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("account");
                }

            }
            else
            {
                return RedirectToAction("account");
            }

        }
        [HttpPost]
        public ActionResult signup(string username, string email, string password)
        {
            StoreEntities entities = new StoreEntities();
            int userID = entities.Users.ToList().LastOrDefault().Id;
            userID++;
            var user = new User() { Id = userID, Email = email, Password = password, Username = username };
            entities.Users.Add(user);
            entities.SaveChanges();
            HttpCookie cookie = new HttpCookie("username");
            cookie.Value = username;
            Response.Cookies.Add(cookie);
            var cart = new Cart();
            var last = entities.Carts.ToList().LastOrDefault();
            int id = 0;
            if (last != null) { 
             id = last.Id;
            id++;
            }
            
            cart.user_id = user.Id;
            cart.Id = id;
            entities.Carts.Add(cart);
            Favorite fav = new Favorite();
            fav.user_id = user.Id;
            var favList = entities.Favorites.ToList().LastOrDefault();
            int favID = 0;
            if (favList != null)
            {
                favID = favList.Id;
                favID++;
            }

            
            fav.Id =favID;
            entities.Favorites.Add(fav);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult arabicbooks()
        {
            return View();
        }

        public ActionResult italianbooks()
        {
            return View();
        }

        public ActionResult spanishbooks()
        {
            return View();
        }

        public ActionResult book1()
        {
            var id = int.Parse(Request.Params["id"]);
            StoreEntities entities = new StoreEntities();
            var Book = entities.Books.Where(b => b.Id == id).FirstOrDefault();
            return View(Book);
        }

        public ActionResult book2()
        {
            return View();
        }

        public ActionResult book3()
        {
            return View();
        }

        public ActionResult cart()
        {
            StoreEntities entities = new StoreEntities();
            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();
            if (user != null)
            {

                var cart = entities.Carts.Where(c => c.user_id == user.Id).FirstOrDefault();
                List<cartProducts> books = new List<cartProducts>();
                if (cart.Books != null)
                {
                    var list = cart.Books.Split(' '); // ["1,1","2,2","3,2"]

                    foreach (var book in list)
                    {
                        if (book != null && book != string.Empty && book != " ")
                        {
                            cartProducts b = new cartProducts();
                            int bookId = int.Parse(book.Split(',')[0]);
                            var current = entities.Books.Where(r => r.Id == bookId).FirstOrDefault();
                            b.book = current;
                            int quant = int.Parse(book.Split(',')[1]);
                            b.quantity = quant;
                            books.Add(b);
                        }
                    }
                }
                return View(books);
            }
            return RedirectToAction("login");
        }

        public ActionResult contact()
        {
            return View();
        }

        public ActionResult ebook()
        {
            int i = 1;
            if (Request.Params["page"] != null)
            {
                i = int.Parse(Request.Params["page"]);
            }


            StoreEntities entities = new StoreEntities();
            var books = entities.Books.ToList();
            var b = books.ToPagedList(i, 12);
            return View(b);
        }

        public ActionResult ebook2()
        {
            return View();
        }

        public ActionResult ebook3()
        {
            return View();
        }

        public ActionResult ebook4()
        {
            return View();
        }

        public ActionResult ebook5()
        {
            return View();
        }

        public ActionResult favorite()
        {
            StoreEntities entities = new StoreEntities();

            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();
            if (user != null)
            {
                var myFav = entities.Favorites.Where(f => f.user_id == user.Id).FirstOrDefault();
                List<Book> books = new List<Book>();
                if (myFav.Book != null) { 
                var list = myFav.Book.Split(' ');
                foreach (var book in list)
                {
                    if (book != null && book != string.Empty)
                    {
                        int bId = int.Parse(book);
                        var currentBook = entities.Books.Where(b => b.Id == bId).FirstOrDefault();
                        books.Add(currentBook);
                    }
                }
                }
                return View(books);
            }

            return RedirectToAction("account");
        }

        public ActionResult userprofile()
        {
            return View();
        }
        public ActionResult orderPlaced()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addToCart(string id, string quantity)
        {
            StoreEntities entities = new StoreEntities();
            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();
            if (user != null)
            {

                var cart = entities.Carts.Where(c => c.user_id == user.Id).FirstOrDefault();
                cart.Books += " " + id + "," + quantity;
                entities.SaveChanges();
                return RedirectToAction("cart");
            }
            else
            {
                return RedirectToAction("account");
            }

        }
        public ActionResult addToFav(string id)
        {
            StoreEntities entities = new StoreEntities();
            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();
            if (user != null)
            {
                var myFav = entities.Favorites.Where(f => f.user_id == user.Id).FirstOrDefault();
                myFav.Book += " " + id;
                entities.SaveChanges();
                return RedirectToAction("favorite");
            }
            return RedirectToAction("account");
        }
        public ActionResult removeFav(string id)
        {
            StoreEntities entities = new StoreEntities();
            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();
            if (user != null)
            {
                var myFav = entities.Favorites.Where(f => f.user_id == user.Id).FirstOrDefault();
                var list = myFav.Book.Split(' ');
                string newData = "";
                foreach (var book in list)
                {
                    if (!book.Equals(id))
                    {
                        newData += " " + book;
                    }

                }
                newData = newData.TrimStart();
                newData = newData.TrimEnd();
                myFav.Book = newData;
                entities.SaveChanges();
                return RedirectToAction("favorite");
            }
            return RedirectToAction("account");
        }
        public ActionResult removeCart(string id)
        {
            StoreEntities entities = new StoreEntities();
            var username = Request.Cookies.Get("username");
            var user = entities.Users.Where(u => u.Username.Equals(username.Value)).FirstOrDefault();

            if (user != null)
            {
                var myCart = entities.Carts.Where(f => f.user_id == user.Id).FirstOrDefault();
                var list = myCart.Books.Split(' '); // ["1,1" , "2,3" , "5,2"]
                string newData = "";
                foreach (var book in list)
                {
                    if (!book.Split(',')[0].Equals(id))
                    {
                        newData += " " + book;
                    }

                }
                // " 1,2 3,3 "
                newData = newData.TrimStart();
                newData = newData.TrimEnd();
                myCart.Books = newData;
                entities.SaveChanges();
                return RedirectToAction("cart");
            }
            return RedirectToAction("account");
        }
    }
}