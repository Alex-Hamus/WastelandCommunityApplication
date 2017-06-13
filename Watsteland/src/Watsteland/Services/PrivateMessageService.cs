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
    public class PrivateMessageService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrivateMessageService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public InboxList GetInbox(string UserId)
        {
            var user = GetUser(UserId);

            var messages = (from a in _context.PrivateMessages
                            where a.ToUserId == UserId
                            select a).ToList();

            InboxList inbox = new InboxList();
            inbox.messages = messages;
            inbox.UserId = user.Id;
            inbox.UserName = user.UserName;

            return inbox;


        }

        public InboxList GetOutBox(string UserId)
        {
            var user = GetUser(UserId);

            var messages = (from a in _context.PrivateMessages
                            where a.FromUserId == UserId
                            select a).ToList();

            InboxList inbox = new InboxList();
            inbox.messages = messages;
            inbox.UserId = user.Id;
            inbox.UserName = user.UserName;

            return inbox;


        }

        public ApplicationUser GetUser(string UserId)
        {
            var user = (from a in _context.Users
                        where a.Id == UserId
                        select a).FirstOrDefault();

            return user;
        }

        public void CreateMessage(PrivateMessage message)
        {
            if(message != null)
            {
                message.SentDate = DateTime.Now;
                _context.PrivateMessages.Add(message);
                _context.SaveChanges();
            }
        }

        public void DeleteMessage(PrivateMessage message)
        {
            if(message != null)
            {
                _context.PrivateMessages.Remove(message);
                _context.SaveChanges();
            }

        }

        public List<ApplicationUser> GetUsersList()
        {
            var list = (from a in _context.Users
                        select a).ToList();

            return list;
        }

        public PrivateMessage GetPrivateMessage(int MessageId)
        {

            var message = (from a in _context.PrivateMessages
                           where a.MessageId == MessageId
                           select a).FirstOrDefault();

            return message;
        }

        public void MarkReadPrivateMessage(int MessageId)
        {
            var message = (from a in _context.PrivateMessages
                           where a.MessageId == MessageId
                           select a).FirstOrDefault();

            if (message != null)
            {
                message.Read = true;
                _context.Entry(message).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }


    }
}
