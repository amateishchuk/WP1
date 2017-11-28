using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profile
        public async Task<ActionResult> Index(string userId)
        {
            var allowAskQuestions = true;
            if (string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
                if (userId == null)
                    return RedirectToAction("Login", "Account");
                allowAskQuestions = false;
            }

            var user = await UserManager.FindByIdAsync(userId);
            var questions = await AppContext.Questions
                .Where(q => q.ResponderId == userId && !string.IsNullOrEmpty(q.Response))
                .OrderByDescending(q => q.DateTimeResponse).ToListAsync();

            if (questions.Any())
            {
                var askerIds = questions.Select(q => q.AskerId).ToList();
                var askers = await AppContext.Users.Where(u => askerIds.Contains(u.Id)).ToListAsync();
                questions.ForEach(q => q.Asker = askers.FirstOrDefault(a => a.Id == q.AskerId));
            }

            var profileViewModel = new ProfileViewModel
            {
                User = user,
                Questions = questions,
                AllowAskQuestions = allowAskQuestions
            };

            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PostQuestion(string text, string responderId)
        {
            string userId = null;
            try
            {
                userId = GetUserId();
            }
            catch
            {

            }

            var question = new Question
            {
                Ask = text,
                AskerId = userId,
                ResponderId = responderId
            };

            AppContext.Questions.Add(question);
            await AppContext.SaveChangesAsync();

            return RedirectToAction("Index", "Profile", new { userId = responderId });
        }

        public async Task<ActionResult> Like(Guid id, string responderId, bool isLike = true)
        {
            var item = await AppContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
            item.Points = isLike ? ++item.Points : --item.Points;

            // if (item.Points < -5) then add to moderator question list

            AppContext.Questions.Attach(item);
            await AppContext.SaveChangesAsync();

            return RedirectToAction("Index", "Profile", new { userId = responderId });
        }
    }
}