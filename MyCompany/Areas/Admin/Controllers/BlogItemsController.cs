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
    /// Контроллер для страницы блог
    /// </summary>
    [Area("Admin")]
    public class BlogItemsController : Controller
    {
        #region Подключение маршрутизатора бд и IWebHostEnvironment
        private readonly DataManager dataManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public BlogItemsController(DataManager dataManager, IWebHostEnvironment hostingEnvironment)
        {
            this.dataManager = dataManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        #endregion
        #region Переход на страницы для редактирования статей
        public IActionResult ShowAll() //Страница со всеми статьями
        {
            var entity = dataManager.BlogItems.GetBlogItems();
            return View(entity);
        }
        public IActionResult Edit(Guid id) //Страница для редактирования статьи
        {
            var entity = id == default ? new BlogItem() : dataManager.BlogItems.GetBlogItemById(id);
            return View(entity);
        }
        #endregion
        #region Действия для страницы редактирования
        [HttpPost]
        public IActionResult Edit(BlogItem model, IFormFile titleImageFile) //Сохранение изменений статьи
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
                dataManager.BlogItems.SaveBlogItem(model); //Сохранение изменений в бд
                //Возврат на главную старницу редактирования
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController()); 
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id) //Удаление статьи из бд по Id
        {
            dataManager.BlogItems.DeleteBlogItem(id);
            //Возврат на главную траницу редактирования
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
        #endregion
    }
}
