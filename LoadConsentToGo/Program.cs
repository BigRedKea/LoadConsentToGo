
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

            var frmMain = new FormMain();
            frmMain.ShowDialog();
            Process(frmMain.FormAction);
        }

        public static void Process(string action)
        {
            // open secrets.json and read the username and password
            var filename = Path.Combine(Consent2GoFunctions.consent2gopath, "secrets.json");
            string jsonData;

            try
            {
                jsonData = File.ReadAllText(filename);
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show($"Directory not found: {ex.Message}", "Error", MessageBoxButtons.OK);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading config: {ex.Message}", "Error", MessageBoxButtons.OK);
                return;
            }

            Console.WriteLine("Reading secrets.json");

            var config = JsonConvert.DeserializeObject<ConfigData>(jsonData);
            if (config == null)
            {
                MessageBox.Show("Could not read config data from secrets.json", "Error", MessageBoxButtons.OK);
                return;
            }

            var grouplookup = GroupLookupLoadData.Load("GroupLookup.json");

            GroupLookupLoadData.WriteToCSV(grouplookup,  Path.Combine( Consent2GoFunctions.consent2gopathlookup, "consent2golookup.csv"));

            var path = Path.Combine(Consent2GoFunctions.consent2gopathdatabase, @"consent2go.db");
            var sqlLiteWrapper = new SqlLiteWrapper(path);


            switch (action)
            {

                case "baseline":
                    {
                        // Pull data from Excel Downloads
                        //255331

                        var mergeddata = MergeExcelData.Execute();
                        var x = sqlLiteWrapper.Upsert(mergeddata);
                        Console.WriteLine($"Merged {x} items from {mergeddata.Count} records from Excel files");
                        break;
                    }

                case "upload":
                    {
                        var openFileDialog = new OpenFileDialog
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

                        var c = new Consent2GoFunctions();
                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

                        var smsdata = LoadSMSData.Load(openFileDialog.FileName);
                        var baselinedata = sqlLiteWrapper.GetData();

                        var cnt = 0;



                        var excludedatalareadyloaded = (from f in smsdata
                                     where !baselinedata.Select(x=> x.UniqueIdentifier).Contains(f.UniqueIdentifier)
                                     select f).ToList();


                        var grpbysmsdata = excludedatalareadyloaded.GroupBy(x => x.SiteUniqueIdentifier);

                        
                        foreach (var formationdata in grpbysmsdata)
                        {
                            var formationlookup = grouplookup.Where(x => x.SMSOrgId == formationdata.Key).FirstOrDefault();

                            if (formationlookup == null)
                            {
                                Console.WriteLine($"No lookup found for {formationdata.Key}");
                                continue;
                            }

                            Console.WriteLine($"{formationlookup.FormationName}");
                            c.OpenGroup(formationlookup);

     
                            foreach (var item in formationdata.OrderBy(x => x.LastName))
                            {
                                item.Grouplookup = formationlookup;

                                try
                                {
                                    cnt++;
                                    Console.WriteLine($"Processing {cnt}/ {excludedatalareadyloaded.Count} {item}");
                                    c.Process(item, cnt);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                            }

                            c.DownloadGroupData(formationlookup);
                        }

                        break;
                    }
            }

            MessageBox.Show("Finished", "Finished", MessageBoxButtons.OK);
        }
    }
}