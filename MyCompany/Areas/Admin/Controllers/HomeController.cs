using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Areas.Admin.Controllers
{
    /// <summary>
    /// Контроллер для главной страницы редактирования
    /// </summary>
    [Area("Admin")]
    public class HomeController : Controller
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public HomeController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        #region Переход на главную страницу редактирования
        public IActionResult Index() 
        {
            return View(dataManager.ServiceItems.GetServiceItems());
        }
        #endregion
    }
}