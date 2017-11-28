using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class QuestionsController : BaseController
    {
        // GET: Questions
        public async Task<ActionResult> Index()
        {
            var userId = GetUserId();
            var questions = await AppContext.Questions
                .Where(q => 
                    q.ResponderId == userId && 
                    string.IsNullOrEmpty(q.Response)
                )
                .ToListAsync();

            if (questions.Any())
            {
                var askerIds = questions.Select(q => q.AskerId).ToList();
                var askers = await AppContext.Users.Where(u => askerIds.Contains(u.Id)).ToListAsync();
                questions.ForEach(q => q.Asker = askers.FirstOrDefault(a => a.Id == q.AskerId));
            }

            return View(questions);
        }
        public async Task<ActionResult> OpenAsk(Guid id)
        {
            var question = await AppContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
            return View(question);
        }

        [HttpPost]
        public async Task<ActionResult> PostAnswer(Question question)
        {
            AppContext.Questions.Attach(question);
            await AppContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteAnswer(Guid id)
        {
            var question = await AppContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
            return View(question);
        }

        [HttpPost]
        [ActionName("DeleteAnswer")]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            var question = await AppContext.Questions.FirstOrDefaultAsync(q => q.Id == id);
            AppContext.Questions.Remove(question);
            await AppContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }        
    }
}