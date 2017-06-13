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
using Watsteland.Services;
using Microsoft.AspNetCore.Authorization;

namespace Watsteland.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PostService _postService;
        private readonly ForumService _forumService;
        private readonly ThreadService _threadService;
        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, PostService postService, ForumService forumService, ThreadService threadService)
        {
            _context = context;
            _userManager = userManager;
            _postService = postService;
            _forumService = forumService;
            _threadService = threadService;
        }

        public ActionResult Index(int ThreadId, int ForumId)
        {
            _postService.AddViewToThread(ThreadId);
            var forum = _forumService.GetForumById(ForumId);
            var thread = _threadService.GetThreadById(ThreadId);

            var userId = _userManager.GetUserId(HttpContext.User);

            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            var check = _threadService.Check(ForumId, ThreadId, user.Id);
            if(check == true)
            {
                _threadService.AddThreadData(ForumId, ThreadId, user.Id, user.UserName);
            }

            vmPostList vmpost = new vmPostList();
            var list = (from a in _context.Posts
                        where a.ThreadId == ThreadId
                        select a).ToList();

            vmpost.posts = list;
            vmpost.ThreadId = ThreadId;
            vmpost.ForumId = ForumId;
            vmpost.UserId = user.Id;
            vmpost.UserName = user.UserName;
            vmpost.BaseForum = forum;
            vmpost.BaseThread = thread;
            return View(vmpost);

        }

        public ActionResult Create(int ThreadId, int ForumId)
        {
            Post post = new Post();
            post.ThreadId = ThreadId;
            post.ForumId = ForumId;

            return View(post);
        }

        public ActionResult CreatePostInfo(Post post)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            post.UserId = user.Id;
            post.UserName = user.UserName;

            _postService.CreatePost(post);
            _postService.NewPostUpdate(post.ForumId, post.ThreadId);
            return RedirectToAction("Index", new { ThreadId = post.ThreadId, ForumId = post.ForumId} );
        }

        public ActionResult Edit(int PostId)
        {
            var post = _postService.GetPostById(PostId);
            return View(post);
        }

        public ActionResult EditPostInfo(Post post)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            post.UserId = user.Id;
            post.UserName = user.UserName;

            _postService.EditPost(post);
            return RedirectToAction("Index", new { ThreadId = post.ThreadId, ForumId = post.ForumId });
        }

        public ActionResult DeletePost(int PostId)
        {
            var post = _postService.GetPostById(PostId);
            _postService.DeletePost(post);

            return RedirectToAction("Index", new { ThreadId = post.ThreadId, ForumId = post.ForumId });
        }
    }
}
