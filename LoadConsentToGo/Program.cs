using Newtonsoft.Json;

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
            Process();
        }

        public static void Process()
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

            FileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files | *.csv",
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = "C:\\temp\\"
            };
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName == "")
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

            MessageBox.Show("Finished", "Finished", MessageBoxButtons.OK);

        }
    }
}