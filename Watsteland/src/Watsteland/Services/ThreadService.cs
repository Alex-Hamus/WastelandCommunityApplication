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
    public class ThreadService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CommunityUpdatesService _updates;
        private readonly ForumService _forumService;

        public ThreadService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, CommunityUpdatesService updates, ForumService forumService)
        {
            _context = context;
            _userManager = userManager;
            _updates = updates;
            _forumService = forumService;
        }


        public Thread GetThreadById(int Id)
        {
            var thread = (from a in _context.Threads
                          where a.ThreadId == Id
                          select a).FirstOrDefault();

            return thread;
        }

        public void CreateThread(Thread thread)
        {

            if (thread != null)
            {
                var forum = _forumService.GetForumById(thread.ForumId);
                _updates.CreateUpdate("Created Thread: " + thread.Title+ " in " + forum.Title, thread.UserId, thread.UserName);

                AddForumThreadCount(thread.ForumId);
                thread.DateCreated = DateTime.Now;
                _context.Threads.Add(thread);
                _context.SaveChanges();
            }
        }

        public void EditThread(Thread thread)
        {
            if (thread != null)
            {
                var forum = _forumService.GetForumById(thread.ForumId);
                _updates.CreateUpdate("Updated Thread: " + thread.Title + " in " + forum.Title, thread.UserId, thread.UserName);

                var dbthread = GetThreadById(thread.ThreadId);
                dbthread.Title = thread.Title;
                dbthread.Description = thread.Description;
                _context.Entry(dbthread).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteThread(Thread thread)
        {
            if (thread != null)
            {
                DecreaseForumThreadCount(thread.ForumId);

                var posts = (from a in _context.Posts
                             where a.ThreadId == thread.ThreadId
                             select a).ToList();

                if(posts != null)
                {
                    foreach(var item in posts)
                    {
                        _context.Posts.Remove(item);
                        _context.SaveChanges();
                    }
                }

                _context.Threads.Remove(thread);
                _context.SaveChanges();
            }
        }

        public void AddForumThreadCount(int Id)
        {
            var fourm = (from a in _context.Forums
                         where a.ForumId == Id
                         select a).FirstOrDefault();

            if (fourm != null)
            {
                fourm.NumberOfThreads = fourm.NumberOfThreads + 1;
                _context.Forums.Update(fourm);
                _context.SaveChanges();
            }
        }

        public void DecreaseForumThreadCount(int Id)
        {
            var fourm = (from a in _context.Forums
                         where a.ForumId == Id
                         select a).FirstOrDefault();

            if (fourm != null)
            {
                fourm.NumberOfThreads = fourm.NumberOfThreads - 1;
                _context.Forums.Update(fourm);
                _context.SaveChanges();
            }
        }

        public void UpdateFourmViewCount(int Id)
        {
            var fourm = (from a in _context.Forums
                         where a.ForumId == Id
                         select a).FirstOrDefault();

            if (fourm != null)
            {
                fourm.Views = fourm.Views + 1;
                _context.Forums.Update(fourm);
                _context.SaveChanges();
            }
        }

        public List<vmThread> GetAllThreadsByForumId(int ForumId)
        {
            var list = (from a in _context.Threads
                        where a.ForumId == ForumId
                        select a).ToList();

            List<vmThread> threads = new List<vmThread>();
            if(list != null)
            {
                foreach(var item in list)
                {
                    vmThread thread = new vmThread();
                    thread.DateCreated = item.DateCreated;
                    thread.Description = item.Description;
                    thread.ForumId = item.ForumId;
                    thread.NumberOfPosts = item.NumberOfPosts;
                    thread.ThreadId = item.ThreadId;
                    thread.Title = item.Title;
                    thread.UserId = item.UserId;
                    thread.UserName = item.UserName;
                    thread.Views = item.Views;

                    threads.Add(thread);
                }
            }
            return threads;
        }

        public int GetNumberOfThreads()
        {
            var threads = (from a in _context.Threads
                           select a).ToList();

            return threads.Count;
        }

        public void AddThreadData(int ForumId, int ThreadId, string UserId, string UserName)
        {
            ThreadData data = new ThreadData();
            data.FourmId = ForumId;
            data.ThreadId = ThreadId;
            data.UserId = UserId;
            data.UserName = UserName;

            _context.ThreadData.Add(data);
            _context.SaveChanges();
        }

        public void DeleteThreadData(int ThreadDataId)
        {
            var data = (from a in _context.ThreadData
                        where a.ThreadDataId == ThreadDataId
                        select a).FirstOrDefault();

            if(data != null)
            {

                _context.ThreadData.Remove(data);
                _context.SaveChanges();
            }

        }

        public List<ThreadData> GetAllThreadDataByUserId(string UserId)
        {
            var list = (from a in _context.ThreadData
                        where a.UserId == UserId
                        select a).ToList();

            return list;
        }

        public bool Check(int ForumId, int ThreadId, string UserId)
        {
            var data = (from a in _context.ThreadData
                        where a.FourmId == ForumId && a.UserId == UserId && a.ThreadId == ThreadId
                        select a).ToList();

            if (data.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public List<ThreadData> GetThreadDataByThreadId(int ThreadId)
        {
            var threads = (from a in _context.ThreadData
                           where a.ThreadId == ThreadId
                           select a).ToList();

            return threads;
        }

        public void NewThreadUpdate(int ForumId)
        {
            var forums = _forumService.GetForumDataByForumId(ForumId);

            if(forums != null)
            {
                foreach(var item in forums)
                {
                    _forumService.DeleteFourmData(item.ForumDataId);
                }
            }
        }

    }
}
