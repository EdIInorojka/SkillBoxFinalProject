using System;
using System.ComponentModel.DataAnnotations;
namespace MyCompanyWPFApp.Domain.Entities
{
    public abstract class QuestionsBase
    {
        protected QuestionsBase() => DateAdded = DateTime.UtcNow;

        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        public virtual string Email { get; set; }
        [Display(Name = "Имя и фамилия")]
        public virtual string FullName { get; set; }
        [Display(Name = "Текст вопроса")]
        public virtual string QuestionText { get; set; }
        [Display(Name = "Статус вопроса")]
        public virtual string QuestionStatus { get; set; }

        [DataType(DataType.Time)]
        public DateTime DateAdded { get; set; }
    }
}
