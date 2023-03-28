using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System;

namespace MyCompany.Controllers
{
    public class BlogsController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public BlogsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Страница статьи блога
        public IActionResult Index(Guid id) //Переход на страницу со статьей
        {
            if (id != default) //Проверка Id статьи
            {
                return View("Show", dataManager.BlogItems.GetBlogItemById(id));
            }

            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageBlogs");
            return View(dataManager.BlogItems.GetBlogItems());
        }
        #endregion
    }
}
