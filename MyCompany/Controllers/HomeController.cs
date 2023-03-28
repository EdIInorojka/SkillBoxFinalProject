using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Service;
using System;

namespace MyCompany.Controllers
{
    public class HomeController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Переход на главную страницу и страницу контактов
        public IActionResult Index() //Переход на главную страницу
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageIndex"));
        }

        public IActionResult Contacts() //Переход на страницу контактов
        {
            return View(dataManager.TextFields.GetTextFieldByCodeWord("PageContacts"));
        }
        #endregion
        #region Добавление вопросов на главной странице
        [HttpPost]
        public IActionResult Index(Question model) //Метод добавления вопроса
        {
            model.QuestionStatus = "Принят";
            dataManager.QuestionRepository.SaveQuestion(model); //Сохранение вопроса в бд
            //Обновление главной страницы
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
        #endregion
    }
}