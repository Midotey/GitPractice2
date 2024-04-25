using GitPractice2_API.Models;

namespace GitPractice2_API
{
    public interface ITodoRepository : IDisposable
    {
        IEnumerable<TodoItem> GetTodoItems();
        TodoItem GetTodoItem(int id);
        void CreateTodoItem(TodoItem todoItem);
        void DeleteTodoItem(int id);
        void UpdateTodoItem(int id);
    }
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoRepository rep;
        public TodoRepository(ITodoRepository rep)
        {
            this.rep = rep;
        }

    }
}
