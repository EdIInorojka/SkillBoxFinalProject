using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{

    /// <summary>
    /// Класс для работы со статьями в бд
    /// </summary>
    public class EFBlogItemsRepository : IBlogItemsRepository
    {
        #region Подключение контекста бд
        private readonly AppDbContext context;
        public EFBlogItemsRepository(AppDbContext context)
        {
            this.context = context;
        }
        #endregion
        #region Действия с бд для статей блога
        public IEnumerable<BlogItem> GetBlogItems() //Получение статей из бд через контекст
        {
            return context.BlogItems;
        }

        public BlogItem GetBlogItemById(Guid id) //Получение статьи из бд по Id через контекст
        {
            return context.BlogItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveBlogItem(BlogItem entity) //Сохранение статьи в бд
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление статьи
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление статьи
            context.SaveChanges();
        }

        public void DeleteBlogItem(Guid id) //Удаление статьи из бд по Id
        {
            context.BlogItems.Remove(new BlogItem() { Id = id });
            context.SaveChanges();
        }
        #endregion
    }
}
