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
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;    
        }

        //FOR ANDROID. Same as Index & Update ???
        [HttpGet("Android")]            //  api/Message/Android
        public JsonResult Chatroom()
        {
            return Json(_context.Messages.ToList());
        }


        // GET: Messages
        [HttpGet("Index", Name = "Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        [HttpGet("exit", Name = "Exit")]
        public IActionResult Exit()
        {
            GlobalVariables.CurrentUser = "";
            return RedirectToAction("index", "user", "api/user/index/");
        }

        //FOR ANDROID. Same as Create
        [Route("send")]     //  api/send   ?
        [HttpPost]
        public async Task<JsonResult> Send(Dictionary<string, string> input)
        {
            string user, text;
            //return Json(_context.Messages.ToList()); 
            input.TryGetValue("Username", out user);
            input.TryGetValue("Text", out text);
            Message message = new WebApplication1.Models.Message(text, user);

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // PAKEISTI (veliau)????
            return Json(_context.Messages.ToList());
        }

        //creates the new text message
        // not idempotent
        [Route("create", Name = "Refresh")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dictionary<string, string> t)
        {
            string temp;
            t.TryGetValue("Text", out temp);
            Message message = new WebApplication1.Models.Message(temp, GlobalVariables.CurrentUser);
            
           try { 
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                //GlobalVariables.Messages.Add(message);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            //ATTENTION! the place to redirect might change.
            //we'll need to redirect the user into the chatroom page

            //later: redirect to update + update function
            return RedirectToAction("index", "message", "api/message/index/");
        }

        [HttpGet("update")]
        public IActionResult Update()
        {
            // cia galim dirbti toliau
            // ar tai tikrai taip ir veiks???
            return RedirectToAction("index", "message", "api/message/index/");
        }

    }
}
