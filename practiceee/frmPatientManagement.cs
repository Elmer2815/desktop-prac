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
    public partial class frmPatientManagement : Form
    {
        public int Id { get; set; }
        private HttpClient _client = new HttpClient();
        private readonly string url = "http://127.0.0.1:5000";
        public frmPatientManagement()
        {
            InitializeComponent();
        }

        private async void frmPatientManagement_Load(object sender, EventArgs e)
        {
            await showData(dgvPatient);
        }

        public async Task showData(DataGridView dgv)
        {
            HttpResponseMessage response = await _client.GetAsync($"{url}/patients");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<Patient>>(json);

            dgv.DataSource = data;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var patient = new Patient();

            var frm = new frmAddPatient(patient.fullname, patient.contact_number, patient.status);
            frm.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await showData(dgvPatient);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPatient.CurrentRow == null)
                return;

            string columnName = dgvPatient.CurrentCell.OwningColumn.Name;
            MessageBox.Show(columnName);
            int patientId = Convert.ToInt32(dgvPatient.CurrentRow.Cells["patient_id"].Value);


            var frm = new frmUpdate(patientId);
            frm.Show();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatient.CurrentRow == null)
                return;

            var selectedId = Convert.ToInt32(dgvPatient.CurrentRow.Cells["patient_id"].Value);

            var json = JsonSerializer.Serialize(selectedId);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.DeleteAsync($"http://127.0.0.1:5000/patients/{selectedId}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Successfully Deleted");
            }

            await showData(dgvPatient);
        }
    }
}
