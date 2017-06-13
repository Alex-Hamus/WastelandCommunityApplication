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
    public class CommunityUpdatesService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommunityUpdatesService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public void CreateCommunityUpdate(CommunityUpdates update )
        {
            if(update != null)
            {
                update.CreatedDate = DateTime.Now;

                _context.CommunityUpdates.Add(update);
                _context.SaveChanges();
                CreateUpdate("Added Community Update: " + update.UpdateTitle, update.UserId, update.UserName);
            }

        }

        public void DeleteCommunityUpdate(CommunityUpdates update)
        {
            if(update != null)
            {

                _context.CommunityUpdates.Remove(update);
                _context.SaveChanges();
            }
        }

        public CommunityUpdates GetCommunityUpdateById(int Id)
        {
            var update = (from a in _context.CommunityUpdates
                          where a.CommunityUpdateId == Id
                          select a).FirstOrDefault();

            return update;
        }

        public void UpdateCommunityUpdate(CommunityUpdates update)
        {

            var dbUpdate = GetCommunityUpdateById(update.CommunityUpdateId);
            dbUpdate.UdateInformation = update.UdateInformation;
            dbUpdate.UpdateTitle = update.UpdateTitle;
            _context.Entry(dbUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public IOrderedQueryable<CommunityUpdates> GetAllCommunityUpdates()
        {

            var updates = (from a in _context.CommunityUpdates
                           select a).OrderByDescending(x => x.CreatedDate);

            return updates;
        }

        public void CreateCommunityUpdateComment(CommunityUpdateComment comment)
        {
            if(comment != null)
            {

                comment.CreatedDate = DateTime.Now;
                _context.CommunityUpdateComments.Add(comment);
                _context.SaveChanges();
                CreateUpdate("Added Comment: " + comment.CommentTitle, comment.UserId, comment.UserName);
            }
        }

        public void DeleteCommunityUpdateComment(CommunityUpdateComment comment)
        {
            if(comment != null)
            {
                var dbComment = (from a in _context.CommunityUpdateComments
                                 where a.CommentId == comment.CommentId
                                 select a).FirstOrDefault();

                if(dbComment != null)
                {
                    _context.CommunityUpdateComments.Remove(dbComment);
                    _context.SaveChanges();
                }
            }
        }

        public CommunityUpdateComment GetCommunityUpdateCommentById(int CommentId)
        {
            var comment = (from a in _context.CommunityUpdateComments
                           where a.CommentId == CommentId
                           select a).FirstOrDefault();

            return comment;
        }

        public void UpdateCommunityUpdateComment(CommunityUpdateComment comment)
        {
            var dbComment = (from a in _context.CommunityUpdateComments
                             where a.CommentId == comment.CommentId
                             select a).FirstOrDefault();

            if(dbComment != null)
            {
                dbComment.CommentDescription = comment.CommentDescription;
                dbComment.CommentTitle = comment.CommentTitle;

                _context.Entry(dbComment).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void IncreaseNumberOfComments(int CommunityUpdateId)
        {
            var update = GetCommunityUpdateById(CommunityUpdateId);

            if(update != null)
            {

                update.NumberOfComments = update.NumberOfComments + 1;
                _context.Entry(update).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DecreaseNumberOfComments(int CommunityUpdateId)
        {
            var update = GetCommunityUpdateById(CommunityUpdateId);

            if (update != null)
            {

                update.NumberOfComments = update.NumberOfComments -1;
                _context.Entry(update).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public vmCommunityUpdateComment GetCommunityUpdatesByUpdateId(int CommunityUpdateId)
        {
            vmCommunityUpdateComment vmCommunityUpdateComment = new vmCommunityUpdateComment();
            var communityupdate = GetCommunityUpdateById(CommunityUpdateId);

            if(communityupdate != null)
            {
                var list = (from a in _context.CommunityUpdateComments
                            where a.CommunityUpdateId == communityupdate.CommunityUpdateId
                            select a).ToList();

                vmCommunityUpdateComment.CommunityUpdate = communityupdate;
                vmCommunityUpdateComment.Comments = list;
            }

            return vmCommunityUpdateComment;

        }

        public int GetNumberOfCommunityUpdates()
        {
            var updates = (from a in _context.CommunityUpdates
                           select a).ToList();

            return updates.Count;

        }

        public int GetNumberOfCommunityUpdateComments()
        {
            var updates = (from a in _context.CommunityUpdateComments
                           select a).ToList();

            return updates.Count;

        }

        public void CreateUpdate(string UpdateInformation, string UserId, string UserName)
        {
            Update update = new Update();
            update.CreatedDate = DateTime.Now;
            update.UpdateInformation = UpdateInformation;
            update.UserId = UserId;
            update.UserName = UserName;

            _context.Updates.Add(update);
            _context.SaveChanges();
        }

        public void RemoveUpdate(int UpdateId)
        {
            var update = (from a in _context.Updates
                          where a.UpdateId == UpdateId
                          select a).FirstOrDefault();

            if(update != null)
            {

                _context.Updates.Remove(update);
                _context.SaveChanges();
            }

        }

        public Update GetUpdateById(int UpdateId)
        {
            var update = (from a in _context.Updates
                          where a.UpdateId == UpdateId
                          select a).FirstOrDefault();

            return update;

        }

        public IOrderedQueryable<Update> GetAllUpdates()
        {
            var updates = (from a in _context.Updates
                           select a).OrderByDescending(x => x.CreatedDate);

            return updates;
        }
    }
}
