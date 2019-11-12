using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unicorns.Models;

namespace Unicorns.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // The below model and constructor are for a user to register.
        // The below lines are referenced w/n _register.cshtml and takes affect when the 'Register' button is clicked.
        // User comes from 'public class User' w/n Users.cs file and user is a variable we can call.
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                // u is the user in the database (u.Email) and we're checking to see if the email already exists
                // if the email does not equal to null (meaning it's not unique), display the error message.
                User userMatchingEmail = context.Users.FirstOrDefault(u => u.Email == user.Email);
                if(userMatchingEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                }
                else
                {
                    // The below two lines ensures we're not passing plain text for the password.
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    context.Users.Add(user);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("userid", user.UserID);
                    return Redirect("/success");
                }
            }
            return View("index", user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser user)
        {
            if(ModelState.IsValid)
            {
                // u is the user in the database (u.Email) and we're checking to see if the email already exists
                // if the email does not equal to null (meaning it's not unique), display the error message.
                User userMatchingEmail = context.Users.FirstOrDefault(u => u.Email == user.LoginEmail);
                if(userMatchingEmail == null)
                {
                    ModelState.AddModelError("LoginEmail", "Unknown Email!");
                }
                else
                {
                    // The below two lines ensures we're not passing plain text for the password.
                    PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                    var result = Hasher.VerifyHashedPassword(user, userMatchingEmail.Password, user.LoginPassword);
                    if(result == 0)
                    {
                        ModelState.AddModelError("LoginPassword", "Incorrect Password!");
                    }
                    else
                    {
                    HttpContext.Session.SetInt32("userid", userMatchingEmail.UserID);
                    return Redirect("/success");
                    }
                }
            }
            return View("Index");
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            int? userid = HttpContext.Session.GetInt32("userid");
            if(userid == null)
            {
                return Redirect("/");
            }
            ViewBag.UserID = (int) userid;
            return View();
        }

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        // This is how we access data from the database.
        // It references the homecontroller to create the database
        private MyContext context;
        public HomeController(MyContext mc)
        {
            context = mc;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
