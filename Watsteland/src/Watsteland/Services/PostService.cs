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
    public class PostService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CommunityUpdatesService _updates;
        private readonly ForumService _forumService;
        private readonly ThreadService _threadService;

        public PostService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, CommunityUpdatesService updates, ForumService forumService, ThreadService threadService)
        {
            _context = context;
            _userManager = userManager;
            _updates = updates;
            _forumService = forumService;
            _threadService = threadService;
        }

        public Post GetPostById(int Id)
        {
            var post = (from a in _context.Posts
                        where a.PostId == Id
                        select a).FirstOrDefault();

            return post;
        }

        public void EditPost(Post post)
        {
            if (post != null)
            {
                var forum = _forumService.GetForumById(post.ForumId);
                var thread = _threadService.GetThreadById(post.ThreadId);

                _updates.CreateUpdate("Updated Post: " + post.Title + " in "+ thread.Title+ " in "+ forum.Title, post.UserId, post.UserName);

                var dbpost = GetPostById(post.PostId);
                dbpost.Title = post.Title;
                dbpost.Description = post.Description;
                _context.Entry(dbpost).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void CreatePost(Post post)
        {

            if (post != null)
            {
                var forum = _forumService.GetForumById(post.ForumId);
                var thread = _threadService.GetThreadById(post.ThreadId);

                _updates.CreateUpdate("Created Post: " + post.Title + " in " + thread.Title + " in " + forum.Title, post.UserId, post.UserName);

                post.DateCreated = DateTime.Now;

                AddPostToThreadCount(post.ThreadId);
                _context.Posts.Add(post);
                _context.SaveChanges();
            }
        }

        public void DeletePost(Post post)
        {
            if (post != null)
            {
                RemovePostToThreadCount(post.ThreadId);
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }

        public void AddPostToThreadCount(int Id)
        {
            var thread = (from a in _context.Threads
                          where a.ThreadId == Id
                          select a).FirstOrDefault();

            if (thread != null)
            {
                thread.NumberOfPosts = thread.NumberOfPosts + 1;
                _context.Threads.Update(thread);
                _context.SaveChanges();
            }
        }

        public void RemovePostToThreadCount(int Id)
        {
            var thread = (from a in _context.Threads
                          where a.ThreadId == Id
                          select a).FirstOrDefault();

            if (thread != null)
            {
                thread.NumberOfPosts = thread.NumberOfPosts - 1;
                _context.Threads.Update(thread);
                _context.SaveChanges();
            }
        }

        public void AddViewToThread(int ThreadId)
        {
            var thread = (from a in _context.Threads
                          where a.ThreadId == ThreadId
                          select a).FirstOrDefault();

            thread.Views = thread.Views + 1;
            _context.Entry(thread).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public List<Post> GetAllPosts()
        {
            var posts = _context.Posts.ToList();
            return posts;
        }

        public int GetNumberOfPosts()
        {
            var posts = (from a in _context.Posts
                         select a).ToList();

            return posts.Count;
        }

        public void NewPostUpdate(int ForumId, int ThreadId)
        {
            var forums = _forumService.GetForumDataByForumId(ForumId);

            if (forums != null)
            {
                foreach (var item in forums)
                {
                    _forumService.DeleteFourmData(item.ForumDataId);
                }
            }

            var threads = _threadService.GetThreadDataByThreadId(ThreadId);

            if(threads != null)
            {
                foreach(var item in threads)
                {
                    _threadService.DeleteThreadData(item.ThreadDataId);
                }
            }
        }
    }
}
