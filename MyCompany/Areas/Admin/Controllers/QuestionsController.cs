using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using Newtonsoft.Json.Schema;
using System;
using System.Threading.Tasks;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Контроллер для страницы редактирования вопросов
    /// </summary>
    [Area("Admin")]
    public class QuestionsController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public QuestionsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Переход на страницы для редактирования вопросов
        public IActionResult ShowAll(string codeWord) //Страница с таблицей вопросов из бд
        {
            var entity = dataManager.QuestionRepository.GetQuestions();
            return View(entity);
        }
        public IActionResult Edit(Guid id) //Страница с таблицей вопросов из бд
        {
            var entity = dataManager.QuestionRepository.GetQuestionById(id);
            return View(entity);
        }
        #endregion
        #region Действия для страниц редактирования вопросов
        [HttpPost]
        public IActionResult SaveChanges(Question model)
        {
            dataManager.QuestionRepository.SaveQuestion(model);
            return RedirectToAction(nameof(QuestionsController.ShowAll), nameof(QuestionsController).CutController());
        }
        [HttpPost]
        public IActionResult Delete(Guid id)  //Удаление вопроса из бд
        {
            dataManager.QuestionRepository.DeleteQuestion(id);
            return RedirectToAction(nameof(QuestionsController.ShowAll), nameof(QuestionsController).CutController());
        }
        #endregion
    }
}
