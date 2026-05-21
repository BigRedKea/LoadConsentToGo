using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
