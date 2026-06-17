using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
//using OpenQA.Selenium.DevTools.V145.Page;

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


            var lookup = GroupLookupLoadData.Load("GroupLookup.json");

            GroupLookupLoadData.WriteToCSV(lookup,  Path.Combine( Consent2GoFunctions.consent2gopathlookup, "consent2golookup.csv"));

            switch (action)
            {
                case "download":
                    {
                        var c = new Consent2GoFunctions();
                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);
                        c.DownloadGroupData(lookup);
                        break;
                    }

                case "baseline":
                    {
                        // Pull data from Excel Downloads
                        var mergeddata = MergeExcelData.Execute();
                        var path = Path.Combine(Consent2GoFunctions.consent2gopathdatabase, @"consent2go.db");
                        var x = SqlLiteWrapper.Upsert(path, mergeddata);
                        Console.WriteLine($"Merged {x} items from {mergeddata.Count} records from Excel files");
                        break;
                    }

                case "upload":
                    {
                        var c = new Consent2GoFunctions();
                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);
                        //var oneDrivePath = Environment.GetEnvironmentVariables(); //.GetEnvironmentVariable("Scouts Queensland");

                        FileDialog openFileDialog = new OpenFileDialog
                        {
                            Filter = "CSV Files | *.csv",
                            CheckFileExists = true,
                            CheckPathExists = true,
                            InitialDirectory = Consent2GoFunctions.consent2gopathupdownloads
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

                        break;
                    }
            }

            MessageBox.Show("Finished", "Finished", MessageBoxButtons.OK);

        }
    }
}