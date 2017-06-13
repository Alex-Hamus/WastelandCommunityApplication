using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Watsteland.Data;
using Watsteland.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Watsteland.Services;

namespace Watsteland.Controllers
{
    [Authorize]
    public class ThreadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ThreadService _threadService;
        private readonly ForumService _forumService;

        public ThreadsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ThreadService threadService, ForumService forumService)
        {
            _context = context;
            _userManager = userManager;
            _threadService = threadService;
            _forumService = forumService;
        }

        public ActionResult Index(int Id)
        {

            _threadService.UpdateFourmViewCount(Id);

            var forum = _forumService.GetForumById(Id);

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            var threads = _threadService.GetAllThreadsByForumId(Id);
            var data = _threadService.GetAllThreadDataByUserId(user.Id);

            if(threads != null)
            {
                foreach(var item in threads)
                {
                    foreach(var item2 in data)
                    {
                        if(item.ThreadId == item2.ThreadId)
                        {
                            item.Read = true;
                        }
                    }
                }
            }

            var check = _forumService.Check(Id, user.Id);
            if(check == true)
            {
                _forumService.AddForumData(Id, user.Id, user.UserName);
            }

            vmThreadList vmthread = new vmThreadList();
            vmthread.threads = threads;
            vmthread.UserId = user.Id;
            vmthread.UserName = user.UserName;
            vmthread.ForumId = Id;
            vmthread.baseForum = forum;

            return View(vmthread);
        }

        public ActionResult Create(int ForumId)
        {
            Thread thread = new Thread();
            thread.ForumId = ForumId;

            return View(thread);
        }

        public ActionResult CreateThreadInfo(Thread thread)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            
            thread.UserId = user.Id;
            thread.UserName = user.UserName;

            _threadService.CreateThread(thread);
            _threadService.NewThreadUpdate(thread.ForumId);
            return RedirectToAction("Index", new { Id = thread.ForumId });
        }

        public ActionResult Edit(int Id)
        {
            var the = _threadService.GetThreadById(Id);
            return View(the);
        }

        public ActionResult EditThreadInfo(Thread thread)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            thread.UserId = user.Id;
            thread.UserName = user.UserName;

            _threadService.EditThread(thread);
            return RedirectToAction("Index", new { Id = thread.ForumId });
        }

        public ActionResult DeleteThreadInfo(int Id)
        {
            var thread = _threadService.GetThreadById(Id);
            _threadService.DeleteThread(thread);
            return RedirectToAction("Index", new { Id = thread.ForumId });
        }
    }
}
