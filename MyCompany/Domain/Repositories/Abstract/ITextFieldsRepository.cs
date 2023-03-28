using System;
using System.Collections.Generic;
using System.Linq;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для действий с бд для текстовых полей
    /// </summary>
    public interface ITextFieldsRepository
    {
        IEnumerable<TextField> GetTextFields(); //Получение всех текстовых полей из бд
        TextField GetTextFieldById(Guid id); //Получение текстового поля по Id
        TextField GetTextFieldByCodeWord(string codeWord); //Получение текстового поля по кодовому слову
        void SaveTextField(TextField entity); //Сохранение текстового поля в бд
        void DeleteTextField(Guid id); //Удаление текстового поля из бд
    }
}
