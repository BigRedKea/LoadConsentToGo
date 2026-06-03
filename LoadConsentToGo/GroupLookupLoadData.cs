using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoadConsentToGo
{
    internal class GroupLookupLoadData
    {
        public static List<GroupLookupData> Load(string filename)
        {
            // Read from Json file and load into list

            var jsonData = File.ReadAllText(filename);
            var result = JsonConvert.DeserializeObject<List<GroupLookupData>>(jsonData);

            return result;
        }

        internal static void WriteToCSV(List<GroupLookupData> lookup, string filePath)
        {

            
            // 1. Initialize FileStream with specific file modes and permissions
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // 2. Wrap the FileStream inside a StreamWriter to write text
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in lookup)
                    {
                        var url = ($"https://www.mcbschools.com/DuplicateIndex?Id={item.Consent2GoOrgId}");
                        writer.WriteLine($"{url} , {item.FormationName} ");
                    }
                }
            }
        }

    }
}
