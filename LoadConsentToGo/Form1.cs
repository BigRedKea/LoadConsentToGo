using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoadConsentToGo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // open secrets.json and read the username and password
            var filename = "secrets.json";
            var jsonData = File.ReadAllText(filename);

            var config = JsonConvert.DeserializeObject<ConfigData>(jsonData);
            if (config == null)
            {
                MessageBox.Show("Could not read config data from secrets.json", "Error", MessageBoxButtons.OK);
                return;
            }

            FileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files | *.csv",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = "C:\\temp\\"
            };
            openFileDialog.ShowDialog();



            if (openFileDialog.FileName == "" )
            {
                MessageBox.Show("No file selected", "Error", MessageBoxButtons.OK);
                return;
            }

            var smsdata = LoadSMSData.Load(openFileDialog.FileName);
            var c = new Consent2GoFunctions();

            c.Open();
            c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

            var lookup = GroupLookupLoadData.Load("GroupLookup.json");

            foreach (var item in smsdata)
            {
                c.Process(item, lookup);
            }
        }
    }
}
