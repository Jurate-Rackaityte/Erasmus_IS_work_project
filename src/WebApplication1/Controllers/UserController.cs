using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Users
        [HttpGet("Index")]
        public IActionResult Index()
        {
            
            return View(_context.Users);
        }


        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Username,Password")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

        // not idempotent
        //Register a new user. Aka create new user in the database
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dictionary<string,string> d)
        //public IActionResult Create([Bind("Username,Password1, Password2")] Register r)
        {
            string user, p1, p2;
            d.TryGetValue("Username", out user);
            d.TryGetValue("Password1", out p1);
            d.TryGetValue("Password2", out p2);
            //return Ok(user + " "+ p1 + " " + p2);
            if (p1.Equals(p2) && !p1.Equals(""))   // if passwords match and is not null
            {
                
                User tempUser = new WebApplication1.Models.User(user, p1);
                //User temp = _context.Users.First(u => u.Username == user);
                
                try
                {
                    //if (temp != null)
                    //{
                       // ModelState.AddModelError("", "Username is not unique. " +
                       // "Try again with another username. ");
                   // }
                   // else
                   // {
                        //return Ok("!!!Pralindau pro kontrole!!!");
                    _context.Add(tempUser);
                    await _context.SaveChangesAsync();
                    //GlobalVariables.Users.Add(tempUser);
                    //return Ok("Uzregistravau!");
                        //userCreated = true;
                  //  }
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Username is not unique. " +
                        "Try again with another username. ");
                }
                //else
                //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "The username is already taken." + "');", true);
            }
            //else
            //    MessageBox.Show("The passwords does not match!");
            //ATTENTION! the place to redirect might change
            return
            //Ok("ALEHANDER");
            RedirectToAction("Index");
            //Redirect(Request.QueryString["r"]);
        }
        [Route("login")]
        [HttpPost]
        public IActionResult Login(Dictionary<string, string> r)
        {
            //Boolean correct = false;
            string user, p;
            r.TryGetValue("Username", out user);
            r.TryGetValue("Password", out p);
            User temp = _context.Users.FirstOrDefault(u => u.Username == user);
            //return Ok(temp.Username + "\n"+temp.Password+"\n"+p);
            //try { 
                if (!temp.Username.Equals("") && temp.Password == p)
                {
                    //correct = true;
                    GlobalVariables.CurrentUser = user;
                    //return Ok("Prisijungiau!");
                    return RedirectToAction("index", "message", "api/message/index/");
                }
            // }
            //catch (DbUpdateException /* ex */)
            //{
            ////Log the error (uncomment ex variable name and write a log.
            //ModelState.AddModelError("", "Login username or password " +
            //    "is wrong. Try again. ");
            //}
            //later we'll need to change this into a redirection.
            //so that if everything is allright, then we'll redirect
            //the user into the chatroom. 
            //If something's wrong, we'll redirect the user again to 
            //the log in page with an error message
            // later: redirect to messages
            return RedirectToAction("index", "user", "api/user/index/");
        }

    }
}
