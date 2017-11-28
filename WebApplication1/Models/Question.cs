using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AskerId { get; set; }
        [NotMapped]
        public ApplicationUser Asker { get; set; } = null;
        public string Ask { get; set; }
        public string ResponderId { get; set; }
        public string Response { get; set; }
        public DateTime DateTimeAsk { get; set; } = DateTime.Now;
        public Nullable<DateTime> DateTimeResponse { get; set; }
        public int Points { get; set; } = 0;
    }
}