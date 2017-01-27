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
        [HttpGet("Index", Name ="Chatroom")]
        public async Task<IActionResult> Index()
        {
            //var json = JsonConvert.SerializeObject(await _context.Messages.ToListAsync());
            //return View(await _context.Messages.ToListAsync());
            //return View(Json(await _context.Messages.ToListAsync()));
            //Message m = new Message("labas", GlobalVariables.CurrentUser);

            //            var req = WebRequest.Create(@"http://localhost:13082/api/Message/Index/");  
            //            var r = await req.GetResponseAsync().ConfigureAwait(false);
            //return Ok("zingsnis baigtas");
            //            var responseReader = new StreamReader(r.GetResponseStream());
            //            var responseData = await responseReader.ReadToEndAsync();

            //            var d = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(responseData);
            //            return View(d);

            //var jsonInString = JsonConvert.SerializeObject(_context.Messages);
            //var client = new HttpClient();
            //client.GetAsync();
            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.BaseAddress = new Uri("http://localhost:13082");
            //client.PostAsJsonAsync("api/Message/Index", _context.Messages.ToListAsync);
            //.ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode() );
            //client.PostAsJsonAsync<Message>();
            //return View("Index", Json(await _context.Messages.ToListAsync())); --> gives error because controller sends JsonResult while View is waiting for Generic.List
            //List<Message> m = await _context.Messages.ToListAsync();
            //List<Message> m2 = null;
            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri("http://localhost:13082/api/Message/Index")
            //};
            //var response = await client.GetAsync("");
            //var stream = await response.Content.ReadAsStreamAsync();
            //var serializer = new DataContractJsonSerializer(typeof(List<Message>));
            //m2 = (List<Message>)serializer.ReadObject(stream);

            //response = await client.GetAsync("");
            //m2 = await response.Content.ReadAsAsync<List<Message>>();



            return View(await _context.Messages.ToListAsync());
        }

        [HttpGet("exit", Name ="Exit")]
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
        [Route("create")]
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

        [HttpGet("update", Name ="Refresh")]
        public IActionResult Update()
        {
            // cia galim dirbti toliau
            // ar tai tikrai taip ir veiks???
            return RedirectToAction("index", "message", "api/message/index/");
        }

    }
}
