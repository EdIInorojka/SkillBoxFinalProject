using System;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Controllers
{
    /// <summary>
    /// Контроллер для страницы услуг
    /// </summary>
    public class ServicesController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public ServicesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Страница услуг
        public IActionResult Index(Guid id) //Переход на страницу со всеми услугами и отдельной услугой
        {
            if (id != default) //Проверка Id услуги
            {
                return View("Show", dataManager.ServiceItems.GetServiceItemById(id)); //Страница с отдельной услугой
            }

            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageServices"); 
            return View(dataManager.ServiceItems.GetServiceItems());
        }
        #endregion
    }
}