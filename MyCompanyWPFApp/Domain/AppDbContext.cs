using MyCompanyWPFApp.Domain.Entities;
using System.Data.Entity;

namespace MyCompanyWPFApp.Domain
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyCompanyDB;Integrated Security=True;Pooling=False")
        { }
        #region Инициализация классов бд
        public DbSet<TextField> TextFields { get; set; } //Страницы
        public DbSet<ServiceItem> ServiceItems { get; set; } //Услуги
        public DbSet<Question> Questions { get; set; } //Вопросы
        public DbSet<ProjectItem> ProjectItems { get; set; } //Проекты
        public DbSet<BlogItem> BlogItems { get; set; } //Блог
        #endregion


    }
}
