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
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

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
        public JsonResult Android()
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
            return RedirectToAction("index", "User", "api/User/index/");
        }

        //FOR ANDROID. Same as Create
        //parse Json
        [AllowAnonymous]
        [ActionName("Send")]
        [Route("Send/{id?}")]     //  api/message/Send   
        [HttpPost]
        public IActionResult Send([FromBody]
            //dynamic input
            Dictionary<string, string> t
            )
        {
            
            //var client = new HttpClient();
            ////client.BaseAddress = new Uri("http://localhost:1302/api/");
            //client.BaseAddress = new Uri("applicationUrl");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //return RedirectToAction("Android", "message", "api/Message/Android");
            //JToken token = JObject.Parse(input);

            string user, text;
            //return Json(_context.Messages.ToList()); 
            t.TryGetValue("Username", out user);
            t.TryGetValue("Text", out text);

            //user = (string)token.SelectToken("Username");
            //text = (string)token.SelectToken("Text");
            //return Ok(user);
            Message message = new WebApplication1.Models.Message(text, user);

            _context.Messages.Add(message);
            _context.SaveChanges();

            // PAKEISTI (veliau)????
            return RedirectToAction("Android", "message", "api/message/Android");
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
