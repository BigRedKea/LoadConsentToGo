using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadConsentToGo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public string FormAction = "";

        // Step 1: Upload prepared student records to Consent2Go.
        private void btnUpload_Click(object sender, EventArgs e)
        {
            FormAction = "upload";
            Close();
        }

        // Step 2: Import student Excel data into the local SQLite database.
        private void btnExcelToSqlLite_Click(object sender, EventArgs e)
        {
            FormAction = "student_data_to_db";
            Close();
        }

        // Step 3: Import staff Excel data into the local SQLite database.
        private void staff_data_upload_button_Click(object sender, EventArgs e)
        {
            FormAction = "staff_data_to_db";
            Close();
        }

        // Step 4: Upload system-user records back to Consent2Go.
        private void button1_Click(object sender, EventArgs e)
        {
            FormAction = "upload_systemuser_data";
            Close();
        }
    }

}