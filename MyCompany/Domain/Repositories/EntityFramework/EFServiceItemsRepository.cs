using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;

namespace MyCompany.Domain.Repositories.EntityFramework
{
    /// <summary>
    /// Класс для работы с услугами в бд
    /// </summary>
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        #region Подключение контекста бд
        private readonly AppDbContext context;
        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }
        #endregion
        #region Действия с услугами в бд
        public IEnumerable<ServiceItem> GetServiceItems() //Получение всех услуг через контекст
        {
            return context.ServiceItems;
        }

        public ServiceItem GetServiceItemById(Guid id) //Получение услуги по Id
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }

        public void SaveServiceItem(ServiceItem entity) //Сохранение услуги в бд
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление услуги
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление услуги
            context.SaveChanges();
        }

        public void DeleteServiceItem(Guid id) //Удаление услуги из бд по Id
        {
            context.ServiceItems.Remove(new ServiceItem() { Id = id });
            context.SaveChanges();
        }
        #endregion
    }
}
