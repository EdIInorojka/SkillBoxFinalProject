using System;
using System.Collections.Generic;
using System.Linq;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для действий с бд для блога
    /// </summary>
    public interface IBlogItemsRepository
    {
        IEnumerable<BlogItem> GetBlogItems(); //получение всех статей из бд
        BlogItem GetBlogItemById(Guid id); //получение статьи по Id
        void SaveBlogItem(BlogItem entity); //Сохрание статьи в бд
        void DeleteBlogItem(Guid id); //Удаление статьи из бд
    }
}

