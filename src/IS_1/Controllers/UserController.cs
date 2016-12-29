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
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            //ATTENTION! the place to redirect might change
            return RedirectToAction("Index");
        }

        //looks if the password is correct.
        //return boolean
        [HttpGet]
        public IActionResult IsPasswordCorrect(User user)
        {
            Boolean correct = false;
            User temp = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (temp != null && temp.Password == user.Password)
            {
                correct = true;
            }
            return View(correct);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
