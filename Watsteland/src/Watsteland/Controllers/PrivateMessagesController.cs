using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Watsteland.Data;
using Watsteland.Models;
using Watsteland.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Watsteland.Controllers
{
    [Authorize]
    public class PrivateMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PrivateMessageService _privateMessageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrivateMessagesController(ApplicationDbContext context, PrivateMessageService privateMessageService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _privateMessageService = privateMessageService;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
           var userId = _userManager.GetUserId(HttpContext.User);
           var inbox = _privateMessageService.GetInbox(userId);

            return View(inbox);

        }

        public ActionResult SentBox()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var sentbox = _privateMessageService.GetOutBox(userId);

            return View(sentbox);
        }

        public ActionResult FindUsersToMessage()
        {
            var users = _privateMessageService.GetUsersList();
            return View(users);
        }

        public ActionResult Create(string UserId)
        {
            PrivateMessage message = new PrivateMessage();
            var user = _privateMessageService.GetUser(UserId);
            message.ToUserId = user.Id;
            message.ToUserName = user.UserName;

            return View(message);
        }

        public ActionResult CreateMessageInfo(PrivateMessage message)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            message.FromUserId = user.Id;
            message.FromUserName = user.UserName;

            _privateMessageService.CreateMessage(message);
            return RedirectToAction("Index");
        }

        public ActionResult ViewMessage(int MessageId)
        {
            var message = _privateMessageService.GetPrivateMessage(MessageId);
            return View(message);
        }

        public ActionResult ReadMessage(int MessageId)
        {
            var message = _privateMessageService.GetPrivateMessage(MessageId);
            _privateMessageService.MarkReadPrivateMessage(MessageId);

            return View(message);
        }

        public ActionResult ReadSentMessage(int MessageId)
        {
            var message = _privateMessageService.GetPrivateMessage(MessageId);

            return View(message);
        }

        public ActionResult DeleteMessage(int MessageId)
        {
            var message = _privateMessageService.GetPrivateMessage(MessageId);
            _privateMessageService.DeleteMessage(message);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteSentMessage(int MessageId)
        {
            var message = _privateMessageService.GetPrivateMessage(MessageId);
            _privateMessageService.DeleteMessage(message);

            return RedirectToAction("SentBox");
        }


        //[HttpPost]
        //public JsonResult AutoComplete(string term)
        //{
        //    var list = (from a in _context.Users
        //                where a.UserName.StartsWith(term)
        //                select new
        //                {
        //                    label = a.UserName,
        //                    val = a.Id
        //                }).ToList();

        //    return Json(list);
        //}

        //public IActionResult Languages(string term)
        //{
        //    var list = new[] { _privateMessageService.GetUserNames() };

        //    return Json(list.Where(x =>
        //        x.Contains(term, StringComparison.CurrentCultureIgnoreCase)).ToArray());
        //}

        //// GET: PrivateMessages
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.PrivateMessage.ToListAsync());
        //}

        //// GET: PrivateMessages/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var privateMessage = await _context.PrivateMessage.SingleOrDefaultAsync(m => m.MessageId == id);
        //    if (privateMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(privateMessage);
        //}

        //// GET: PrivateMessages/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PrivateMessages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MessageId,FromUserId,FromUserName,MessageDescription,MessageTitle,ToUserId,ToUserName,Unread")] PrivateMessage privateMessage)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(privateMessage);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(privateMessage);
        //}

        //// GET: PrivateMessages/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var privateMessage = await _context.PrivateMessage.SingleOrDefaultAsync(m => m.MessageId == id);
        //    if (privateMessage == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(privateMessage);
        //}

        //// POST: PrivateMessages/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("MessageId,FromUserId,FromUserName,MessageDescription,MessageTitle,ToUserId,ToUserName,Unread")] PrivateMessage privateMessage)
        //{
        //    if (id != privateMessage.MessageId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(privateMessage);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PrivateMessageExists(privateMessage.MessageId))
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
        //    return View(privateMessage);
        //}

        //// GET: PrivateMessages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var privateMessage = await _context.PrivateMessage.SingleOrDefaultAsync(m => m.MessageId == id);
        //    if (privateMessage == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(privateMessage);
        //}

        //// POST: PrivateMessages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var privateMessage = await _context.PrivateMessage.SingleOrDefaultAsync(m => m.MessageId == id);
        //    _context.PrivateMessage.Remove(privateMessage);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool PrivateMessageExists(int id)
        //{
        //    return _context.PrivateMessage.Any(e => e.MessageId == id);
        //}
    }
}
