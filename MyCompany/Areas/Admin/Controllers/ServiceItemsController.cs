using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Контролер для страниц редактирования услуг
    /// </summary>
    [Area("Admin")]
    public class ServiceItemsController : Controller
    {
        #region Подключение маршрутизатора бд и IWebHostEnvironment
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        #endregion
        #region Переход на страницы для редактирования услуг
        public IActionResult ShowAll() //Страница со всеми улугами
        {
            var entity = dataManager.ServiceItems.GetServiceItems();
            return View(entity);
        }
        public IActionResult Edit(Guid id) //Страница для редактирования услуги
        {
            var entity = id == default ? new ServiceItem() : dataManager.ServiceItems.GetServiceItemById(id);
            return View(entity);
        }
        #endregion
        #region Действия для страниц редактирования услуг
        [HttpPost]
        public IActionResult Edit(ServiceItem model, IFormFile titleImageFile) //Сохранение изменений услуг
        {
            if (ModelState.IsValid) //Проверка валидности модели
            {
                if (titleImageFile != null) //Загрузка картинки
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                dataManager.ServiceItems.SaveServiceItem(model); //Сохранение услуги в бд
                //Перехеод на главную страницу редактирования
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id) //Удаление услуги из бд
        {
            dataManager.ServiceItems.DeleteServiceItem(id);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
        #endregion
    }
}