using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watsteland.Data;
using Watsteland.Models;

namespace Watsteland.Services
{
    public class ForumService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CommunityUpdatesService _updates;

        public ForumService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, CommunityUpdatesService updates)
        {
            _context = context;
            _userManager = userManager;
            _updates = updates;

        }

        public void CreateForum(Forum forum)
        {

            forum.DateCreated = DateTime.Now;

            if (forum != null)
            {
                _updates.CreateUpdate("Created Forum: " + forum.Title, forum.UserId, forum.UserName);

                _context.Forums.Add(forum);
                _context.SaveChanges();
            }
        }

        public void EditForum(Forum forum)
        {
            if (forum != null)
            {
                _updates.CreateUpdate("Updated Forum: " + forum.Title, forum.UserId, forum.UserName);

                //_context.Forum.Update(forum);
                var dbForum = GetForumById(forum.ForumId);
                dbForum.Title = forum.Title;
                dbForum.Description = forum.Description;
                _context.Entry(dbForum).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void RemoveForum(Forum forum)
        {
            if (forum != null)
            {
                var threads = (from a in _context.Threads
                               where a.ForumId == forum.ForumId
                               select a).ToList();

                if (threads != null)
                {
                    foreach (var item in threads)
                    {
                        var posts = (from a in _context.Posts
                                     where a.ForumId == item.ForumId
                                     select a).ToList();

                        if(posts != null)
                        {
                            foreach(var item2 in posts)
                            {
                                _context.Posts.Remove(item2);
                                _context.SaveChanges();
                            }
                        }
                        _context.Threads.Remove(item);
                        _context.SaveChanges();
                    }
                }

                _context.Forums.Remove(forum);
                _context.SaveChanges();
            }
        }

        public Forum GetForumById(int ForumId)
        {
            var forum = (from a in _context.Forums
                         where a.ForumId == ForumId
                         select a).FirstOrDefault();

            return forum;
        }

        public int GetNumberOfForums()
        {
            var forums = (from a in _context.Forums
                          select a).ToList();

            return forums.Count;
        }

        public List<vmForum> GetAllForums()
        {
            var list = _context.Forums.ToList();
            List<vmForum> vmlist = new List<vmForum>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    vmForum forum = new vmForum();
                    forum.DateCreated = item.DateCreated;
                    forum.Description = item.Description;
                    forum.ForumId = item.ForumId;
                    forum.NumberOfThreads = item.NumberOfThreads;
                    forum.Title = item.Title;
                    forum.UserId = item.UserId;
                    forum.UserName = item.UserName;
                    forum.Views = item.Views;
                    vmlist.Add(forum);
                }
            }

            return vmlist;
        }

        public void AddForumData(int ForumId, string UserId, string UserName)
        {
            ForumData data = new ForumData();
            data.ForumId = ForumId;
            data.UserId = UserId;
            data.UserName = UserName;

            _context.ForumData.Add(data);
            _context.SaveChanges();

        }

        public void DeleteFourmData(int ForumDataId)
        {
            var data = (from a in _context.ForumData
                        where a.ForumDataId == ForumDataId
                        select a).FirstOrDefault();

            if(data != null)
            {
                _context.ForumData.Remove(data);
                _context.SaveChanges();
            }

        }

        public List<ForumData> GetForumDataByUserId(string UserId)
        {
            var list = (from a in _context.ForumData
                        where a.UserId == UserId
                        select a).ToList();

            return list;

        }

        public List<ForumData> GetForumDataByForumId(int ForumId)
        {
            var forums = (from a in _context.ForumData
                          where a.ForumId == ForumId
                          select a).ToList();

            return forums;
        }

        public bool Check(int ForumId, string UserId)
        {
            var data = (from a in _context.ForumData
                        where a.ForumId == ForumId && a.UserId == UserId
                        select a).ToList();

            if(data.Count == 0)
            {
                return true;
            }else
            {
                return false;
            }


        }
    }
}
