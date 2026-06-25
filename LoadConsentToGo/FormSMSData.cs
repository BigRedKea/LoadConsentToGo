using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LoadConsentToGo
{
    public partial class FormSMSData : Form
    {
        public FormSMSData()
        {
            InitializeComponent();
            DialogResult = DialogResult.OK;
        }

        internal void LoadSMSData(C2GDownload smsdata)
        {
            txtGroup.Text = smsdata.Grouplookup?.FormationName;
            txtFirstName.Text = smsdata.FirstName;
            txtLastName.Text = smsdata.LastName;
            txtGuardian1Email.Text = smsdata.Guardian1Email;
            txtUniqueId.Text = smsdata.UniqueIdentifier;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            // Create
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
