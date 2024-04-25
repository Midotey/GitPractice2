using Microsoft.EntityFrameworkCore;

namespace GitPractice2_API
{
    public class TodoItemContext : DbContext
    {
        public TodoItemContext() : base(...)  ///TODO: добавить строку подклюения к бд в WebAPI
        {

        }

    }
}
