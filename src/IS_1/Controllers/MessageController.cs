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
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //creates the new text message
        // not idempotent
        [HttpPost]
        public IActionResult Create(string text)
        {
            Message message = new IS_1.Models.Message(text, GlobalVariables.CurrentUser);
            _context.Messages.Add(message);
            _context.SaveChanges();
            //ATTENTION! the place to redirect might change.
            //we'll need to redirect the user into the chatroom page
            return RedirectToAction("Index");
        }



        // GET: /<controller>/
        //aka "Read All" - to view a chat
        //To-Do: paziureti, ar tikrai visada automatiskai
        //       surusiuoja pagal laika ir ar tinkama tvarka
        //translation (tl;dr): check if this will be ok and will it 
        //              sort the messages according to their dates in the right order
        // idempotent
        [HttpGet]
        //veliau: atiduoti ne view, o json. Is android paskui atgal pasiimt json ir susiformatuoti. 
        //tikriausiai atgal i array suformatuos 
        //-------------------------------------
        // Translation:
        //To-DO: return not a view, but json. Later, when on android, take json and formate it into something appropriate
        //it will probably formate it back into the array
        public IActionResult Index()
        {
            var model = _context.Messages.ToList();
            return View(model);
        }
    }
}
