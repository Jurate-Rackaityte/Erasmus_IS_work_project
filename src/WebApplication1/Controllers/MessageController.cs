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
        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.ToListAsync());
        }

        //[HttpGet]
        //public IActionResult Exit()
        //{
        //    return RedirectToAction("Users/Index/");
        //}

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Text)
        {
            Message message = new WebApplication1.Models.Message(Text, GlobalVariables.CurrentUser);
           try { 
            // if (ModelState.IsValid)
            //{
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                GlobalVariables.Messages.Add(message);
                Console.WriteLine("HA, PARASIAU, NA IR KAS DABAR???");
                Console.WriteLine(GlobalVariables.CurrentUser);
            // }
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
            return View(_context.Messages);
        }

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
