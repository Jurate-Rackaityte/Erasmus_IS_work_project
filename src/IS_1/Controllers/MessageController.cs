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
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
            //ATTENTION! the place to redirect might change
            return RedirectToAction("Index");
        }



        // GET: /<controller>/
        //aka "Read All" - to view a chat
        //To-Do: paziureti, ar tikrai visada automatiskai
        //       surusiuoja pagal laika ir ar tinkama tvarka
        [HttpGet]
        public IActionResult Index()
        {
            var model = _context.Messages.ToList();
            return View(model);
        }
    }
}
