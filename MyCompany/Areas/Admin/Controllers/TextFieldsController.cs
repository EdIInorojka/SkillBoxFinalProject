using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Контроллер для редактирования текстовых полей
    /// </summary>
    [Area("Admin")]
    public class TextFieldsController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;
        public TextFieldsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Страница редактирования текстовых полей
        public IActionResult Edit(string codeWord)
        {
            var entity = dataManager.TextFields.GetTextFieldByCodeWord(codeWord);
            return View(entity);
        }
        #endregion
        #region Действия для страниц редактирования
        [HttpPost]
        public IActionResult Edit(TextField model) //Сохранение изменений в бд
        {
            if (ModelState.IsValid) //Проверка валидности модели
            {
                dataManager.TextFields.SaveTextField(model); // Сохранение изменений
                //Переход на главную страницу редактирования
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
            }
            return View(model);
        }
        #endregion
    }
}