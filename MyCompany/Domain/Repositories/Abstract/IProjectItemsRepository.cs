using System;
using System.Collections.Generic;
using System.Linq;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для действий с бд для проектов
    /// </summary>
    public interface IProjectItemsRepository
    {
        IEnumerable<ProjectItem> GetProjectItems(); //Получение всех проектов из бд
        ProjectItem GetProjectItemById(Guid id); //Получение проекта по Id
        void SaveProjectItem(ProjectItem entity); //Сохранение проекта в бд
        void DeleteProjectItem(Guid id); //Удаление проекта из бд
    }
}
