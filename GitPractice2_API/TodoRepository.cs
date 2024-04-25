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
        void Save();
    }
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoItemContext db;
        public TodoRepository(TodoItemContext db)
        {
            this.db = db;
        }

        public IEnumerable<TodoItem> GetTodoItems()
        {
            return db.TodoItems;
        }
        public TodoItem GetTodoItem(int id)
        {
            if (db.TodoItems.Any(t => t.Id == id) == false)
                return null;
            return db.TodoItems.FirstOrDefault(t => t);  ///TODO: доделать реализацию паттерна-репозиторий
        }


        private bool isDisposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing == true)
                    //rep.Dispose();
                    db.Dispose();
            }
            this.isDisposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
