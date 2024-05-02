using System.Dynamic;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GitPractice2
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;
        private Action Get;
        private TodoItem[] todoItems1;
        public Form1()
        {
            this.CenterToScreen();
            InitializeComponent();
            httpClient = new HttpClient();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Get += async () =>
            {
                comboBox1.Items.Clear();
                string response = await httpClient.GetStringAsync("https://localhost:7255/api/TodoItem");
                /*var */todoItems1 = JsonSerializer.Deserialize<TodoItem[]>(response);
                if (todoItems1 == null)
                    return;
                foreach (var todoItem in todoItems1)
                    comboBox1.Items.Add($"{todoItem?.Id} - {todoItem?.Name}");
                //comboBox1.DataSource = todoItems1;
                //MessageBox.Show($"Name: {todoItem.Name} | Id: {todoItem.Id} | Text: {todoItem.Text} | IsComplete: {todoItem.IsComplete}");

                //string response = await httpClient.GetStringAsync("https://localhost:7255/api/TodoItem/id?id=1");
                //TodoItem? todoItem = JsonSerializer.Deserialize<TodoItem>(response);
                //MessageBox.Show($"Name: {todoItem?.Name} | Id: {todoItem?.Id} | Text: {todoItem?.Text} | IsComplete: {todoItem?.IsComplete}");
            };
            Get?.Invoke();

        }

        private async void button1_Click(object sender, EventArgs e)  //put
        {
            //Get?.Invoke();
            if (comboBox1.SelectedIndex < 0)
                return;
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox1.Text))
                return;
            var todoResponse = await httpClient.GetStringAsync($"https://localhost:7255/api/TodoItem/id?id={comboBox1.SelectedIndex + 1}");
            TodoItem? todoItem1 = JsonSerializer.Deserialize<TodoItem>(todoResponse);
            if (todoItem1 == null)
                return;
            todoItem1.Name = comboBox1.Text;
            todoItem1.Text = textBox1.Text;
            todoItem1.IsComplete = checkBox1.Checked;
            //string json = JsonSerializer.Serialize(todoItem1);
            //MessageBox.Show(json);
            //StringContent content = new StringContent(/*JsonSerializer.Serialize(todoItem1)*/json/*@"{""id"":1, ""name"": ""freak!"", ""text"": ""bye bye!"" , ""isComplete"": false}"*/, Encoding.UTF8, "application/json");
            //var response = await httpClient.PutAsync("https://localhost:7255/api/TodoItem/id, name, text, isComplete", content);
            //var response = await httpClient.PutAsJsonAsync("https://localhost:7255/api/TodoItem/id, name, text, isComplete", todoItem1);

            ////MessageBox.Show(response.ToString());
            //var json = JsonSerializer.Serialize(todoItem1);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = httpClient.PutAsync("https://localhost:7255/api/TodoItem/id, name, text, isComplete", content);
            //MessageBox.Show(response.Result.ToString());

            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "id", todoItem1.Id.ToString() },
                { "name", todoItem1.Name },
                { "text", todoItem1.Text },
                { "isComplete", todoItem1.IsComplete.ToString() }
            });
            var response = await httpClient.PutAsync("https://localhost:7255/api/TodoItem/id, name, text, isComplete", formContent);
            //MessageBox.Show(response.ToString());

            Get?.Invoke();
        }
        private async void button3_Click(object sender, EventArgs e)  //delete
        {
            if (comboBox1.SelectedIndex < 0)
                return;
            //MessageBox.Show(comboBox1.SelectedIndex.ToString());
            var response = await httpClient.DeleteAsync($"https://localhost:7255/api/TodoItem/id?id={comboBox1.SelectedIndex + 1}");
            Get?.Invoke();
        }
        private async void button2_Click(object sender, EventArgs e)  //post
        {
            //var values = new Dictionary<string, string>()
            //{
            //    { "thing1", comboBox1.Text },
            //    { "thing2", textBox1.Text }
            //};
            ////var values = new StringContent($"{comboBox1.Text} {textBox1.Text}");
            //var content = new FormUrlEncodedContent(values);
            //var response = await httpClient.PostAsync("https://localhost:7255/api/TodoItem/", content);
            //var responseString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(textBox1.Text))
                return;
            var values = new Dictionary<string, string>()
            {
                { "name", comboBox1.Text.ToString() },
                { "text", textBox1.Text.ToString() },

                //{ "name", "nigga!!!!daw" },
                //{ "text","dibil" },
            };
            var content = new FormUrlEncodedContent(values);
            //var json = JsonSerializer.Serialize(values);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7255/api/TodoItem/name, text", content);
            var responseString = await response.Content.ReadAsStringAsync();
            //MessageBox.Show(responseString);

            //MessageBox.Show(await response.Content.ReadAsStringAsync());

            Get?.Invoke();
        }
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  //get
        {
            //Get?.Invoke();
            if (comboBox1.SelectedIndex < 0)
                return;
            var todoItem = todoItems1[comboBox1.SelectedIndex];
            textBox1.Text = todoItem.Text;
            checkBox1.Checked = todoItem.IsComplete;
            //Get?.Invoke();

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }


        ///TODO: сделать TODO приложуху
        /* 1. Сделать интерфейс -p.
         * 2. Сделать апишку. -d.
         * 3. Соединить интерфейс с апишкой - добавить логику, сделать запросы в API. -p.
         * 4. ...
         */





    }

}
