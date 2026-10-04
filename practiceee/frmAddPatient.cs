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
    public partial class frmAddPatient : Form
    {
        public HttpClient _client = new HttpClient();
   
        public frmAddPatient(string name, string contact, string status)
        {
            InitializeComponent();
        }



        private async void btnSave_Click(object sender, EventArgs e)
        {
            var request = new Patient()
            {
                fullname = txtFullname.Text,
                contact_number = txtContact.Text,
                status = "active"
            };

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("http://127.0.0.1:5000/patients", content);

            var data = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<Patient>(data);
                MessageBox.Show(result.fullname);
                MessageBox.Show(data.ToString());
                var frm = new frmAddPatient(request.fullname, request.contact_number, request.status);
                frm.Close();
            } else
            {
                MessageBox.Show("Error");
            }

        }

        private void frmAddPatient_Load(object sender, EventArgs e)
        {

        }
    }
}
