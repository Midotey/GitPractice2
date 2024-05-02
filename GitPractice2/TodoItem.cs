using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GitPractice2
{
    public class TodoItem
    {
        [JsonPropertyName("id")]
        public int Id { get; /*private */set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("isComplete")]
        public bool IsComplete { get; set; }

        //public int id { get; set; }
        //public string name { get; set; }
        //public string text { get; set; }
        //public bool isComplete { get; set; }

        public TodoItem() { }
        public TodoItem(int id, string name, string text, bool isComplete)
        {
            Id = id;
            Name = name;
            Text = text;
            IsComplete = isComplete;

            //this.id = id;
            //this.name = name;
            //this.text = text;
            //this.isComplete = isComplete;
        }
    }
}
