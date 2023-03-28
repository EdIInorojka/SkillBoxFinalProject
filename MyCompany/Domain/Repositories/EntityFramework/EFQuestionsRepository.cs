using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс для работы с вопросами в бд
    /// </summary>
    public class EFQuestionsRepository : IQuestionsRepository
    {
        #region Подключение контекста бд
        private readonly AppDbContext context;

        public EFQuestionsRepository(AppDbContext context)
        {
            this.context = context;
        }
        #endregion
        #region Действия с вопросами в бд
        public IEnumerable<Question> GetQuestions() //Получение всех вопросов из бд через контекст
        {
            return context.Questions;
        }

        public Question GetQuestionById(Guid id) //Получение вопроса по Id
        {
            return context.Questions.FirstOrDefault(x => x.Id == id);
        }

        public Question GetQuestionByEmail(string Email) //Получение вопроса по Email
        {
            return context.Questions.FirstOrDefault(x => x.Email == Email);
        }

        public void SaveQuestion(Question entity) //Сохранение вопроса в бд
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление вопроса
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление вопроса
            context.SaveChanges();
        }

        public void DeleteQuestion(Guid id) //Удаление вопроса из бд по Id
        {
            context.Questions.Remove(new Question() { Id = id });
            context.SaveChanges();
        }
    }
    #endregion
}
