using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс для работы с текстовыми полями в бд
    /// </summary>
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        #region Подключение контекста бд
        private readonly AppDbContext context;
        public EFTextFieldsRepository(AppDbContext context)
        {
            this.context = context;
        }
        #endregion
        #region Действия с текстовыми полями в бд
        public IEnumerable<TextField> GetTextFields() //Получение всех текстовых полей через контекст
        {
            return context.TextFields;
        }

        public TextField GetTextFieldById(Guid id) //Получение текстового поля по Id
        {
            return context.TextFields.FirstOrDefault(x => x.Id == id);
        }

        public TextField GetTextFieldByCodeWord(string codeWord) //Получение текстового поля по кодовому слову
        {
            return context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
        }

        public void SaveTextField(TextField entity) //Сохранение текстового поля в бд
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление текстового поля
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление текстового поля
            context.SaveChanges();
        }

        public void DeleteTextField(Guid id) //Удаление текстового поля по Id
        {
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }
        #endregion
    }
}
