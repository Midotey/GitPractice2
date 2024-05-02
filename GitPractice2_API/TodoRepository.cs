using GitPractice2_API.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.TestDecoding;

namespace GitPractice2_API
{
    public interface ITodoRepository : IDisposable
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(int id);
        Task CreateTodoItem(TodoItem todoItem);
        Task DeleteTodoItem(int id);
        Task UpdateTodoItem(/*int id, */TodoItem todoItem);
        Task Save();
    }
    public class TodoRepository : ITodoRepository
    {
        private Action ReorderId;
        private readonly TodoItemContext db;
        public TodoRepository(TodoItemContext db)
        {
            this.db = db;
            //db = new TodoItemContext();
            ReorderId += () =>
            {
                //db.Database.ExecuteSqlRaw("alter sequence \"TodoItems_Id_seq\" restart with 1;\r\nupdate \"TodoItems\" set \"Id\" =nextval('\"TodoItems_Id_seq\"') where \"Id\" > 0;");
                //db.Database.ExecuteSqlRaw("select setval('\"TodoItems_Id_seq\"', coalesce((select max(\"Id\") from \"TodoItems\" ti), 1), false);\r\nalter sequence \"TodoItems_Id_seq\" restart with 1;\r\nupdate \"TodoItems\" set \"Id\" =nextval('\"TodoItems_Id_seq\"') where \"Id\" > 0;");

                db.Database.ExecuteSqlRaw("select setval('\"TodoItems_Id_seq\"', coalesce((select max(\"Id\") + 1 from \"TodoItems\" ti), 1), false);\r\nupdate \"TodoItems\" set \"Id\" =nextval('\"TodoItems_Id_seq\"') where \"Id\" > 0;\r\nalter sequence \"TodoItems_Id_seq\" restart with 1;\r\nupdate \"TodoItems\" set \"Id\" =nextval('\"TodoItems_Id_seq\"') where \"Id\" > 0;");
            };
        }
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await db.TodoItems.ToListAsync();
        }
        public async Task<TodoItem> GetTodoItem(int id)
        {
            //if (db.TodoItems.Any(t => t.Id == id) == false)
            //    return null;
            //return db.TodoItems.Where(t => t.Id == id).First();
            return await db.TodoItems.FindAsync(id);
        }
        public async Task CreateTodoItem(TodoItem todoItem)
        {
            //if (todoItem == null)
            //    return;
            //db.TodoItems.Add(todoItem);
            if (todoItem == null)
                return;
            //await db.TodoItems.AddAsync(todoItem);
            db.TodoItems.Attach(todoItem);
            //ReorderId?.Invoke();
        }
        public async Task DeleteTodoItem(int id)
        {
            //if (db.TodoItems.Any(t => t.Id == id) == false)
            //    return;
            //db.Remove(db.TodoItems.Where(t => t.Id == id).First());
            TodoItem todoItem = await db.TodoItems.FindAsync(id);
            db.TodoItems.Remove(todoItem);
            //ReorderId?.Invoke();
        }
        public async Task UpdateTodoItem(/*int id, */TodoItem todoItem)  
        {
            ////if(db.TodoItems.Any())
            //db.Entry(todoItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //db.Entry(todoItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            ////TodoItem entity = await db.TodoItems.FindAsync(todoItem.Id);
            ////TodoItem entity = await db.TodoItems.FindAsync(todoItem.Name);
            //TodoItem entity = await db.TodoItems.FindAsync(id);
            //if (entity == null)
            //    return;
            ////db.TodoItems.Entry(entity).CurrentValues.SetValues(todoItem);
            //db.TodoItems.Entry(entity).State = EntityState.Modified;
            ////db.TodoItems.Attach(entity);
            //entity.Name = todoItem.Name;
            //entity.Text = todoItem.Text;
            //entity.IsComplete = todoItem.IsComplete;
            ////ReorderId?.Invoke();


            ////db.TodoItems.Entry(todoItem).State = EntityState.Modified;
            //var entity = await db.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            //if (entity == null)
            //    return;
            ////db.TodoItems.Attach(todoItem);
            //db.TodoItems.Entry(todoItem).State = EntityState.Modified;
            //entity.Name = todoItem.Name;
            //entity.Text = todoItem.Text;
            //entity.IsComplete = todoItem.IsComplete;

            //var entity = await db.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
            //if (entity == null)
            //    return;
            //entity.Name = todoItem.Name;
            //entity.Text = todoItem.Text;
            //entity.IsComplete = todoItem.IsComplete;
            //db.TodoItems.Entry(todoItem).State = EntityState.Modified;

            var entity = await db.TodoItems.FindAsync(todoItem.Id);
            if (entity == null)
                return;
            db.TodoItems.Entry(entity).CurrentValues.SetValues(todoItem);


        }
        public async Task Save()
        {
            //db.SaveChanges();

            //await db.SaveChangesAsync();
            //ReorderId?.Invoke();
            //await db.SaveChangesAsync();

            db.SaveChanges();
            ReorderId?.Invoke();
            //db.SaveChanges();
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
