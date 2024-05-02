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
        //[Route("api/[controller]/{name}/{text}")]
        [HttpPost("name, text")]
        public async Task PostTodoItem([FromForm] string name, [FromForm] string text)
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
        //public async Task UpdateTodoItem(/*[FromForm]*//*[FromBody]*/int id, /*[FromForm]*//*[FromBody]*/string name, /*[FromForm]*//*[FromBody]*/string text, /*[FromForm]*/ /*[FromBody]*/bool isComplete)
        public async Task UpdateTodoItem([FromForm] int id, [FromForm] string name, [FromForm] string text, [FromForm] bool isComplete)
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

                //string baseUrl = $"{Request.Scheme}://{Request.Host.Value}/{Request.Path}";
                //string requestUri = $"{Request.Scheme}://{Request.Host.Value}/";
                //var baseUrl = Request.Path;
                //await Task.Delay(100);
            }

        }


    }
}
