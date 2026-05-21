using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V145.Audits;
using System.Linq;

namespace LoadConsentToGo
{
    internal class Consent2GoFunctions
    {
        IWebDriver driver = new ChromeDriver();
        public int emailcounter = 0;

        public Consent2GoFunctions() {
            driver = new ChromeDriver();
        }
        public void Open()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        public void Login(string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.mcbschools.com/Login");

            var loginBox = driver.FindElement(By.Name("username"));
            loginBox.SendKeys(username);
            var submitButton = driver.FindElement(By.ClassName("amplify-button"));
            submitButton.Click();
            Thread.Sleep(500);

            driver.FindElement(By.Name("password")).SendKeys(password);
            submitButton.Click();
            Thread.Sleep(500);

        }

        public void Process(SMSData smsdata, List<GroupLookupData> GroupLookup)
        {
            var lookup = GroupLookup.Where(x => x.SMSOrgId == smsdata.SiteUniqueIdentifier).FirstOrDefault();

            if (lookup == null)
            {
                Console.WriteLine($"No lookup found for {smsdata.SiteUniqueIdentifier}");
                return;
            }

            //MessageBox.Show($"Processing {smsdata.FirstName} {smsdata.LastName} for {lookup.FormationName}","Info",MessageBoxButtons.OK);

            // Navigate to the organisation page
            var url = ($"https://www.mcbschools.com/DuplicateIndex?Id={lookup.Consent2GoOrgId}");
            driver.Navigate().GoToUrl(url);

            if (CheckExists(smsdata, lookup))
            {
                return;
            }

            Thread.Sleep(2000);

            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/AddEditPlayer");

            driver.FindElement(By.Id("txtFirstName")).SendKeys(smsdata.FirstName);
            driver.FindElement(By.Id("txtLastName")).SendKeys(smsdata.LastName);
            driver.FindElement(By.Id("ddlTitle")).SendKeys(smsdata.Title);
            driver.FindElement(By.Id("txtBirthDate")).SendKeys(smsdata.DateofBirth);
            driver.FindElement(By.Id("txtRegistration")).SendKeys(smsdata.UniqueIdentifier);
            
            // driver.FindElement(By.Id("txtEmail")).SendKeys(smsdata.Email);
            

            // bug with Consent2go... won't allow multiple emails with the same address to the same site
            emailcounter++;
            var email = smsdata.Email;
            switch (emailcounter % 4)
            {
                case 0:
                    email = "noemails@mcbschools.au";
                    break;
                case 1:
                    email = "noemails@mcbschools.com.au";
                    break;
                case 2:
                    email = "noemails@mcbschools.jp";
                    break;
                case 3:
                    email = "noemails@mcbschools.fr";
                    break;
                default:
                    email = "noemails@mcbschools.net";
                    break;
            }

            driver.FindElement(By.Id("txtEmail")).SendKeys(email);

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

            // Change to the parents details tab
            driver.FindElement(By.Id("liAdditionalDetails")).Click();

            //Fill in the parent details
            driver.FindElement(By.Id("txtGuardianName")).SendKeys(smsdata.Guardian1FirstName);
            driver.FindElement(By.Id("txtGuardianLastName")).SendKeys(smsdata.Guardian1LastName);
            driver.FindElement(By.Id("ddlGuardianTitle")).SendKeys(smsdata.Guardian1WorkNumber);
            driver.FindElement(By.Id("txtGuardianMobileNumber")).SendKeys(smsdata.Guardian1MobileNumber);
            driver.FindElement(By.Id("txtGuardianEmail")).SendKeys(smsdata.Guardian1MobileNumber);

            var commit = MessageBox.Show($"Ok to Commit {smsdata.FirstName} {smsdata.LastName} of {lookup.FormationName}", "Exists?", MessageBoxButtons.YesNo);

            if (commit == DialogResult.Yes)
            {
                driver.FindElement(By.Id("btnSave")).Click();
            }

            

            //var submitButton = driver.FindElement(By.TagName("button"));

            // linkText = Additional Details OK

            //click on id = ddlGuardianTitle OK
            //select on id = ddlGuardianTitle with value label = Mr OK
            //click on id = txtGuardianName OK
            //type on id = txtGuardianName with value Chris OK
            //click on id = txtGuardianLastName OK
            //type on id = txtGuardianLastName with value Noonan OK
            //click on id = txtGuardianEmail OK
            //type on id = txtGuardianEmail with value chris @noonanfamily.org OK
            //click on id = txtGuardianMobileNumber OK
            //type on id = txtGuardianMobileNumber with value 0439 744 201 OK
            //click on id = btnSave OK
            //mouseOver on id = btnSave OK
            //mouseOut on id = btnSave OK
            //click on id = ddlSchoolYear
        }

        public bool CheckExists(SMSData smsdata, GroupLookupData lookup)
        {
            //Search for the student by last name
            driver.Navigate().GoToUrl("https://www.mcbschools.com/School/Player");

            driver.FindElement(By.Id("txtSearch")).SendKeys(smsdata.LastName);

            //MessageBox.Show($"Waiting", "Wating?", MessageBoxButtons.YesNo);

            //Commit search
            var searchicon = driver.FindElement(By.CssSelector(".fa-search"));
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", searchicon);

            var alreadyexists = MessageBox.Show($"Does {smsdata.FirstName} {smsdata.LastName} of {lookup.FormationName} already exist?", "Exists?", MessageBoxButtons.YesNo);
            return (alreadyexists == DialogResult.Yes);

        }
    }


}
