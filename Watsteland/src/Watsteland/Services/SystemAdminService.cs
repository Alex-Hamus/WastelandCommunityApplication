using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watsteland.Data;
using Watsteland.Models;

namespace Watsteland.Services
{
    public class SystemAdminService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ForumService _forumService;
        private readonly ThreadService _threadService;
        private readonly PostService _postService;
        private readonly CommunityUpdatesService _communityUpdatesService;
        private readonly UserManagementService _userManagementService;

        public SystemAdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ForumService forumService, ThreadService threadService, PostService postService, CommunityUpdatesService communityUpdatesService, UserManagementService userManagementService)
        {
            _context = context;
            _userManager = userManager;
            _forumService = forumService;
            _threadService = threadService;
            _postService = postService;
            _communityUpdatesService = communityUpdatesService;
            _userManagementService = userManagementService;

        }

        //public List<Forum> GetAllForums()
        //{
        //    var forums = _forumService.GetAllForums();
        //    return forums;
        //}

        //public List<Thread> GetAllThreads()
        //{
        //    var threads = _threadService.GetAllThreads();
        //    return threads;
        //}

        public List<Post> GetAllPosts()
        {
            var posts = _postService.GetAllPosts();
            return posts;
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var users = _context.Users.ToList();
            return users;
        }

        public IOrderedQueryable<CommunityUpdates> GetAllCommunityUpdates()
        {

            var updates = _communityUpdatesService.GetAllCommunityUpdates();
            return updates;
        }

        public List<UserInformation> UserInformationList()
        {

            var userlist = _context.UserInformation.ToList();
            return userlist;
        }

        public UserInformation GetUserInformationByUserId(string UserId)
        {
            var user = (from a in _context.UserInformation
                        where a.UserId == UserId
                        select a).FirstOrDefault();

            return user;
        }

        public void DeleteUser(string UserId)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

            }
        }

        public vmUser GetVmUser(string UserId)
        {
            vmUser user = new vmUser();
            List<vmRole> vmUserRoles = new List<vmRole>();
            var appuser = GetUserById(UserId);
            var roles = _userManagementService.GetAllRolesForUserById(UserId);
            var userinfo = GetUserInformationByUserId(UserId);

            if (roles != null)
            {
                foreach (var item in roles)
                {
                    vmRole role = new vmRole();
                    role = _userManagementService.GetRoleInfoById(item.RoleId);
                    vmUserRoles.Add(role);
                }
            }

            user.UserRoles = roles;
            user.UserName = userinfo.UserName;
            user.UserPassowrd = userinfo.Password;
            user.vmUserRoles = vmUserRoles;
            user.UserId = appuser.Id;

            return user;
        }

        public ApplicationUser GetUserById(string UserId)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            return user;
        }

        //public GetRoleForUser(string UserId)
        //{
        //    var user = (from a in _context.Users
        //                where a.Id == UserId
        //                select a).FirstOrDefault();

        //    var roles = _userManager.GetClaims(user);
        //    return roles;
        //}

    }
}
