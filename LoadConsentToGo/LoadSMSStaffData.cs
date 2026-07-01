

namespace LoadConsentToGo
{
    internal class LoadSMSStaffData
    {
        public static List<SystemUser> Load(string filePath)
        {
            var results = new List<SystemUser>();

            // Load SMS CSV data
            var csvData = File.ReadAllLines(filePath);
            var i = 0;

            foreach (var line in csvData)
            {
                var systemuser = new SystemUser();
                var fields = line.Split(',');

                systemuser.FirstName = Clean(fields[0]);
                if (i == 0 && systemuser.FirstName != "firstname") throw new Exception($"{systemuser.FirstName} != 'firstname'");

                systemuser.LastName = Clean(fields[1]);
                if (i == 0 && systemuser.LastName != "lastname") throw new Exception($"{systemuser.LastName} != 'lastname'");

                systemuser.Email = Clean(fields[2]);
                if (i == 0 && systemuser.Email != "email") throw new Exception($"{systemuser.Email} != 'email'");

                systemuser.Role = Clean(fields[3]);
                if (i == 0 && systemuser.Role != "userrole") throw new Exception($"{systemuser.Role} != 'userrole'");

                systemuser.SiteIdentifier = Clean(fields[4]);
                if (i == 0 && systemuser.SiteIdentifier != "site_unique_identifier") throw new Exception($"{systemuser.SiteIdentifier} != 'site_unique_identifier'");
                

                // Remove title row
                if (i>0) results.Add(systemuser);

                i++;
            }



            return results;
        }

       static string Clean(string instring)
        {
            return instring.Replace("/", string.Empty).Replace("\"", string.Empty);
        }
    }
}
