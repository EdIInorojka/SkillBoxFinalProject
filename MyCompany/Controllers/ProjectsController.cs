using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using System;

namespace MyCompany.Controllers
{
    public class ProjectsController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public ProjectsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Страница со всеми проектами
        public IActionResult Edit(string codeWord) //Переход на страницу с проектами по кодовому слову
        {
            var entity = dataManager.TextFields.GetTextFieldByCodeWord(codeWord);
            return View(entity);
        }
        #endregion
        #region Страница отдельного проекта
        public IActionResult Index(Guid id) //Переход на страницу проекта по Id
        {
            if (id != default) //Проверка id проекта
            {
                return View("Show", dataManager.ProjectItems.GetProjectItemById(id));//Переход на страницу проекта
            }

            ViewBag.TextField = dataManager.TextFields.GetTextFieldByCodeWord("PageProjects");
            return View(dataManager.ProjectItems.GetProjectItems());
        }
        #endregion
    }
}
