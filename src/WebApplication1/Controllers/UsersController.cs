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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Users
        [HttpGet("Index/{id}")]
        public IActionResult Index()
        {
            return View();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Username, string Password1, string Password2)
        {
            //Boolean userCreated = false;
            if (Password1.Equals(Password2) && !Password1.Equals(""))   // if passwords match and is not null
            {
                User user = new WebApplication1.Models.User(Username, Password1);
                User temp = _context.Users.FirstOrDefault(u => u.Username == Username);
                if (temp == null && ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    //userCreated = true;
                }
                //else
                //    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "The username is already taken." + "');", true);
            }
            //else
            //    MessageBox.Show("The passwords does not match!");
            //ATTENTION! the place to redirect might change
            return RedirectToAction("Index");
        }

        [HttpGet("Login/{id}")]
        public IActionResult Login(string username, string password)
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
            // later: redirect to messages
            return RedirectToAction("Messages/Index/");
        }


        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(string? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .SingleOrDefaultAsync(m => m.Username == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(string? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.SingleOrDefaultAsync(m => m.Username == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Username,Password")] User user)
        //{
        //    if (id != user.Username)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Username))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(string? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .SingleOrDefaultAsync(m => m.Username == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var user = await _context.Users.SingleOrDefaultAsync(m => m.Username == id);
        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool UserExists(string id)
        //{
        //    return _context.Users.Any(e => e.Username == id);
        //}
    }
}
