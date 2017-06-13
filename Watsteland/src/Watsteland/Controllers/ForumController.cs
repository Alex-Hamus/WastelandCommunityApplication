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
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ForumService _forumService;

        public ForumController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ForumService forumService)
        {
            _context = context;
            _userManager = userManager;
            _forumService = forumService;

        }

        // GET: Forum
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            //var list = _context.Forums.ToList();
            var list = _forumService.GetAllForums();
            vmForumList vmfo = new vmForumList();
            vmfo.forums = list;
            vmfo.UserId = userId;
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            
            var data = _forumService.GetForumDataByUserId(user.Id);

            if(list != null)
            {
                foreach(var item in list)
                {
                    foreach(var item2 in data)
                    {
                        if(item.ForumId == item2.ForumId)
                        {

                            item.Read = true;
                        }
                    }
                }
            }

            vmfo.UserName = user.UserName;
            vmfo.Data = data;
            return View(vmfo);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateForumInfo(Forum forum)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            forum.UserId = user.Id;
            forum.UserName = user.UserName;

            _forumService.CreateForum(forum);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var forum = _forumService.GetForumById(Id);
            vmForum vmforum = new vmForum();
            vmforum.ForumId = forum.ForumId;
            vmforum.Title = forum.Title;
            vmforum.Description = forum.Description;
            return View(vmforum);
        }

        public ActionResult EditForumInfo(vmForum forum)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            Forum uforum = new Forum();
            uforum.ForumId = forum.ForumId;
            uforum.Title = forum.Title;
            uforum.Description = forum.Description;
            uforum.UserId = user.Id;
            uforum.UserName = user.UserName;

            _forumService.EditForum(uforum);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteForum(int Id)
        {
            var forum = _forumService.GetForumById(Id);
            _forumService.RemoveForum(forum);
            return RedirectToAction("Index");
        }
    }
}
