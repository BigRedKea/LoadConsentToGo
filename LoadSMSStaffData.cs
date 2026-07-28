

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


                switch (systemuser.Role)
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
                        systemuser.consent2gorole = "Cub Scout Section";
                        break;

                    case "Full System Administration":
                        systemuser.consent2gorole = "Full System Administration";
                        break;

                    case "Group Committee / Other Adults":
                        systemuser.consent2gorole = "Group Committee / Other Adults";
                        break;

                    case "Assistant Group Leader":
                    case "Group Leader":
                    case "Group Leader / LIC":
                        systemuser.consent2gorole = "Group Leader / LIC";
                        break;

                    case "Assistant Joey Scout Leader":
                    case "Joey Scout Leader":
                    case "Joey Scout Section":
                        systemuser.consent2gorole = "Joey Scout Section";
                        break;

                    case "Rover Scout Section":
                        systemuser.consent2gorole = "Rover Scout Section";
                        break;

                    case "Assistant Scout Leader":
                    case "Scout Leader":
                    case "Scout Section":
                        systemuser.consent2gorole = "Scout Section";
                        break;

                    case "Assistant Venturer Scout Leader":
                    case "Venturer Scout Leader":
                    case "Venturer Scout Section":
                        systemuser.consent2gorole = "Venturer Scout Section";
                        break;

                    default:
                        break;

                }

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
