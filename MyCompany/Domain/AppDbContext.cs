using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain.Entities;

namespace MyCompany.Domain
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        #region Инициализация классов бд
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TextField> TextFields { get; set; } //страницы
        public DbSet<ServiceItem> ServiceItems { get; set; } //Услуги
        public DbSet<Question> Questions { get; set; } //Вопросы
        public DbSet<ProjectItem> ProjectItems { get; set; } //Проекты
        public DbSet<BlogItem> BlogItems { get; set; } //Блог
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Добавление админа и роли для него

            //Добавление роли админа
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            //Заполнение данных для админа
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "3b62472e-4f66-49fa-a20f-e7685b9565d8",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "my@email.com",
                NormalizedEmail = "MY@EMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "superpassword"),
                SecurityStamp = string.Empty
            });
            //Связывание админа и его роли
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "44546e06-8719-4ad8-b88a-f271ae9d6eab",
                UserId = "3b62472e-4f66-49fa-a20f-e7685b9565d8"
            });
            #endregion

            #region Добавление страниц в бд
            //Главная страница
            modelBuilder.Entity<TextField>().HasData(new TextField {
                Id = new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"), 
                CodeWord = "PageIndex", 
                Title = "Главная"
            });
            //Страница услуг
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"), 
                CodeWord = "PageServices", 
                Title = "Наши услуги"
            });
            //Страница контактов
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"), 
                CodeWord = "PageContacts", 
                Title = "Контакты"
            });
            //Страница проектов
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("339612bd-38ae-43c7-b1d3-c14820e8f754"),
                CodeWord = "PageProjects",
                Title = "Наши проекты"
            });
            //Страница блога
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("032e81fa-2ade-4f31-afa8-46ab2039dcc0"),
                CodeWord = "PageBlog",
                Title = "Блог"
            });
            //Страница вопросов
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("baa5ab7b-b825-4289-bd30-fa11215bfa76"),
                CodeWord = "PageQuestions",
                Title = "Вопросы"
            });
            #endregion
        }
    }
}
