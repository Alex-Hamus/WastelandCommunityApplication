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
    public class WellcomeMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WellcomeMessageService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public WellcomeMessage GetWellcomeMessageById(int WellcomeMessageId)
        {
            var wellcomemessage = (from a in _context.WellcomeMessages
                                   where a.WellcomeMessageId == WellcomeMessageId
                                   select a).FirstOrDefault();

            return wellcomemessage;

        }

        public List<WellcomeMessage> GetAllWellcomeMessages()
        {
            var list = (from a in _context.WellcomeMessages
                        select a).ToList();

            return list;
        }

        public void CreateWellcomeMessage(string WellcomeTitle, string WellcomeMessage)
        {
            WellcomeMessage message = new WellcomeMessage();
            message.WellcomeTitle = WellcomeTitle;
            message.MessageDescription = WellcomeMessage;

            _context.WellcomeMessages.Add(message);
            _context.SaveChanges();
        }

        public void UpdateWellcomeMessage(int WellcomeMessageId, string WellcomeTitle, string WellcomeMessage)
        {
            var dbmessage = GetWellcomeMessageById(WellcomeMessageId);

            if(dbmessage != null)
            {

                dbmessage.WellcomeTitle = WellcomeTitle;
                dbmessage.MessageDescription = WellcomeMessage;

                _context.Entry(dbmessage).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void DeleteWellcomeMessage(int WellcomeMessageId)
        {
            var message = GetWellcomeMessageById(WellcomeMessageId);

            if(message != null)
            {

                _context.WellcomeMessages.Remove(message);
                _context.SaveChanges();
            }

        }
    }
}
