using GitPractice2_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace GitPractice2_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private ITodoRepository rep;
        public TodoItemController(ITodoRepository rep)
        {
            this.rep = rep;
        }
        [HttpGet()]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            using (rep)
            {
                return await rep.GetTodoItems();
            }
        }
        [HttpGet("id")]
        public async Task<TodoItem> GetTodoItem(int id)
        {
            using (rep)
            {
                return await rep.GetTodoItem(id);
            }

        }
        [HttpPost("name, text")]
        public async Task PostTodoItem(string name, string text)
        {
            using (rep)
            {
                if (string.IsNullOrEmpty(name) == true || string.IsNullOrEmpty(text) == true)
                    return;
                await rep.CreateTodoItem(new TodoItem(name, text));
                await rep.Save();
            }
        }
        [HttpDelete("id")]
        public async Task RemoveTodoItem(int id)
        {
            using (rep)
            {
                await rep.DeleteTodoItem(id);
                await rep.Save();
            }
        }
        [HttpPut("id, name, text, isComplete")]
        public async Task UpdateTodoItem(int id, string name, string text, bool isComplete)
        {
            if (string.IsNullOrEmpty(name) == true || string.IsNullOrEmpty(text) == true)
                return;
            using (rep)
            {
                var todoItem = new TodoItem(name, text) { Id = id, IsComplete = isComplete };
                //await rep.UpdateTodoItem(/*new TodoItem(name, text)*/todoItem);
                //todoItem.Name = name;
                //todoItem.Text = text;
                //todoItem.IsComplete = isComplete;
                await rep.UpdateTodoItem(/*id, */todoItem);
                await rep.Save();

            }

        }


    }
}
