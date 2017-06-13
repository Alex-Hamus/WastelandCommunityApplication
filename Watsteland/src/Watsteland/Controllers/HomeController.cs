using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Watsteland.Services;
using Watsteland.Data;
using Microsoft.AspNetCore.Authorization;
using Watsteland.Models;
using Microsoft.AspNetCore.Identity;

namespace Watsteland.Controllers
{

    public class HomeController : Controller
    {

        private readonly CommunityUpdatesService _communityUpdatesService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GameService _gameService;
        private readonly PollService _pollService;
        private readonly ForumService _forumService;
        private readonly ThreadService _threadService;
        private readonly PostService _postService;
        private readonly UserManagementService _userManagementService;
        private readonly WellcomeMessageService _wellcomeMessageService;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, CommunityUpdatesService communityUpdatesService, GameService gameService, PollService pollService, ForumService forumService, ThreadService threadService, PostService postService, UserManagementService userManagementService, WellcomeMessageService wellcomeMessageService)
        {
            _context = context;
            _communityUpdatesService = communityUpdatesService;
            _userManager = userManager;
            _gameService = gameService;
            _pollService = pollService;
            _forumService = forumService;
            _threadService = threadService;
            _postService = postService;
            _userManagementService = userManagementService;
            _wellcomeMessageService = wellcomeMessageService;

        }


        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();
            

            vmIndex index = new vmIndex();
            var updates = _communityUpdatesService.GetAllCommunityUpdates();
            var polls = _pollService.GetAllActivePolls();

            if(polls != null)
            {
                foreach(var item in polls)
                {
                    if(user != null)
                    {
                        index.UserId = user.Id;
                        var check = _pollService.CheckIfVoted(item.Poll.PollId, user.Id);
                        if (check == true)
                        {
                            item.AreadyVoted = true;
                        }
                        else
                        {
                            item.AreadyVoted = false;
                        }
                    }
                }
            }

            index.Polls = polls;
            index.CommmunityUpdates = updates;

            if(user != null)
            {
                index.PollData = _pollService.GetUserPollData(user.Id);
            }

            vmStats stats = new vmStats();
            var forums = _forumService.GetNumberOfForums();
            var threads = _threadService.GetNumberOfThreads();
            var posts = _postService.GetNumberOfPosts();
            var npolls = _pollService.GetNumberOfPolls();
            var vpolls = _pollService.GetNumberOfPeopleVoted();
            var cupdates = _communityUpdatesService.GetNumberOfCommunityUpdates();
            var comments = _communityUpdatesService.GetNumberOfCommunityUpdateComments();
            var members = _userManagementService.GetNumberOfMembers();

            stats.NumberOfForums = forums;
            stats.NumberOfThreads = threads;
            stats.NumberOfPosts = posts;
            stats.NumberOfPolls = npolls;
            stats.NumberOfCommunityUpdateComments = comments;
            stats.NumberOfCommunityUpdates = cupdates;
            stats.NumberOfPeopleVoted = vpolls;
            stats.NumberOfCommunityMembers = members;

            index.Stats = stats;

            var Cupdates = _communityUpdatesService.GetAllUpdates();
            index.Updates = Cupdates;

            var wellcomeMessages = _wellcomeMessageService.GetAllWellcomeMessages();
            if(wellcomeMessages != null)
            {
                index.WellcomeMessages = wellcomeMessages;
            }


