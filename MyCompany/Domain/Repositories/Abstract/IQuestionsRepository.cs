using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany.Domain.Entities;
namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для действий с бд для вопросов
    /// </summary>
    public interface IQuestionsRepository
    {
        IEnumerable<Question> GetQuestions(); //Получение всех вопросов из бд
        Question GetQuestionById(Guid id); //Получение вопроса по Id
        void SaveQuestion(Question entity); //Сохранение вопроса в бд
        void DeleteQuestion(Guid id); //Удаление вопроса из бд
    }
}
