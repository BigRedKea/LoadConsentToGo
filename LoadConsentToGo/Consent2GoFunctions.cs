using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V145.Audits;
using System.Linq;
using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LoadConsentToGo
{
    internal class Consent2GoFunctions
    {
        IWebDriver driver = new ChromeDriver();
        public int emailcounter = 0;

        public static string consent2gopath = @"C:\Consent2Go";

        private string LogFilePath = Path.Combine(consent2gopath, $"consent2golog{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.log");

        public GroupLookupData GroupName { get; private set; }

        public Consent2GoFunctions()
        {
            driver = new ChromeDriver();
            Log("Consent2GoFunctions initialized");
        }

        private void Log(string message)
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

        public void Process(SMSData smsdata, List<GroupLookupData> GroupLookup, int cnt)
        {
            Log($"Process start: {smsdata?.FirstName} {smsdata?.LastName} Site:{smsdata?.SiteUniqueIdentifier} Count:{cnt}");

            if (string.IsNullOrEmpty(smsdata.SiteUniqueIdentifier))
            {
                Log("No unique identifier found");
                Console.WriteLine($"No unique identifier found ");
                return;
            }

            var lookup = GroupLookup.Where(x => x.SMSOrgId == smsdata.SiteUniqueIdentifier).FirstOrDefault();

            if (lookup == null)
            {
                Log($"No lookup found for {smsdata.SiteUniqueIdentifier}");
                Console.WriteLine($"No lookup found for {smsdata.SiteUniqueIdentifier}");
                return;
            }

            smsdata.Grouplookup = lookup;

            emailcounter++;

            var url = ($"https://www.mcbschools.com/DuplicateIndex?Id={lookup.Consent2GoOrgId}");
            driver.Navigate().GoToUrl(url);
            Log($"Navigated to {url}");
            Thread.Sleep(2000);

            CheckExists(smsdata, lookup, cnt);


            //    var alreadyexists = MessageBox.Show($"{cnt} Does {smsdata.FirstName} {smsdata.LastName} of {lookup.FormationName} already exist?", "Exists?", MessageBoxButtons.YesNo);
            //Log($"CheckExists result: {(alreadyexists == DialogResult.Yes)}");
            //return (alreadyexists == DialogResult.Yes);
            //{
            Log($"CheckExists returned true for {smsdata.FirstName} {smsdata.LastName}");
            var frm = new FormSMSData();
            frm.LoadSMSData(smsdata);

            // 1. Tell Windows Forms you want to set the coordinates manually
            frm.StartPosition = FormStartPosition.Manual;

            // 2. Set the X and Y coordinates (in pixels) from the top-left of the screen
            frm.Location = new Point(3000, 600);

            var rslt = frm.ShowDialog();

            if (rslt == DialogResult.OK) return;
            //}

            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/AddEditPlayer");
            Log("Navigated to AddEditPlayer form");

            driver.FindElement(By.Id("txtFirstName")).SendKeys(smsdata.FirstName);
            driver.FindElement(By.Id("txtLastName")).SendKeys(smsdata.LastName);
            driver.FindElement(By.Id("ddlTitle")).SendKeys(smsdata.Title);
            driver.FindElement(By.Id("txtBirthDate")).SendKeys(smsdata.DateofBirth);
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

            driver.FindElement(By.Id("liAdditionalDetails")).Click();
            driver.FindElement(By.Id("txtGuardianName")).SendKeys(smsdata.Guardian1FirstName);
            driver.FindElement(By.Id("txtGuardianLastName")).SendKeys(smsdata.Guardian1LastName);
            driver.FindElement(By.Id("ddlGuardianTitle")).SendKeys(smsdata.Guardian1Title);
            driver.FindElement(By.Id("txtGuardianMobileNumber")).SendKeys(smsdata.Guardian1MobileNumber);
            driver.FindElement(By.Id("txtGuardianEmail")).SendKeys(smsdata.Guardian1Email);

            var commit = MessageBox.Show($"Ok to Commit {smsdata.FirstName} {smsdata.LastName} of {lookup.FormationName}", "Exists?", MessageBoxButtons.YesNo);

            if (commit == DialogResult.Yes)
            {
                driver.FindElement(By.Id("btnSave")).Click();
                Log("Member committed by user confirmation");
                Console.Write("Member Committed");
            }

            Log($"Process finished for {smsdata.FirstName} {smsdata.LastName}");
        }

        public void CheckExists(SMSData smsdata, GroupLookupData lookup, int cnt)
        {
            Log($"CheckExists: Searching for {smsdata.FirstName} {smsdata.LastName} (count {cnt}) in {lookup?.FormationName}");
            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/Player");

            driver.FindElement(By.Id("txtSearch")).SendKeys(smsdata.LastName);

            var searchicon = driver.FindElement(By.CssSelector(".fa-search"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchicon);
            Log("Search icon clicked");
        }
    }
}
