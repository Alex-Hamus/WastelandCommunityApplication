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
    public class GameService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public List<vmGameRules> GetAllGamesAndRules()
        {
            List<vmGameRules> games = new List<vmGameRules>();
            var dbgames = (from a in _context.Games
                        select a).ToList();

            if(dbgames != null)
            {
                foreach(var item in dbgames)
                {
                    vmGameRules game = new vmGameRules();
                    game.Game = item;

                    var rules = (from a in _context.Rules
                                 where a.GameId == item.GameId
                                 select a).ToList();
                    if(rules != null)
                    {
                        game.Rules = rules;
                    }
                    games.Add(game);
                }
            }


            return games;
        }

        public List<Rules> GetAllRules()
        {
            var list = (from a in _context.Rules
                        select a).ToList();

            return list;
        }

        public Game GetGameById(int GameId)
        {
            var game = (from a in _context.Games
                        where a.GameId == GameId
                        select a).FirstOrDefault();

            return game;
        }

        public void CreateGame(Game game)
        {
            if(game != null)
            {
                _context.Games.Add(game);
                _context.SaveChanges();
            }
        }

        public void DeleteGame(Game game)
        {
            if(game != null)
            {
                var rules = (from a in _context.Rules
                             where a.GameId == game.GameId
                             select a).ToList();

                if(rules != null)
                {

                    foreach(var item in rules)
                    {
                        DeleteRule(item);
                    }
                }

                _context.Games.Remove(game);
                _context.SaveChanges();
            }
        }

        public void UpdateGame(Game game)
        {

            if(game != null)
            {
                var Dbgame = GetGameById(game.GameId);

                if(Dbgame != null)
                {
                    Dbgame.GameName = game.GameName;
                    Dbgame.GameDescription = game.GameDescription;

                    _context.Entry(Dbgame).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
        }

        public Rules GetRuleById(int RuleId)
        {
            var rule = (from a in _context.Rules
                        where a.RulesId == RuleId
                        select a).FirstOrDefault();

            return rule;
        }

        public void CreateRule(Rules rule)
        {
            _context.Rules.Add(rule);
            _context.SaveChanges();
        }

        public void UpdateRule(Rules rule)
        {
            if(rule != null)
            {
                var dbRule = (from a in _context.Rules
                              where a.RulesId == rule.RulesId
                              select a).FirstOrDefault();

                dbRule.RuleDescription = rule.RuleDescription;

                _context.Entry(dbRule).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteRule(Rules rule)
        {
            if(rule != null)
            {
                _context.Rules.Remove(rule);
                _context.SaveChanges();
            }
        }
    }
}
