using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Security.Policy;
using static System.ComponentModel.Design.ObjectSelectorEditor;
//using OpenQA.Selenium.DevTools.V145.Audits;

namespace LoadConsentToGo
{
    internal class Consent2GoFunctions
    {
        readonly ChromeDriver driver = new();
        public int emailcounter = 0;

        public static string profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();

        public static string consent2gopath = Path.Combine(profilePath, @"Scouts Queensland", "SO Admin - Consent2Go", "Automated Upload");
        public static string consent2gopathupload = Path.Combine(consent2gopath, "Uploads");
        public static string consent2gopathuploadlog = Path.Combine(consent2gopathupload, "Logs");
        public static string consent2gopathupdownloads = Path.Combine(consent2gopathupload, "Downloads");

        private string LogFilePath = Path.Combine(consent2gopathuploadlog, $"consent2golog{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.log");
        internal static string consent2gopathlookup = Path.Combine(consent2gopath, "Lookups");
        internal static string consent2gopathdatabase = Path.Combine(consent2gopath, "Database");

        public GroupLookupData? GroupName { get; private set; }

        public Consent2GoFunctions()
        {
            driver = new ChromeDriver();
            Log("Consent2GoFunctions initialized");
        }

        public void Log(string message)
        {
            try
            {
                var dir = Path.GetDirectoryName(LogFilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var entry = $"{DateTime.UtcNow:O} {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, entry);
            }
            catch
            {
                // Swallow logging errors so logging never breaks execution.
            }
        }

        public void Open()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        public void Login(string username, string password)
        {
            Log($"Login start for user '{username}'");
            driver.Navigate().GoToUrl("https://www.mcbschools.com/Login");

            var loginBox = driver.FindElement(By.Name("username"));
            loginBox.SendKeys(username);
            var submitButton = driver.FindElement(By.ClassName("amplify-button"));
            submitButton.Click();
            Thread.Sleep(500);

            driver.FindElement(By.Name("password")).SendKeys(password);
            submitButton.Click();
            Thread.Sleep(500);
            Log("Login sequence completed");
        }


        public void DownloadStudentGroupData(GroupLookupData lookup)
        {
            try
            {
                OpenGroup(lookup);
                driver.Navigate().GoToUrl("https://www.mcbschools.com/School/Player");

                Thread.Sleep(3000);
                driver.FindElement(By.ClassName("btn-secondary")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.LinkText("Export to Excel")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("btnSelectAllColumns")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("btnReport_Player")).Click();
                Thread.Sleep(1000);
                RenameLatestFile("StudentList_*_UTC.xlsx", $"student_{lookup.FormationName}.xlsx");
            }
            catch (Exception ex)
            {
                Log($"Exception {ex.Message}");
            }
        }

        public void DownloadStaffGroupData(GroupLookupData lookup)
        {
            try
            {
                OpenGroup(lookup);
                driver.Navigate().GoToUrl("https://www.mcbschools.com/School/SystemUsers");
                driver.ExecuteScript("ExportToExcel()");
                Thread.Sleep(2000);
                RenameLatestFile("SystemUser*.xlsx", $"staff_{lookup.FormationName}.xlsx");
            }
            catch (Exception ex)
            {
                Log($"Exception {ex.Message}");
            }
        }


        void RenameLatestFile(string serachpattern, string newFileName)
        {
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(userProfile, "Downloads");

            var directory = new DirectoryInfo(downloadsPath);
            if (!Directory.Exists(downloadsPath))
            {
                Log($"Downloads not located at: {downloadsPath}");
                return;
            }

            var latestFile = directory.GetFiles(serachpattern)
                                      .OrderByDescending(f => f.LastWriteTime)
                                      .FirstOrDefault();

            if (latestFile != null)
            {
                var newFilePath = Path.Combine(directory.FullName, newFileName);
                latestFile.MoveTo(newFilePath);
                Log($"Renamed file {latestFile.Name} to {newFileName}");
            }
            else
            {
                Log("No files found to rename.");
            }
        }




        public void OpenGroup(GroupLookupData lookup)
        {


            var url = $"https://www.mcbschools.com/DuplicateIndex?Id={lookup.Consent2GoOrgId}";
            driver.Navigate().GoToUrl(url);
            Log($"Navigated to {url}");
            Thread.Sleep(2000);
        }

        static int frmTop = 500;
        static int frmLeft = 1000;

        public void UploadStudent(C2GData smsdata, int cnt)
        {
            Log($"Process start: {smsdata?.FirstName} {smsdata?.LastName} Site:{smsdata?.SiteUniqueIdentifier} Count:{cnt}");

            emailcounter++;

            CheckExists(smsdata, cnt);


            //    var alreadyexists = MessageBox.Show($"{cnt} Does {smsdata.FirstName} {smsdata.LastName} of {lookup.FormationName} already exist?", "Exists?", MessageBoxButtons.YesNo);
            //Log($"CheckExists result: {(alreadyexists == DialogResult.Yes)}");
            //return (alreadyexists == DialogResult.Yes);
            //{
            Log($"CheckExists returned true for {smsdata.FirstName} {smsdata.LastName}");
            var frm = new FormSMSData();
            frm.LoadSMSData(smsdata);

            // 1. Tell Windows Forms you want to set the coordinates manually
            // frm.StartPosition = FormStartPosition.Manual;

            // 2. Set the X and Y coordinates (in pixels) from the top-left of the screen
            //frm.Location = new Point(3000, 600);
            frm.Top = frmTop;
            frm.Left = frmLeft;

            var rslt = frm.ShowDialog();

            frmTop = frm.Top;
            frmLeft = frm.Left;

            if (rslt == DialogResult.OK) return;
            //}

            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/AddEditPlayer");
            Log("Navigated to AddEditPlayer form");

            driver.FindElement(By.Id("txtFirstName")).SendKeys(smsdata.FirstName);
            driver.FindElement(By.Id("txtLastName")).SendKeys(smsdata.LastName);
            driver.FindElement(By.Id("ddlTitle")).SendKeys(smsdata.Title);
            driver.FindElement(By.Id("txtBirthDate")).SendKeys(smsdata.BirthDate);
            driver.FindElement(By.Id("txtRegistration")).SendKeys(smsdata.UniqueIdentifier);

            var email = smsdata.Email;
            switch (emailcounter % 16)
            {
                case 0:
                    email = "noemails@mcbschools.a";
                    break;
                case 1:
                    email = "noemails@mcbschools.b";
                    break;
                case 2:
                    email = "noemails@mcbschools.c";
                    break;
                case 3:
                    email = "noemails@mcbschools.d";
                    break;
                case 4:
                    email = "noemails@mcbschools.e";
                    break;
                case 5:
                    email = "noemails@mcbschools.f";
                    break;
                case 6:
                    email = "noemails@mcbschools.g";
                    break;
                case 7:
                    email = "noemails@mcbschools.h";
                    break;
                case 8:
                    email = "noemails@mcbschools.i";
                    break;
                case 9:
                    email = "noemails@mcbschools.j";
                    break;
                case 10:
                    email = "noemails@mcbschools.k";
                    break;
                case 11:
                    email = "noemails@mcbschools.l";
                    break;
                case 12:
                    email = "noemails@mcbschools.n";
                    break;
                case 13:
                    email = "noemails@mcbschools.n";
                    break;
                case 14:
                    email = "noemails@mcbschools.p";
                    break;
                case 15:
                    email = "noemails@mcbschools.p";
                    break;
                default:
                    email = "noemails@mcbschools.e";
                    break;
            }

            driver.FindElement(By.Id("txtEmail")).SendKeys(email);
            Log($"Email set to {email}");

            driver.FindElement(By.Id("ddlSchoolYear")).SendKeys(smsdata.SchoolYear);

            switch (smsdata.Gender)
            {
                case "Male":
                    driver.FindElement(By.CssSelector("label[for='GenderMale']")).Click();
                    break;

                case "Female":
                    driver.FindElement(By.CssSelector("label[for='GenderFemale']")).Click();
                    break;
            }
            var msgboxresult = MessageBox.Show($"Check email is ok ", "Check Data", MessageBoxButtons.OK);

            driver.FindElement(By.Id("liAdditionalDetails")).Click();
            driver.FindElement(By.Id("txtGuardianName")).SendKeys(smsdata.Guardian1FirstName);
            driver.FindElement(By.Id("txtGuardianLastName")).SendKeys(smsdata.Guardian1LastName);
            if (!string.IsNullOrEmpty(smsdata.Guardian1Title)) driver.FindElement(By.Id("ddlGuardianTitle")).SendKeys(smsdata.Guardian1Title);
            driver.FindElement(By.Id("txtGuardianMobileNumber")).SendKeys(smsdata.Guardian1MobileNumber);
            driver.FindElement(By.Id("txtGuardianEmail")).SendKeys(smsdata.Guardian1Email);

            var commit = MessageBox.Show($"Ok to Commit {smsdata.FirstName} {smsdata.LastName} of {smsdata.Grouplookup.FormationName}", "Exists?", MessageBoxButtons.YesNo);

            if (commit == DialogResult.Yes)
            {
                driver.FindElement(By.Id("btnSave")).Click();
                Log("Member committed by user confirmation");
                Console.Write("Member Committed");
            }

            Log($"Process finished for {smsdata.FirstName} {smsdata.LastName}");
        }

        public void UploadStaffData(SystemUser smsdata, int cnt)
        {
            Log($"Process start: {smsdata?.FirstName} {smsdata?.LastName} Site:{smsdata?.SiteIdentifier} Count:{cnt}");

            emailcounter++;


            string role = "";
            switch (smsdata.Role)
            {
                //Activity Leader
                //Adult Supporter
                //Adult Supporter(Caretaker)
                //Adult Supporter(Chair)
                //Adult Supporter(Chairman)
                //Adult Supporter(Committee)
                //Adult Supporter(Secretary)
                //Adult Supporter(Treasurer)
                //Branch Commissioner(Environment and Sustainability)

                //District Leader(Joey Scouts)
                //Region Activity Leader(Activities)
                //Region Leader
                //Rover Scout
                //Member - Scout Fellowship


                //SMS Access(Training Team)
                //Staff(SO)

                //Team Supporter


                //case "Assistant Region Commissioner(Rover Scout Adviser)":

                //case "Team Member":


                case "Cub Scout Section":
                case "Assistant Cub Scout Leader":
                case "Cub Scout Leader":
                    role = "Cub Scout Section";
                    break;

                case "Full System Administration":
                    role = "Full System Administration";
                    break;

                case "Group Committee / Other Adults":
                    role = "Group Committee / Other Adults";
                    break;

                case "Assistant Group Leader":
                case "Group Leader":
                case "Group Leader / LIC":
                    role = "Group Leader / LIC";
                    break;

                case "Assistant Joey Scout Leader":
                case "Joey Scout Leader":
                case "Joey Scout Section":
                    role = "Joey Scout Section";
                    break;

                case "Rover Scout Section":
                    role = "Rover Scout Section";
                    break;

                case "Assistant Scout Leader":
                case "Scout Leader":
                case "Scout Section":
                    role = "Scout Section";
                    break;

                case "Assistant Venturer Scout Leader":
                case "Venturer Scout Leader":
                case "Venturer Scout Section":
                    role = "Venturer Scout Section";
                    break;

                default:
                    Log($"{smsdata.Role} not found");
                    break;

            }

            if (!string.IsNullOrEmpty(role))
            {
                driver.Navigate().GoToUrl("https://www.mcbschools.com/School/SystemUsers");

                Log($"CheckExists: Searching for {smsdata.FirstName} {smsdata.LastName} (count {cnt}) in {smsdata.Grouplookup?.FormationName}");

                driver.FindElement(By.Id("txtSearch")).SendKeys(smsdata.LastName);
                Thread.Sleep(1000);

                var rslt = MessageBox.Show($"Create {smsdata.FirstName} {smsdata.LastName} in {smsdata.Grouplookup?.FormationName}", "Create", MessageBoxButtons.YesNo);

                if (rslt == DialogResult.Yes)
                {
                    //var searchicon = driver.FindElement(By.CssSelector(".fa-search"));
                    //IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                    //driver.ExecuteScript("SearchUsers()");
                    Thread.Sleep(1000);


                    driver.Navigate().GoToUrl("https://www.mcbschools.com/School/AddEditUsers");
                    Thread.Sleep(1000);
                    driver.FindElement(By.Id("txtFirstName")).SendKeys(smsdata.FirstName);
                    driver.FindElement(By.Id("txtLastName")).SendKeys(smsdata.LastName);
                    driver.FindElement(By.Id("txtEmail")).SendKeys(smsdata.Email);
                    driver.FindElement(By.Id("ddl_role")).SendKeys(role);

                    var rslt2 = MessageBox.Show($"Save {smsdata.FirstName} {smsdata.LastName} in {smsdata.Grouplookup?.FormationName}", "Create", MessageBoxButtons.YesNo);

                    if (rslt2 == DialogResult.Yes)
                    {
                        driver.FindElement(By.Id("btnSave")).Click();
                    }

                }

            }
            Log($"Process finished for {smsdata.FirstName} {smsdata.LastName}");
        }

        public void CheckExists(C2GData smsdata, int cnt)
        {
            Log($"CheckExists: Searching for {smsdata.FirstName} {smsdata.LastName} (count {cnt}) in {smsdata.Grouplookup?.FormationName}");
            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/Player");

            driver.FindElement(By.Id("txtSearch")).SendKeys(smsdata.LastName);

            var searchicon = driver.FindElement(By.CssSelector(".fa-search"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchicon);
            Log("Search icon clicked");
        }
    }
}
