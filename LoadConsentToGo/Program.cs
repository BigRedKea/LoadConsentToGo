using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V145.Page;

namespace LoadConsentToGo
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var frmMain = new FormMain();
            frmMain.ShowDialog();
            Process(frmMain.FormAction);
        }

        public static void Process(string action)
        {
            // open secrets.json and read the username and password
            var filename = "secrets.json";
            var jsonData = File.ReadAllText(filename);

            Console.WriteLine("Reading secrets.json");

            var config = JsonConvert.DeserializeObject<ConfigData>(jsonData);
            if (config == null)
            {
                MessageBox.Show("Could not read config data from secrets.json", "Error", MessageBoxButtons.OK);
                return;
            }

            var c = new Consent2GoFunctions();

            c.Open();
            c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

            var lookup = GroupLookupLoadData.Load("GroupLookup.json");

            GroupLookupLoadData.WriteToCSV(lookup, @"C:\temp\consent2golookup.csv");

            switch (action)
            {
                case "download":

                    c.DownloadGroupData(lookup);
                    break;

                case "upload":

                    var oneDrivePath = Environment.GetEnvironmentVariables(); //.GetEnvironmentVariable("Scouts Queensland");

                    FileDialog openFileDialog = new OpenFileDialog
                    {
                        Filter = "CSV Files | *.csv",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        InitialDirectory = Consent2GoFunctions.consent2gopath
                    };
                    openFileDialog.ShowDialog();

                    if (openFileDialog.FileName == "")
                    {
                        MessageBox.Show("No file selected", "Error", MessageBoxButtons.OK);
                        return;
                    }

                    var smsdata = LoadSMSData.Load(openFileDialog.FileName);

                    var cnt = 0;
                    foreach (var item in smsdata.OrderBy(x => x.LastName))
                    {
                        Console.WriteLine($"Processing {cnt}/ {smsdata.Count} {item}");
                        c.Process(item, lookup, cnt);
                    }

                    MessageBox.Show("Finished", "Finished", MessageBoxButtons.OK);
                    break;
            }

        }
    }
}