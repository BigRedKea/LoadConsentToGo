using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadConsentToGo
{
    internal class LoadSMSData
    {
        public static List<SMSData> Load(string filePath)
        {
            var smsDataList = new List<SMSData>();

            // Load SMS CSV data
            var csvData = File.ReadAllLines(filePath);
            var i = 0;

            foreach (var line in csvData)
            {
                var smsdata = new SMSData();
                var fields = line.Split(',');

                smsdata.InstanceLookup = fields[0];
                smsdata.Title = fields[1];
                smsdata.FirstName = fields[2];
                smsdata.LastName = fields[3];
                smsdata.DateofBirth = fields[4];
                smsdata.Gender = fields[5];
                smsdata.InstanceLookup = fields[8];
                smsdata.Email = fields[6];
                smsdata.SchoolYear = fields[7];
                smsdata.UniqueIdentifier = fields[8];
                smsdata.ExamNoSCSA = fields[9];
                smsdata.Address = fields[10];
                smsdata.Suburb = fields[11];
                smsdata.State = fields[12];
                smsdata.Postcode = fields[13];
                smsdata.PhoneNumber = fields[14];
                smsdata.MobileNumber = fields[15];
                smsdata.HomeRoom = fields[16];
                smsdata.House = fields[17];
                smsdata.Guardian1Title = fields[18];
                smsdata.Guardian1FirstName = fields[19];
                smsdata.Guardian1LastName = fields[20];
                smsdata.Guardian1Email = fields[21];
                smsdata.Guardian1Address = fields[22];
                smsdata.Guardian1Suburb = fields[23];
                smsdata.Guardian1State = fields[24];
                smsdata.Guardian1Postcode = fields[25];
                smsdata.Guardian1HomeNumber = fields[26];
                smsdata.Guardian1WorkNumber = fields[27];
                smsdata.Guardian1MobileNumber = fields[28];
                smsdata.Guardian2Title = fields[29];
                smsdata.Guardian2FirstName = fields[30];
                smsdata.Guardian2LastName = fields[31];
                smsdata.Guardian2Email = fields[32];
                smsdata.Guardian2Address = fields[33];
                smsdata.Guardian2Suburb = fields[34];
                smsdata.Guardian2State = fields[35];
                smsdata.Guardian2Postcode = fields[36];
                smsdata.Guardian2HomeNumber = fields[37];
                smsdata.Guardian2WorkNumber = fields[38];
                smsdata.Guardian2MobileNumber = fields[39];
                smsdata.EmergencyContactName = fields[40];
                smsdata.EmergencyContactNumber = fields[41];
                smsdata.IsEmergencyMedicalAuthorised = fields[42];
                smsdata.FamilyDoctorName = fields[43];
                smsdata.FamilyDoctorSurgeryName = fields[44];
                smsdata.FamilyDoctorAddress = fields[45];
                smsdata.FamilyDoctorSuburb = fields[46];
                smsdata.FamilyDoctorState = fields[47];
                smsdata.FamilyDoctorPostcode = fields[48];
                smsdata.FamilyDoctorNumber = fields[49];
                smsdata.SiteUniqueIdentifier = fields[50];
                smsdata.DistrictUniqueIdentifier = fields[52];
                smsdata.RegionUniqueIdentifier = fields[53];
                smsdata.BranchUniqueIdentifier = fields[54];

                // Process the data as needed
                Console.WriteLine($"Loaded {smsdata}");
                if (i>0) smsDataList.Add(smsdata);

                i++;
            }

            return smsDataList;
        }
    }
}
