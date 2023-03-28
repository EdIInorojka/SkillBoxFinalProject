using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCompany.Domain;

namespace MyCompany.Models.ViewComponents
{
    /// <summary>
    /// Класс для боковой области с услугами
    /// </summary>
    public class SidebarViewComponent : ViewComponent
    {
        #region Подключение маршрутизатора бд
        private readonly DataManager dataManager;

        public SidebarViewComponent(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        #endregion
        public Task<IViewComponentResult> InvokeAsync() //Метод для блока с услугами (сбоку)
        {
            return Task.FromResult((IViewComponentResult) View("Default", dataManager.ServiceItems.GetServiceItems()));
        }
    }
}
