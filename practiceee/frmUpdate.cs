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
    public partial class frmUpdate : Form
    {
        private int patientId;
        private HttpClient _client = new HttpClient();
        public frmUpdate(int id)
        {
            patientId = id;
            InitializeComponent();
        }

        private async void frmUpdate_Load(object sender, EventArgs e)
        {
            lblId.Text = patientId.ToString();

            var response = await _client.GetAsync($"http://127.0.0.1:5000/patients/{patientId}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<Patient>(json);

            txtFullname.Text = data.fullname;
            txtContact.Text = data.contact_number;
            cbStatus.Text = data.status;

        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var request = new Patient
            {
                fullname = txtFullname.Text,
                contact_number = txtContact.Text,
                status = cbStatus.Text
            };

            var json = JsonSerializer.Serialize(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"http://127.0.0.1:5000/patients/{patientId}", content);

            MessageBox.Show(response.StatusCode.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
