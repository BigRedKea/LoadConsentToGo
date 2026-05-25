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

                
                smsdata.Title = fields[0];
                smsdata.FirstName = fields[1];
                smsdata.LastName = fields[2];
                smsdata.DateofBirth = fields[3];
                smsdata.Gender = fields[4];
                smsdata.Email = fields[5];
                smsdata.SchoolYear = fields[6];
                smsdata.UniqueIdentifier = fields[7];
                smsdata.ExamNoSCSA = fields[8];
                smsdata.Address = fields[9];
                smsdata.Suburb = fields[10];
                smsdata.State = fields[11];
                smsdata.Postcode = fields[12];
                smsdata.PhoneNumber = fields[13];
                smsdata.MobileNumber = fields[14];
                smsdata.HomeRoom = fields[15];
                smsdata.House = fields[16];
                smsdata.Guardian1Title = fields[17];
                smsdata.Guardian1FirstName = fields[18];
                smsdata.Guardian1LastName = fields[19];
                smsdata.Guardian1Email = fields[20];
                smsdata.Guardian1Address = fields[21];
                smsdata.Guardian1Suburb = fields[22];
                smsdata.Guardian1State = fields[23];
                smsdata.Guardian1Postcode = fields[24];
                smsdata.Guardian1HomeNumber = fields[25];
                smsdata.Guardian1WorkNumber = fields[26];
                smsdata.Guardian1MobileNumber = fields[27];
                smsdata.Guardian2Title = fields[28];
                smsdata.Guardian2FirstName = fields[29];
                smsdata.Guardian2LastName = fields[30];
                smsdata.Guardian2Email = fields[31];
                smsdata.Guardian2Address = fields[32];
                smsdata.Guardian2Suburb = fields[33];
                smsdata.Guardian2State = fields[34];
                smsdata.Guardian2Postcode = fields[35];
                smsdata.Guardian2HomeNumber = fields[36];
                smsdata.Guardian2WorkNumber = fields[37];
                smsdata.Guardian2MobileNumber = fields[38];
                smsdata.EmergencyContactName = fields[39];
                smsdata.EmergencyContactNumber = fields[40];
                smsdata.IsEmergencyMedicalAuthorised = fields[41];
                smsdata.FamilyDoctorName = fields[42];
                smsdata.FamilyDoctorSurgeryName = fields[43];
                smsdata.FamilyDoctorAddress = fields[44];
                smsdata.FamilyDoctorSuburb = fields[45];
                smsdata.FamilyDoctorState = fields[46];
                smsdata.FamilyDoctorPostcode = fields[47];
                smsdata.FamilyDoctorNumber = fields[48];
                smsdata.SiteUniqueIdentifier = fields[49];
                //smsdata.InstanceLookup = fields[50];
                smsdata.DistrictUniqueIdentifier = fields[51];
                smsdata.RegionUniqueIdentifier = fields[52];
                smsdata.BranchUniqueIdentifier = fields[53];

                // Process the data as needed
                Console.WriteLine($"Loaded {smsdata}");
                if (i>0) smsDataList.Add(smsdata);

                i++;
            }

            return smsDataList;
        }
    }
}
