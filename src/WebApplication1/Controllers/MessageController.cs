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
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessageController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Messages
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        [HttpGet("exit")]
        public IActionResult Exit()
        {
            GlobalVariables.CurrentUser = "";
            return RedirectToAction("index", "user", "api/user/index/");
        }

        //// GET: Messages/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages
        //        .SingleOrDefaultAsync(m => m.id == id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(message);
        //}

        //// GET: Messages/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

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

        [HttpGet("update")]
        public IActionResult Update()
        {
            // cia galim dirbti toliau
            // ar tai tikrai taip ir veiks???
            return RedirectToAction("index", "message", "api/message/index/");
        }

        //[HttpGet]
        //public IActionResult Login([FromBody][Bind("Username,Password")] User user)
        //{
        //    //Boolean correct = false;
        //    User temp = _context.Users.FirstOrDefault(u => u.Username == user.Username);
        //    try
        //    {
        //        if (temp != null && temp.Password == user.Password)
        //        {
        //            //correct = true;
        //            GlobalVariables.CurrentUser = user.Username;
        //            return RedirectToAction("Index");
        //        }
                
        //    }
        //    catch (DbUpdateException /* ex */)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.
        //        ModelState.AddModelError("", "Unable to save changes. " +
        //            "Try again, and if the problem persists " +
        //            "see your system administrator.");
        //    }
        //    //later we'll need to change this into a redirection.
        //    //so that if everything is allright, then we'll redirect
        //    //the user into the chatroom. 
        //    //If something's wrong, we'll redirect the user again to 
        //    //the log in page with an error message
        //    // later: redirect to messages
        //    return RedirectToAction("Message/Index");
        //}

        //// POST: Messages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Username,Text")] Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(message);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(message);
        //}

        //// GET: Messages/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages.SingleOrDefaultAsync(m => m.id == id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(message);
        //}

        //// POST: Messages/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Username,Text,id")] Message message)
        //{
        //    if (id != message.id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(message);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MessageExists(message.id))
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
        //    return View(message);
        //}

        //// GET: Messages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages
        //        .SingleOrDefaultAsync(m => m.id == id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(message);
        //}

        //// POST: Messages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var message = await _context.Messages.SingleOrDefaultAsync(m => m.id == id);
        //    _context.Messages.Remove(message);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool MessageExists(int id)
        //{
        //    return _context.Messages.Any(e => e.id == id);
        //}
    }
}
