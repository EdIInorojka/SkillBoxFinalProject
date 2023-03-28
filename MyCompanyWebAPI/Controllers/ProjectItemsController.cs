using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain;
using MyCompanyWebAPI.Domain.Entities;

namespace MyCompanyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectItemsController : Controller
    {
        AppDbContext context;
        public ProjectItemsController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IEnumerable<ProjectItem> GetProjectItems()
        {
            return context.ProjectItems;
        }
        [HttpGet("{id}")]
        public ProjectItem GetProjectItemById(Guid id)
        {
            return context.ProjectItems.FirstOrDefault(x => x.Id == id);
        }
        [HttpPost]
        public void SaveProjectItem([FromBody] ProjectItem entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added; //Добавление вопроса
            else
                context.Entry(entity).State = EntityState.Modified; //Обновление вопроса
            context.SaveChanges();
        }
        [HttpDelete("{id}")]
        public void DeleteProjectItem(Guid id)
        {
            context.ProjectItems.Remove(new ProjectItem() { Id = id });
            context.SaveChanges();
        }
    }
}
