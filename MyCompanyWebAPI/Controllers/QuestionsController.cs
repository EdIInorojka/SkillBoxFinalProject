using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain;
using MyCompanyWebAPI.Domain.Entities;

namespace MyCompanyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        AppDbContext context;
        public QuestionsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<Question> GetQuestions()
        {
            IQueryable<Question> questions = context.Questions;
            return questions;
        }
        [HttpGet("{id}")]
        public Question GetQuestionItemById(Guid id)
        {
            return context.Questions.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void SaveQuestion([FromBody] Question entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление вопроса
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление вопроса
            context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void DeleteQuestion(Guid id)
        {
            context.Questions.Remove(new Question() { Id = id });
            context.SaveChanges();
        }

    }
}
