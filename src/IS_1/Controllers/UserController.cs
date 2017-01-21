using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IS_1.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace IS_1.Controllers
{
	[Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        // not idempotent
        //Register a new user. Aka create new user in the database
        [HttpPost]
        public IActionResult Register(string username, string password1, string password2)
        {
            Boolean userCreated = false;
            if (password1.Equals(password2))
            {
                User user = new IS_1.Models.User(username, password1);
                User temp = _context.Users.FirstOrDefault(u => u.Username == username);
                if (temp == null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    userCreated = true;
                }
                //else
                //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "The username is already taken." + "');", true);
            }
            //else
            //    MessageBox.Show("The passwords does not match!");
            //ATTENTION! the place to redirect might change
            return View(userCreated);
        }

        //looks if the password is correct.
        //return boolean
        // idempotent
        [HttpGet]
        public IActionResult IsPasswordCorrect(string username, string password)
        {
            Boolean correct = false;
            User temp = _context.Users.FirstOrDefault(u => u.Username == username);
            if (temp != null && temp.Password == password)
            {
                correct = true;
                GlobalVariables.CurrentUser = username;
            }
            //later we'll need to change this into a redirection.
            //so that if everything is allright, then we'll redirect
            //the user into the chatroom. 
            //If something's wrong, we'll redirect the user again to 
            //the log in page with an error message
            return View(correct);
        }
        // idempotent
        [HttpGet]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
