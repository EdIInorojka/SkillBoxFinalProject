using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain;
using MyCompanyWebAPI.Domain.Entities;

namespace MyCompanyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogItemsController : ControllerBase
    {
        AppDbContext context;
        public BlogItemsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<BlogItem> GetBlogItems()
        {
            return context.BlogItems;
        }
        [HttpGet("{id}")]
        public BlogItem GetBlogItemById(Guid id)
        {
            return context.BlogItems.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void SaveBlogItem([FromBody]BlogItem entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление вопроса
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление вопроса
            context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void DeleteBlogItem(Guid id)
        {
            context.BlogItems.Remove(new BlogItem() { Id = id });
            context.SaveChanges();
        }
    }
}
