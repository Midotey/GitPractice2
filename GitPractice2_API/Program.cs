
using GitPractice2_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GitPractice2_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<ITodoRepository, TodoRepository>(); 
            //builder.Services.AddTransient<ITodoRepository, TodoRepository>();
            //builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
            //builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddDbContext<TodoItemContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<ITodoRepository, TodoRepository>();
            //builder.Services.AddHttpContextAccessor();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
