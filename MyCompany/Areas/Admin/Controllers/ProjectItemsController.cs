using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System;
using System.IO;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Контроллер для страницы проекты
    /// </summary>
    [Area("Admin")]
    public class ProjectItemsController : Controller
    {
        #region Подключение маршрутизатора бд и IWebHostEnvironment
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ProjectItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        #endregion
        #region Переход на страницы для редактирование проектов
        public IActionResult ShowAll() //Страница со всеми проектами
        {
            var entity = dataManager.ProjectItems.GetProjectItems();
            return View(entity);
        }
        public IActionResult Edit(Guid id) //Страница ля редактирования проекта
        {
            var entity = id == default ? new ProjectItem() : dataManager.ProjectItems.GetProjectItemById(id);
            return View(entity);
        }
        #endregion
        #region Действия для страницы редактирования
        [HttpPost]
        public IActionResult Edit(ProjectItem model, IFormFile titleImageFile) //Сохранение изменений проекта
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
                dataManager.ProjectItems.SaveProjectItem(model); //Сохранение изменений в бд
                //Возврат на главную страницу редактирования
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id) //Удаление проекта из бд
        {
            dataManager.ProjectItems.DeleteProjectItem(id);
            //Возврат на главную страницу редактирования
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
        #endregion
    }
}
