using System;
using System.Collections.Generic;
using System.Linq;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain.Repositories.Abstract
{
    /// <summary>
    /// Интерфейс для действий с бд для услуг
    /// </summary>
    public interface IServiceItemsRepository
    {
        IEnumerable<ServiceItem> GetServiceItems(); //Получение всех услуг из бд
        ServiceItem GetServiceItemById(Guid id); //Получение услуги по ID
        void SaveServiceItem(ServiceItem entity); //Сохранение услуги в бд
        void DeleteServiceItem(Guid id); //Уддаление усулги из бд
    }
}
