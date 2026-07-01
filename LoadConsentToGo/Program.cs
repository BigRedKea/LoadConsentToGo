
using Newtonsoft.Json;


namespace LoadConsentToGo
{
    internal static class Program
    {
        static Consent2GoFunctions c = new();

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

            GroupLookupLoadData.WriteToCSV(grouplookup, Path.Combine(Consent2GoFunctions.consent2gopathlookup, "consent2golookup.csv"));

            var path = Path.Combine(Consent2GoFunctions.consent2gopathdatabase, @"consent2go.db");
            var sqlLiteWrapperStudentProfile = new SqlLiteWrapperStudentProfile(path);
            var sqlLiteWrapperSystemUser = new SqlLiteWrapperSystemUser(path);




            switch (action)
            {
                case "student_data_to_db":
                    {
                        var mergeddata = MergeExcelStudentData.Execute();
                        var x = sqlLiteWrapperStudentProfile.Upsert(mergeddata);
                        Console.WriteLine($"Merged {x} items from {mergeddata.Count} records from Excel files");
                        break;
                    }

                case "download staff data":
                    {
                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

                        foreach (var formationlookup in grouplookup)
                        {
                            c.DownloadStaffGroupData(formationlookup);
                        }
                    }
                    break;

                case "staff_data_to_db":
                    {
                        var mergeddata = MergeExcelSystemUserData.Execute();
                        var x = sqlLiteWrapperSystemUser.Upsert(mergeddata);
                        Console.WriteLine($"Merged {x} items from {mergeddata.Count} records from Excel files");
                        break;
                    }


                case "upload_systemuser_data":
                    {
                        var baselinedata = sqlLiteWrapperSystemUser.GetData();

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

                        var smsdata = LoadSMSStaffData.Load(openFileDialog.FileName);

                        var datatoload = (from f in smsdata
                                          where !baselinedata.Select(x => x.Email).Contains(f.Email)
                                          select f).ToList();

                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

                        foreach (var itm in datatoload)
                        {
                            var formationlookup = grouplookup.Where(x => x.SMSOrgId == itm.SiteIdentifier).FirstOrDefault();

                            if (formationlookup == null)
                            {
                                Console.WriteLine($"No lookup found for {itm.SiteIdentifier}");
                                continue;
                            }
                            itm.Grouplookup = formationlookup;
                        }

                        UploadConsentToGoSystemUser(datatoload);
                        //DownloadConsentToGoStudent(datatoload);
                        break;
                    }

                case "upload":
                    {

                        var baselinedata = sqlLiteWrapperStudentProfile.GetData();

                        string brownseasmslookupkey = "102231";

                        //string[] brownseaparticipants =
                        //      { "293644",
                        //        "333278",
                        //        "281018",
                        //        "306509",
                        //        "281983",
                        //        "275885",
                        //        "286540",
                        //        "247377",
                        //        "283160",
                        //        "244775",
                        //        "202402",
                        //        "214570",
                        //        "256965",
                        //        "224265",
                        //        "252535",
                        //        "249264",
                        //        "321764",
                        //        "184584",
                        //        "279802",
                        //        "224265" };


                        //var datatoload = baselinedata.Where(x => brownseaparticipants.Contains(x.UniqueIdentifier)).ToList();
                        //foreach (var itm in datatoload)
                        //{
                        //    itm.SiteUniqueIdentifier = brownseasmslookupkey;
                        //}


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

                        var smsdata = LoadSMSStudentData.Load(openFileDialog.FileName);

                        var datatoload = (from f in smsdata
                                          where !baselinedata.Select(x => x.UniqueIdentifier).Contains(f.UniqueIdentifier)
                                          select f).ToList();

                        c.Open();
                        c.Login(config.Consent2GoUsername, config.Consent2GoPassword);

                        foreach (var itm in datatoload)
                        {
                            var formationlookup = grouplookup.Where(x => x.SMSOrgId == itm.SiteUniqueIdentifier).FirstOrDefault();

                            if (formationlookup == null)
                            {
                                Console.WriteLine($"No lookup found for {itm.SiteUniqueIdentifier}");
                                continue;
                            }
                            itm.Grouplookup = formationlookup;
                        }

                        UploadConsentToGoStudent(datatoload);
                        break;
                    }
            }

            MessageBox.Show("Finished", "Finished", MessageBoxButtons.OK);
        }



        static void UploadConsentToGoSystemUser(List<SystemUser> datatoload)
        {
            var grpbysmsdata = datatoload.GroupBy(x => x.SiteIdentifier);

            int cnt = 0;
            foreach (var formationdata in grpbysmsdata)
            {
                var formationlookup = formationdata.FirstOrDefault()?.Grouplookup;
                if (formationlookup != null)
                {
                    Console.WriteLine($"{formationlookup.FormationName}");
                    c.OpenGroup(formationlookup);

                    foreach (var item in formationdata.OrderBy(x => x.LastName))
                    {
                        try
                        {
                            cnt++;
                            Console.WriteLine($"Processing {cnt}/ {datatoload.Count} {item}");
                            c.UploadStaffData(item, cnt);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }


                //c.DownloadStudentGroupData(formationlookup);

            }
        }


        static void UploadConsentToGoStudent(List<StudentData> datatoload)
        {
            var grpbysmsdata = datatoload.GroupBy(x => x.SiteUniqueIdentifier);

            int cnt = 0;
            foreach (var formationdata in grpbysmsdata)
            {
                var formationlookup = formationdata.FirstOrDefault()?.Grouplookup;
                if (formationlookup != null)
                {
                    Console.WriteLine($"{formationlookup.FormationName}");
                    c.OpenGroup(formationlookup);

                    foreach (var item in formationdata.OrderBy(x => x.LastName))
                    {
                        try
                        {
                            cnt++;
                            Console.WriteLine($"Processing {cnt}/ {datatoload.Count} {item}");
                            c.UploadStudent(item, cnt);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }


                c.DownloadStudentGroupData(formationlookup);

            }
        }
    }
}
