using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;

namespace practiceee
{
    public partial class Form1 : Form
    {
        public readonly HttpClient _client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {

            var request = new LoginRequest
            {
                username = txtUsername.Text,
                pass_word = txtPassword.Text
            };

            string json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("http://127.0.0.1:5000/login", content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<LoginResponse>(data);
                var frm = new frmPatientManagement();
                frm.ShowDialog();
            } else
            {
             
                MessageBox.Show($"ERROR: {data}");
            }
          


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();

            frm.Close();
        }
    }
}
