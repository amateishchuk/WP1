using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Question> Questions { get; set; }
        public bool AllowAskQuestions { get; internal set; }
    }
}