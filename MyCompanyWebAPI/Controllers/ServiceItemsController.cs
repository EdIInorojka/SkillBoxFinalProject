using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain;
using MyCompanyWebAPI.Domain.Entities;

namespace MyCompanyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceItemsController : Controller
    {
        AppDbContext context;
        public ServiceItemsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]

        public IEnumerable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }
        [HttpGet("{id}")]
        public ServiceItem GetServiceItemById(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void SaveServiceItem([FromBody] ServiceItem entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление вопроса
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление вопроса
            context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void DeleteServiceItem(Guid id)
        {
            context.BlogItems.Remove(new BlogItem() { Id = id });
            context.SaveChanges();
        }
    }
}
