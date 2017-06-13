using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Watsteland.Data;
using Watsteland.Models;
using Microsoft.AspNetCore.Identity;
using Watsteland.Services;
using Microsoft.AspNetCore.Authorization;

namespace Watsteland.Controllers
{

    [Authorize(Roles = "SystemAdmin")]
    public class SystemAdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ForumService _forumService;
        private readonly ThreadService _threadService;
        private readonly PostService _postService;
        private readonly CommunityUpdatesService _communityUpdatesService;
        private readonly SystemAdminService _systemAdminService;
        private readonly UserManagementService _userManagementService;
        private readonly WellcomeMessageService _wellcomeMessageService;

        public SystemAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ForumService forumService, ThreadService threadService, PostService postService, CommunityUpdatesService communityUpdatesService, SystemAdminService systemAdminService, UserManagementService userManagementService, WellcomeMessageService wellcomeMessageService)
        {
            _context = context;
            _userManager = userManager;
            _forumService = forumService;
            _threadService = threadService;
            _postService = postService;
            _communityUpdatesService = communityUpdatesService;
            _systemAdminService = systemAdminService;
            _userManagementService = userManagementService;
            _wellcomeMessageService = wellcomeMessageService;

        }


        public IActionResult Index()
        {
            var users = _systemAdminService.GetAllUsers();
            return View(users);
        }

        public ActionResult PostsList()
        {
            var posts = _systemAdminService.GetAllPosts();
            return View(posts);
        }

        public ActionResult UserInformationList()
        {
            var list = _systemAdminService.UserInformationList();
            return View(list);
        }

        public ActionResult DeleteUser(string UserId)
        {
            _systemAdminService.DeleteUser(UserId);
            return RedirectToAction("Index");
        }

        public ActionResult AddCommunityUpdateView()
        {
            return View();
        }

        public ActionResult AddCommunityUpdate(CommunityUpdates update)
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            update.CreatedDate = DateTime.Now;
            update.UserId = user.Id;
            update.UserName = user.UserName;

            _communityUpdatesService.CreateCommunityUpdate(update);
            return RedirectToAction("Index");
        }

        public ActionResult CommunityUpdateList()
        {
            var updates = _communityUpdatesService.GetAllCommunityUpdates();

            return View(updates);

        }

        public ActionResult GetUserRoles(string UserId)
        {

            var user = _systemAdminService.GetVmUser(UserId);

            return View(user);
        }

        public ActionResult SaveUserRoles(vmUser user)
        {
            //_userManagementService.CreateRole(user.RoleName);
            _userManagementService.AddRoleToUser(user.UserId, user.RoleName);
            return RedirectToAction("GetUserRoles", new { UserId = user.UserId });
        }

        public ActionResult RemoveRole(string UserId, string Role)
        {
            var user = _userManagementService.GetUserById(UserId);

            if(user != null)
            {
                _userManagementService.RemoveRoleFromUser(user.Id, Role);
            }

            return RedirectToAction("GetUserRoles", new { UserId = user.Id });
        }

        public ActionResult CreateWellcomeMessageView()
        {
            return View();

        }

        public ActionResult CreateWellcomeMessageInfo(WellcomeMessage message)
        {
            if(message != null)
            {
                _wellcomeMessageService.CreateWellcomeMessage(message.WellcomeTitle, message.MessageDescription);   
            }

            return RedirectToAction("Index");
        }


    }
}