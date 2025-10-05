using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Domain.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public int ExamId { get; set; }
        [ForeignKey("ExamId")]
        public string QuestionText { get; set; } = string.Empty;
        public int Marks { get; set; }

        // Navigation
        [JsonIgnore]
        public Exam Exam { get; set; }
        [JsonIgnore]
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
