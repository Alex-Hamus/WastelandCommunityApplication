using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Watsteland.Data;
using Watsteland.Models;

namespace Watsteland.Services
{
    public class PollService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PollService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<vmPollOptions> GetAllActivePolls()
        {
            var list = (from a in _context.Polls
                        where a.Active == true
                        select a).ToList();

            List<vmPollOptions> poll = new List<vmPollOptions>();

            if (list != null)
            {
                

                foreach (var item in list)
                {
                    vmPollOptions vmpoll = new vmPollOptions();
                    vmpoll.Poll = item;

                    var options = (from a in _context.PollOptions
                                   where a.PollId == item.PollId
                                   select a).ToList();

                    if(options != null)
                    {
                        List<PollOption> pollOptions = new List<PollOption>();

                        foreach (var op in options)
                        {
                            PollOption newop = new PollOption();
                            newop = op;
                            pollOptions.Add(newop);
                            vmpoll.PollOptions = pollOptions;
                        }
                        
                    }
                    
                    poll.Add(vmpoll);
                }
                
            }

            return poll;
        }

        public Poll GetPollById(int PollId)
        {
            var poll = (from a in _context.Polls
                        where a.PollId == PollId
                        select a).FirstOrDefault();

            return poll;
        }

        public PollOption GetPollOptionById(int PollOptionId)
        {
            var option = (from a in _context.PollOptions
                          where a.PollOptionsId == PollOptionId
                          select a).FirstOrDefault();

            return option;
        }

        public void CreatePoll(Poll poll)
        {
            if(poll != null)
            {
                poll.DateCreated = DateTime.Now;
                _context.Polls.Add(poll);
                _context.SaveChanges();
            }
        }

        public void UpdatePoll(Poll poll)
        {
            if(poll != null)
            {
                var dbpoll = (from a in _context.Polls
                              where a.PollId == poll.PollId
                              select a).FirstOrDefault();

                if(dbpoll != null)
                {
                    dbpoll.Description = poll.Description;
                    dbpoll.Question = poll.Question;

                    _context.Entry(dbpoll).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }

        }

        public void DeletePoll(Poll poll)
        {
            if(poll != null)
            {

                var dbpoll = (from a in _context.Polls
                              where a.PollId == poll.PollId
                              select a).FirstOrDefault();

                if(dbpoll != null)
                {
                    var pollops = (from a in _context.PollOptions
                                   where a.PollId == dbpoll.PollId
                                   select a).ToList();

                    if(pollops != null)
                    {

                        foreach(var item in pollops)
                        {
                            DeletePollOption(item);
                        }
                    }

                    _context.Polls.Remove(dbpoll);
                    _context.SaveChanges();
                }
            }
        }

        public void DeletePollOption(PollOption polloption)
        {
            if(polloption != null)
            {
                var pollop = (from a in _context.PollOptions
                              where a.PollOptionsId == polloption.PollOptionsId
                              select a).FirstOrDefault();

                if(pollop != null)
                {

                    _context.PollOptions.Remove(pollop);
                    _context.SaveChanges();
                }
            }
        }

        public void CreatePollOption(PollOption option)
        {
            if(option != null)
            {
                _context.PollOptions.Add(option);
                _context.SaveChanges();
            }
        }

        public void UpdatePollOption(PollOption option)
        {
            var op = (from a in _context.PollOptions
                      where a.PollOptionsId == option.PollOptionsId
                      select a).FirstOrDefault();

            if(op != null)
            {
                op.OptionDescription = option.OptionDescription;

                _context.Entry(op).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void DeactivatePoll(int PollId)
        {
            var poll = GetPollById(PollId);

            if(poll != null)
            {
                poll.Active = false;

                _context.Entry(poll).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Vote(int PollOptionId)
        {
            var option = GetPollOptionById(PollOptionId);
            
            if(option != null)
            {
                option.Votes = option.Votes += 1;

                _context.Entry(option).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public List<PollData> GetUserPollData(string UserId)
        {
            var list = (from a in _context.PollData
                        where a.UserId == UserId
                        select a).ToList();

            return list;

        }

        public void AddPollData(string UserName, string UserId, string PollOption, int PollId)
        {
            PollData data = new PollData();
            data.UserName = UserName;
            data.UserId = UserId;
            data.PollOption = PollOption;
            data.PollId = PollId;

            _context.PollData.Add(data);
            _context.SaveChanges();

        }

        public bool CheckIfVoted(int PollId, string UserId)
        {
            var data = (from a in _context.PollData
                        where a.PollId == PollId && a.UserId == UserId
                        select a).FirstOrDefault();

            if(data != null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public int GetNumberOfPolls()
        {

            var polls = (from a in _context.Polls
                         select a).ToList();

            return polls.Count;
        }

        public int GetNumberOfPeopleVoted()
        {

            var polls = (from a in _context.PollData
                         select a).ToList();

            return polls.Count;
        }


    }
}
