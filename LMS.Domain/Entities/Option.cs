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
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]

        [Required]
        public string OptionText { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }
    }

}
