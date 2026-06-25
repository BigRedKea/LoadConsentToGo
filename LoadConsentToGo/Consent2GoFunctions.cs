using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoadConsentToGo
{
    internal class Consent2GoFunctions
    {
        private readonly ChromeDriver driver = new();
        private int emailcounter = 0;

        internal static string profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();

        internal static string consent2gopath = Path.Combine(profilePath, @"Scouts Queensland", "SO Admin - Consent2Go", "Automated Upload");
        internal static string consent2gopathupload = Path.Combine(consent2gopath, "Uploads");
        internal static string consent2gopathuploadlog = Path.Combine(consent2gopathupload, "Logs");
        internal static string consent2gopathupdownloads = Path.Combine(consent2gopathupload, "Downloads");

        internal static string consent2gopathlookup = Path.Combine(consent2gopath, "Lookups");
        internal static string consent2gopathdatabase = Path.Combine(consent2gopath, "Database");

        internal GroupLookupData? GroupName { get; private set; }

        internal Consent2GoFunctions()
        {
            driver = new ChromeDriver();
            Logging.Instance.Log("Consent2GoFunctions initialized");
        }


        internal void Open()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        internal void Login(string username, string password)
        {
            Logging.Instance.Log($"Login start for user '{username}'");
            driver.Navigate().GoToUrl("https://www.mcbschools.com/Login");

            var loginBox = driver.FindElement(By.Name("username"));
            loginBox.SendKeys(username);
            var submitButton = driver.FindElement(By.ClassName("amplify-button"));
            submitButton.Click();
            Thread.Sleep(500);

            driver.FindElement(By.Name("password")).SendKeys(password);
            submitButton.Click();
            Thread.Sleep(500);
            Logging.Instance.Log("Login sequence completed");
        }


        internal void DownloadGroupData(GroupLookupData lookup)
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
            }
            catch (Exception ex)
            {
                Logging.Instance.Log($"Exception {ex.Message}");
            }
        }

        internal void OpenGroup(GroupLookupData lookup)
        {
            var url = $"https://www.mcbschools.com/DuplicateIndex?Id={lookup.Consent2GoOrgId}";
            driver.Navigate().GoToUrl(url);
            Logging.Instance.Log($"Navigated to {url}");
            Thread.Sleep(2000);
        }

        internal static int frmTop = 500;
        internal static int frmLeft = 1000;

        internal void Process(C2GData smsdata, int cnt)
        {
            if (smsdata == null)
            {
                Logging.Instance.Log($"smsdata is null");
                return;
            }

            Logging.Instance.Log($"Process start: {smsdata?.FirstName} {smsdata?.LastName} Site:{smsdata?.SiteUniqueIdentifier} Count:{cnt}");

            emailcounter++;

            if (CheckExists(smsdata, cnt)) return;

            //Otherwise add the record

            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/AddEditPlayer");
            Logging.Instance.Log("Navigated to AddEditPlayer form");

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
            Logging.Instance.Log($"Email set to {email}");

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

            var commit = MessageBox.Show($"Ok to Commit {smsdata.FirstName} {smsdata.LastName} of {smsdata?.Grouplookup?.FormationName}", "Exists?", MessageBoxButtons.YesNo);

            if (commit == DialogResult.Yes)
            {
                driver.FindElement(By.Id("btnSave")).Click();
                Logging.Instance.Log("Member committed by user confirmation");
                Console.Write("Member Committed");
            }

            Logging.Instance.Log($"Process finished for {smsdata?.FirstName} {smsdata?.LastName}");
        }

        internal bool CheckExists(C2GData smsdata, int cnt)
        {
            Logging.Instance.Log($"CheckExists: Searching for {smsdata.FirstName} {smsdata.LastName} (count {cnt}) in {smsdata.Grouplookup?.FormationName}");

            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/Player");
            driver.FindElement(By.Id("txtSearch")).SendKeys(smsdata.LastName);

            var searchicon = driver.FindElement(By.CssSelector(".fa-search"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchicon);
            Logging.Instance.Log("Search icon clicked");

            var frm = new FormSMSData();
            frm.LoadSMSData(smsdata);

            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(frmLeft, frmTop);
            var rslt = frm.ShowDialog();

            frmTop = frm.Top;
            frmLeft = frm.Left;

            return rslt == DialogResult.Yes;
        }


        internal void LoadConsentToGo(List<C2GData> datatoload)
        {
            var grpbysmsdata = datatoload.GroupBy(x => x.SiteUniqueIdentifier);

            int cnt = 0;
            foreach (var formationdata in grpbysmsdata)
            {
                var formationlookup = formationdata.FirstOrDefault()?.Grouplookup;
                if (formationlookup != null)
                {
                    Logging.Instance.Log($"{formationlookup.FormationName}");
                    OpenGroup(formationlookup);

                    foreach (var item in formationdata.OrderBy(x => x.LastName))
                    {
                        try
                        {
                            cnt++;
                            Logging.Instance.Log($"Processing {cnt}/ {datatoload.Count} {item}");
                            Process(item, cnt);
                        }
                        catch (Exception ex)
                        {
                            Logging.Instance.Log(ex.ToString(), true);
                        }
                    }

                    DownloadGroupData(formationlookup);
                }
            }
        }
    }
}
