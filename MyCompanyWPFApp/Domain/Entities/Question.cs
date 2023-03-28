using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyCompanyWPFApp.Domain.Entities
{
    public class Question : QuestionsBase
    {
        [Required]
        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Required]
        [Display(Name = "Имя и фамилия")]
        public override string FullName { get; set; }

        [Required]
        [Display(Name = "Текст вопроса")]
        public override string QuestionText { get; set; }

    }
}
