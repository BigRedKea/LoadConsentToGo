
namespace LoadConsentToGo
{
    internal class LoadSMSData
    {
        public static List<C2GData> Load()
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
                Logging.Instance.Log("No file selected", true);
                throw new Exception("No file selected");
            }

            return Load(openFileDialog.FileName);
        }

        public static List<C2GData> Load(string filePath)
        {
            var smsDataList = new List<C2GData>();


            // Load SMS CSV data
            var csvData = File.ReadAllLines(filePath);
            var i = 0;

            foreach (var line in csvData)
            {
                var smsdata = new C2GData();
                var fields = line.Split(',');

                smsdata.Title = Clean(fields[0]);
                if (i == 0 && smsdata.Title != "title") throw new Exception($"{smsdata.Title} != 'title'");

                smsdata.FirstName = Clean(fields[1]);
                if (i == 0 && smsdata.FirstName != "firstname") throw new Exception($"{smsdata.FirstName} != 'firstname'");

                smsdata.LastName = Clean(fields[2]);
                if (i == 0 && smsdata.LastName != "lastname") throw new Exception($"{smsdata.LastName} != 'lastname'");

                smsdata.BirthDate = Clean(fields[3]);
                if (i == 0 && smsdata.BirthDate != "dateofbirth") throw new Exception($"{smsdata.BirthDate} != 'dateofbirth'");

                smsdata.Gender = Clean(fields[4]);
                if (i == 0 && smsdata.Gender != "gender") throw new Exception($"{smsdata.Gender} != 'gender'");

                smsdata.Email = Clean(fields[5]);
                if (i == 0 && smsdata.Email != "email") throw new Exception($"{smsdata.Email} != 'email'");

                smsdata.SchoolYear = Clean(fields[6]);
                if (i == 0 && smsdata.SchoolYear != "schoolyear") throw new Exception($"{smsdata.SchoolYear} != 'schoolyear'");

                smsdata.UniqueIdentifier = Clean(fields[7]);
                if (i == 0 && smsdata.UniqueIdentifier != "UniqueIdentifier") throw new Exception($"{smsdata.UniqueIdentifier} != 'UniqueIdentifier'");
               
                smsdata.Guardian1FirstName = Clean(fields[18]);
                if (i == 0 && smsdata.Guardian1FirstName != "guardian1firstname") throw new Exception($"{smsdata.Guardian1FirstName} != 'guardian1firstname'");

                smsdata.Guardian1LastName = Clean(fields[19]);
                if (i == 0 && smsdata.Guardian1LastName!= "guardian1lastname") throw new Exception($"{smsdata.Guardian1LastName} != 'guardian1lastname'");

                smsdata.Guardian1Email = Clean(fields[20]);
                if (i == 0 && smsdata.Guardian1Email != "guardian1email") throw new Exception($"{smsdata.Guardian1Email} != 'guardian1email'");

                smsdata.Guardian1MobileNumber = Clean(fields[27]);
                if (i == 0 && smsdata.Guardian1MobileNumber != "guardian1mobile") throw new Exception($"{smsdata.Guardian1MobileNumber} != 'guardian1mobile'");

                smsdata.SiteUniqueIdentifier = Clean(fields[49]);
                if (i == 0 && smsdata.SiteUniqueIdentifier != "siteuniqueidentifier") throw new Exception($"{smsdata.SiteUniqueIdentifier} != 'siteuniqueidentifier'");

                // Insert Rover Adult information in as Rover Guardian information if not provided in the CSV
                if (smsdata.SchoolYear == "RS")
                {
                    if (string.IsNullOrEmpty(smsdata.Guardian1FirstName)) smsdata.Guardian1FirstName = smsdata.FirstName;
                    if (string.IsNullOrEmpty(smsdata.Guardian1LastName)) smsdata.Guardian1LastName = smsdata.LastName;
                    if (string.IsNullOrEmpty(smsdata.Guardian1Title)) smsdata.Guardian1Title = smsdata.Title;
                }

                if (string.IsNullOrEmpty(smsdata.Guardian1FirstName) || string.IsNullOrEmpty(smsdata.Guardian1LastName) || string.IsNullOrEmpty(smsdata.Guardian1MobileNumber))
                {
                    Logging.Instance.Log($"Warning: Guardian information is missing for {smsdata.UniqueIdentifier} {smsdata.FirstName} {smsdata.LastName}. Please check SMS has loaded correctly.");
                }

                // Ignore title row
                if (i > 0)
                {
                    smsDataList.Add(smsdata);
                    // Process the data as needed
                    Logging.Instance.Log($"Loaded {smsdata} from {filePath}");
                }

                i++;
            }



            return smsDataList;
        }

       static string Clean(string instring)
        {
            return instring.Replace("/", string.Empty).Replace("\"", string.Empty);
        }
    }
}
