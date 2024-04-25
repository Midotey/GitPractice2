namespace GitPractice2_API.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public bool IsComplete { get; set; }
        public TodoItem() { }
        public TodoItem(string name, string task)
        {
            Name = name;
            Task = task;
        }
    }
}
