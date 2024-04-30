using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitPractice2_API.Models
{
    public class TodoItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order =0)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsComplete { get; set; }
        public TodoItem() { }
        public TodoItem(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
