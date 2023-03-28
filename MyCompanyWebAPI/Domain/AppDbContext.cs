using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain.Entities;

namespace MyCompanyWebAPI.Domain
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ServiceItem> ServiceItems { get; set; } //Услуги
        public DbSet<Question> Questions { get; set; } //Вопросы
        public DbSet<ProjectItem> ProjectItems { get; set; } //Проекты
        public DbSet<BlogItem> BlogItems { get; set; } //Блог
            }
}