            return View(index);
        }

        public ActionResult AddCommunityUpdateView()
        {
            return View();
        }

        public ActionResult AddCommunityUpdate(CommunityUpdates update)
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            update.CreatedDate = DateTime.Now;
            update.UserId = user.Id;
            update.UserName = user.UserName;

            _communityUpdatesService.CreateCommunityUpdate(update);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SystemAdmin")]
        public ActionResult DeleteCommunityUpdate(int CommunityUpdateId)
        {
            var update = _communityUpdatesService.GetCommunityUpdateById(CommunityUpdateId);
            _communityUpdatesService.DeleteCommunityUpdate(update);

            return RedirectToAction("Index");

        }

        public ActionResult EditCommunityUpdateView(int CommunityUpdateId)
        {
            var update = _communityUpdatesService.GetCommunityUpdateById(CommunityUpdateId);

            return View(update);

        }

        public ActionResult UpdateCommunityInfo(CommunityUpdates update)
        {
            if (update != null)
            {
                _communityUpdatesService.UpdateCommunityUpdate(update);

            }

            return RedirectToAction("Index");

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Rules()
        {
            var games = _gameService.GetAllGamesAndRules();

            return View(games);
        }

        public ActionResult CreateGameView()
        {
            return View();
        }

        public ActionResult CreateRuleView(int GameId)
        {
            Rules rule = new Rules();
            rule.GameId = GameId;
            return View(rule);
        }

        public ActionResult CreateRule(Rules rule)
        {
            if(rule != null)
            {
                _gameService.CreateRule(rule);

            }

            return RedirectToAction("Rules");
        }

        public ActionResult UpdateRuleView(int RuleId)
        {
            var rule = _gameService.GetRuleById(RuleId);

            return View(rule);
        }

        public ActionResult UpdateRule(Rules rule)
        {
            if(rule != null)
            {
                _gameService.UpdateRule(rule);
            }

            return RedirectToAction("Rules");
        }

        public ActionResult DeleteRule(int RuleId)
        {
            var rule = _gameService.GetRuleById(RuleId);
            _gameService.DeleteRule(rule);

            return RedirectToAction("Rules");
        }

        public ActionResult CreateGame(Game game)
        {
            if(game != null)
            {
                _gameService.CreateGame(game);
            }

            return RedirectToAction("Rules");
        }

        public ActionResult DeleteGame(int GameId)
        {
            var game = _gameService.GetGameById(GameId);

            if(game != null)
            {

                _gameService.DeleteGame(game);
            }

            return RedirectToAction("Rules");
        }

        public IActionResult Streams()
        {
            return View();
        }

        public ActionResult CreatePollView()
        {

            return View();
        }

        public ActionResult CreatePollInfo(Poll poll)
        {
            if(poll != null)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = (from a in _context.Users
                            where a.Id == userId
                            select a).FirstOrDefault();

                poll.DateCreated = DateTime.Now;
                poll.Active = true;
                poll.CreatedByUserId = user.Id;
                poll.CreatedByUserName = user.UserName;
                _pollService.CreatePoll(poll);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditPollView(int PollId)
        {
            var poll = _pollService.GetPollById(PollId);

            return View(poll);
        }

        public ActionResult EditPollInfo(Poll poll)
        {
            if(poll != null)
            {
                _pollService.UpdatePoll(poll);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreatePollOptionView(int PollId)
        {
            PollOption option = new PollOption();
            option.PollId = PollId;
            return View(option);
        }

        public ActionResult CreatePollOptionsInfo(PollOption pollOption)
        {
            if(pollOption != null)
            {
                _pollService.CreatePollOption(pollOption);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditPollOptionView(int PollOptionId)
        {
            var option = _pollService.GetPollOptionById(PollOptionId);

            return View(option);

        }

        public ActionResult EditPollOptionInfo(PollOption option)
        {
            if(option != null)
            {
                _pollService.UpdatePollOption(option);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeactivatePoll(int PollId)
        {
            _pollService.DeactivatePoll(PollId);

            return RedirectToAction("Index");
        }

        public ActionResult DeletePollOption(int PollOptionId)
        {
            var option = _pollService.GetPollOptionById(PollOptionId);

            if(option != null)
            {

                _pollService.DeletePollOption(option);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Vote(int PollOptionId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            
            var polloption = _pollService.GetPollOptionById(PollOptionId);

            if(polloption != null)
            {
                _pollService.Vote(PollOptionId);
                _pollService.AddPollData(user.UserName, user.Id, polloption.OptionDescription, polloption.PollId);

            }

            return RedirectToAction("Index");
        }

        public ActionResult EditGameView(int GameId)
        {
            var game = _gameService.GetGameById(GameId);
            return View(game);
        }

        public ActionResult ViewCommunityUpdateComments(int CommunityUpdateId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = (from a in _context.Users
                        where a.Id == userId
                        select a).FirstOrDefault();

            var list = _communityUpdatesService.GetCommunityUpdatesByUpdateId(CommunityUpdateId);
            if(user != null)
            {
                list.UserId = user.Id;
                list.UserName = user.UserName;
            }

            return View(list);
        }

        public ActionResult CreateCommunityUpdateView(int CommunityUpdateId)
        {
            CommunityUpdateComment comment = new CommunityUpdateComment();
            comment.CommunityUpdateId = CommunityUpdateId;

            return View(comment);
        }

        public ActionResult CreateCommunityUpdateComment(CommunityUpdateComment comment)
        {
            if(comment != null)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = (from a in _context.Users
                            where a.Id == userId
                            select a).FirstOrDefault();

                comment.UserId = user.Id;
                comment.UserName = user.UserName;

                _communityUpdatesService.CreateCommunityUpdateComment(comment);
                _communityUpdatesService.IncreaseNumberOfComments(comment.CommunityUpdateId);
            }

            return RedirectToAction("ViewCommunityUpdateComments", new { CommunityUpdateId = comment.CommunityUpdateId });

        }

        public ActionResult UpdateCommunityCommentView(int CommentId)
        {
            var comment = _communityUpdatesService.GetCommunityUpdateCommentById(CommentId);

            return View(comment);
        }

        public ActionResult UpdateCommunityComment(CommunityUpdateComment comment)
        {
            if(comment != null)
            {
                _communityUpdatesService.UpdateCommunityUpdateComment(comment);
            }

            return RedirectToAction("ViewCommunityUpdateComments", new { CommunityUpdateId = comment.CommunityUpdateId });
        }

        public ActionResult DeleteCommunityComment(int CommentId)
        {
            var comment = _communityUpdatesService.GetCommunityUpdateCommentById(CommentId);
            _communityUpdatesService.DeleteCommunityUpdateComment(comment);
            _communityUpdatesService.DecreaseNumberOfComments(comment.CommunityUpdateId);

            return RedirectToAction("ViewCommunityUpdateComments", new { CommunityUpdateId = comment.CommunityUpdateId });
        }

        public ActionResult UpdateCommunityMessageView(int CommunityMessageId)
        {
            var communityMessage = _wellcomeMessageService.GetWellcomeMessageById(CommunityMessageId);

            return View(communityMessage);
        }

        public ActionResult UpdateCommunityMessageInfo(WellcomeMessage message)
        {
            if(message != null)
            {
                _wellcomeMessageService.UpdateWellcomeMessage(message.WellcomeMessageId,message.WellcomeTitle, message.MessageDescription);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteCommunityMessage(int WellcomeMessageId)
        {
            _wellcomeMessageService.DeleteWellcomeMessage(WellcomeMessageId);

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
