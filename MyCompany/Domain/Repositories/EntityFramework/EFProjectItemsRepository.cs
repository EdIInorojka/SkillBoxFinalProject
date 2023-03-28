using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс для работы с проектами в бд
    /// </summary>
    public class EFProjectItemsRepository : IProjectItemsRepository
    {
        #region Подключение контекста бд
        private readonly AppDbContext context;
        public EFProjectItemsRepository(AppDbContext context)
        {
            this.context = context;
        }
        #endregion
        public IEnumerable<ProjectItem> GetProjectItems() //Получение 
        {
            return context.ProjectItems;
        }
        #region Действия с бд для проектов
        public ProjectItem GetProjectItemById(Guid id) //Получение проектов из бд через контекст
        {
            return context.ProjectItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveProjectItem(ProjectItem entity) //Сохранение проекта в бд
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление нового проекта
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление проекта в бд
            context.SaveChanges();
        }

        public void DeleteProjectItem(Guid id) //Удаление проекта из бд по Id
        {
            context.ProjectItems.Remove(new ProjectItem() { Id = id });
            context.SaveChanges();
        }
        #endregion
    }
}