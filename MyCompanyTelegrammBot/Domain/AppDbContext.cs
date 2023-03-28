using MyCompanyTelegramBot.Domain.Entities;
using System.Data.Entity;

namespace MyCompanyTelegramBot.Domain
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("Data Source=(localdb)\\mssqllocaldb; Initial Catalog=MSSQLLocalDB; Integrated Security=True; Pooling=False")
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
