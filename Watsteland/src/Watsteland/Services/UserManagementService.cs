using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watsteland.Data;
using Watsteland.Models;

namespace Watsteland.Services
{
    public class UserManagementService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<ApplicationUser> GetUsersList()
        {
            var list = (from a in _context.Users
                        select a).ToList();

            foreach (var item in list)
            {
                var roles = _userManager.GetRolesAsync(item);
            }


            return list;
        }

        public void AddRoleToUser(string UserId, string Role)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(Role).Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = Role;
                    var re = _roleManager.CreateAsync(role);
                    re.Wait();
                    if (re.IsCompleted)
                    {
                        
                    }

                    _userManager.AddToRoleAsync(user, Role).Wait();
                }else
                {
                    CreateRole(Role);
                    _userManager.AddToRoleAsync(user, Role).Wait();
                }

                
            }
        }

        public void CreateRole(string Role)
        {
            if (!_roleManager.RoleExistsAsync(Role).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = Role;
                var re = _roleManager.CreateAsync(role);
                if (re.IsCompleted)
                {

                }
            }
        }

        public void RemoveRoleFromUser(string UserId, string Role)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            if (user != null)
            {

                _userManager.RemoveFromRoleAsync(user, Role).Wait();
            }
        }


        public List<IdentityRole> GetAllRoles()
        {
            var roles = (from a in _context.Roles
                         select a).ToList();

            return roles;
        }

        public List<IdentityUserRole<string>> GetAllRolesForUserById(string UserId)
        {
            var roles = (from a in _context.UserRoles
                         where a.UserId == UserId
                         select a).ToList();

            return roles;
        }

        public ApplicationUser GetUserById(string UserId)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            return user;
        }

        public string GetRoleById(string RoleId)
        {
            var rolename = (from a in _context.Roles
                            where a.Id == RoleId
                            select a.Name).FirstOrDefault();

            return rolename;
        }

        public vmRole GetRoleInfoById(string RoleId)
        {
            var role = (from a in _context.Roles
                        where a.Id == RoleId
                        select a).FirstOrDefault();

            vmRole vmrole = new vmRole();
            vmrole.RoleId = role.Id;
            vmrole.RoleName = role.Name;

            return vmrole;
        }

        public void UpdateUserInformation(ApplicationUser user)
        {
            var UPuser = (from a in _context.Users
                          where a.Id == user.Id
                          select a).FirstOrDefault();

            if (UPuser != null)
            {
                UPuser.Email = user.Email;
                UPuser.PhoneNumber = user.PhoneNumber;

                _context.Entry(UPuser).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public int GetNumberOfMembers()
        {

            var members = (from a in _context.Users
                           select a).ToList();

            return members.Count;
        }
       
      }
    }
