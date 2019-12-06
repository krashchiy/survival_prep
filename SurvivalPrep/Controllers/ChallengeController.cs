using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FuzzySharp;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivalPrep.DBModels;
using SurvivalPrep.Models;

namespace SurvivalPrep.Controllers
{
    [Authorize]
    public class ChallengeController : Controller
    {
        private readonly PrepContext _context;
        private readonly ApplicationUser _currentUser;
        private static List<int> Visited { get; set; }
        private static int MinQuestionId { get; set; }
        private static int MaxQuestionId { get; set; }
        private static Random Rand => new Random();
        private static int Count { get; set; }
        private static int CurrentScore { get; set; }
    

        public ChallengeController(PrepContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor accessor)
        {
            var usr = User ?? accessor.HttpContext.User;
            var userId = userManager.GetUserId(usr);
            _currentUser = context.Users.Find(userId);
            _context = context;
            _context.Database.SetCommandTimeout(int.MaxValue);
        }

        public async Task<IActionResult> Index()
        {
            MinQuestionId = _context.Questions.Min(v => v.QuestionId);
            MaxQuestionId = _context.Questions.Max(v => v.QuestionId);
            Visited = new List<int>();
            CurrentScore = 0;
            Count = 1;

            ViewBag.Money = _currentUser.Money;
            ViewBag.CurrentScore = CurrentScore;
            ViewBag.QuestionNumber = Count;

            List<QuestionCategory> quest_cats = await _context.QuestionCategories.ToListAsync();
            int nextQuestionId = GetRandomQuestionId();
            Question question = await _context.Questions.FindAsync(nextQuestionId);

            var viewModel = new QuestionViewModel
            {
                Question = question,
                QuestionCategories = quest_cats
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FilterQuestions(int catId)
        {
            var filtered = await (from q in _context.Questions
                orderby q.QuestionId
                where !Visited.Contains(q.QuestionId)
                select new {questionId = q.QuestionId, body = q.QuestionBody, catId = q.QuestionCategoryId }).ToListAsync();

            if (catId > 0)
            {
                filtered = filtered.Where(f => f.catId == catId).ToList();
            }

            MinQuestionId = filtered.Min(q => q.questionId);
            MaxQuestionId = filtered.Max(q => q.questionId);

            return await GetNextQuestion();
        }

        [HttpGet]
        public async Task<IActionResult> GetNextQuestion(bool fromFilter = true)
        {
            int nextId = GetRandomQuestionId();
            var question = await (from q in _context.Questions
                where q.QuestionId == nextId
                select new { questionId = q.QuestionId, body = q.QuestionBody, catId = q.QuestionCategoryId }).FirstOrDefaultAsync();
            
            if (!fromFilter)
            {
                Count++;
            }

            var data = new
            {
                currentScore = CurrentScore,
                number = Count,
                question
            };
            var jsonData = Json(data);
            return jsonData;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswer(int qId, string answer)
        {
            //todo: implement fuzzy matching
            Question question = await _context.Questions.Include(q => q.QuestionCategory).FirstOrDefaultAsync(q => q.QuestionId == qId);
            string stored = question.Answer.ToLower();
            string input = answer.ToLower().Trim();
            //bool success = string.Equals(, answer.Trim(), StringComparison.CurrentCultureIgnoreCase);

            //Check for number vs word digit representation match by converting number to word.
            if(int.TryParse(stored, out int numStored))
            {
                stored = numStored.ToWords();
            }
            
            if(int.TryParse(input, out int inputStored))
            {
                input = inputStored.ToWords();
            }

            //Perform fuzzy matching using Levenshtein distance and consider 90% match a success
            int ratio = Fuzz.Ratio(stored, input);
            bool success = ratio >= 90;
            if (success)
            {
                CurrentScore += question.QuestionCategory.ScoreWeight;
                _currentUser.Money += question.QuestionCategory.ScoreWeight;

                _context.Entry(_currentUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                Visited.Add(qId);
            }

            return Json(new {success, currentScore = CurrentScore});
        }

        private int GetRandomQuestionId()
        {
            int currentId;
            do
            {
                currentId = Rand.Next(MinQuestionId, MaxQuestionId+1);
            } while (Visited.Contains(currentId));

            return currentId;
        }
    }
}