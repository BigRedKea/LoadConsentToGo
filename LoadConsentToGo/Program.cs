using Newtonsoft.Json;


namespace LoadConsentToGo
{
    internal static class Program
    {
        static readonly Consent2GoFunctions consent2gofunctions = new();

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
            var filename = Path.Combine(Consent2GoFunctions.consent2gopath, "secrets.json");
            string jsonData;

            try
            {
                jsonData = File.ReadAllText(filename);
            }
            catch (DirectoryNotFoundException ex)
            {
                Logging.Instance.Log($"Directory not found: {ex.Message}", true);
                return;
            }
            catch (Exception ex)
            {
                Logging.Instance.Log($"Error reading config: {ex.Message}", true);
                return;
            }

            Logging.Instance.Log("Reading secrets.json");

            var config = JsonConvert.DeserializeObject<ConfigData>(jsonData);
            if (config == null)
            {
                MessageBox.Show("Could not read config data from secrets.json", "Error", MessageBoxButtons.OK);
                return;
            }

            var grouplookup = GroupLookupLoadData.Load("GroupLookup.json");

            GroupLookupLoadData.WriteToCSV(grouplookup, Path.Combine(Consent2GoFunctions.consent2gopathlookup, "consent2golookup.csv"));

            var path = Path.Combine(Consent2GoFunctions.consent2gopathdatabase, @"consent2go.db");
            var sqlLiteWrapper = new SqlLiteWrapper(path);

            {

                var baselinedata = sqlLiteWrapper.GetData();
                List<C2GData> datatoload;

                //var loaddataintospecificsite = "102231"; // brownsea
                string? loaddataintospecificsite = null;

                if (loaddataintospecificsite == null)
                {

                    var smsdata = LoadSMSData.Load();
                    datatoload = (from f in smsdata
                                  where !baselinedata.Select(x => x.UniqueIdentifier).Contains(f.UniqueIdentifier)
                                  select f).ToList();
                }
                else
                {

                    string[] brownseaparticipants = {"281983",
                                   "247377",
                                   "333278",
                                   "275885",
                                   "244775",
                                   "281018",
                                   "279802",
                                   "306509",
                                   "321764",
                                   "256965",
                                   "249264" };

                    datatoload = baselinedata.Where(x => brownseaparticipants.Contains(x.UniqueIdentifier)).ToList();
                    foreach (var itm in datatoload)
                    {
                        itm.SiteUniqueIdentifier = loaddataintospecificsite;
                    }
                }

                consent2gofunctions.Open();
                consent2gofunctions.Login(config.Consent2GoUsername, config.Consent2GoPassword);

                //populate the group lookup for each entry based on the site unique identifier 
                foreach (var itm in datatoload)
                {
                    var formationlookup = grouplookup.Where(x => x.SMSOrgId == itm.SiteUniqueIdentifier).FirstOrDefault();

                    if (formationlookup == null)
                    {
                        Logging.Instance.Log($"No lookup found for {itm.SiteUniqueIdentifier}", true);
                        continue;
                    }
                    itm.Grouplookup = formationlookup;
                }
                consent2gofunctions.LoadConsentToGo(datatoload);

                Logging.Instance.Log("Finished", true);
            }
        }
    }
}
