using GitPractice2_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GitPractice2_API
{
    public class TodoItemContext : DbContext
    {
        //public DbSet<Note> Notes => Set<Note>();
        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        private readonly IConfiguration Configuration;
        public TodoItemContext(IConfiguration configuration)
        {  ///TODO: добавить в параметры приложения инициализацию контекста класса бд добавления значения, чтобы исправно запускалась апишка с бд
            this.Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
