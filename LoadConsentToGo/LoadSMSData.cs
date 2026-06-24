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
                if (i == 0 && smsdata.Title != "title") throw new Exception($"{smsdata.Title} != 'title'");

                smsdata.FirstName = fields[1];
                if (i == 0 && smsdata.FirstName != "firstname") throw new Exception($"{smsdata.FirstName} != 'firstname'");

                smsdata.LastName = fields[2];
                if (i == 0 && smsdata.LastName != "lastname") throw new Exception($"{smsdata.LastName} != 'lastname'");

                smsdata.DateofBirth = fields[3];
                if (i == 0 && smsdata.DateofBirth != "dateofbirth") throw new Exception($"{smsdata.DateofBirth} != 'dateofbirth'");

                smsdata.Gender = fields[4];
                if (i == 0 && smsdata.Gender != "gender") throw new Exception($"{smsdata.Gender} != 'gender'");

                smsdata.Email = fields[5];
                if (i == 0 && smsdata.Email != "email") throw new Exception($"{smsdata.Email} != 'email'");

                smsdata.SchoolYear = fields[6];
                if (i == 0 && smsdata.SchoolYear != "schoolyear") throw new Exception($"{smsdata.SchoolYear} != 'schoolyear'");

                smsdata.UniqueIdentifier = fields[7];
                if (i == 0 && smsdata.UniqueIdentifier != "UniqueIdentifier") throw new Exception($"{smsdata.UniqueIdentifier} != 'UniqueIdentifier'");
                
                //smsdata.ExamNoSCSA = fields[8];
                //if (i == 0 && smsdata.Title != "title") throw new Exception($"{smsdata.Title} != 'title'");
                //smsdata.Address = fields[9];
                //if (i == 0 && smsdata.Title != "title") throw new Exception($"{smsdata.Title} != 'title'");
                //smsdata.Suburb = fields[10];
                //smsdata.State = fields[11];
                //smsdata.Postcode = fields[12];
                //smsdata.PhoneNumber = fields[13];
                //smsdata.MobileNumber = fields[14];
                //smsdata.HomeRoom = fields[15];
                //smsdata.House = fields[16];

                    //guardian1mobile
                    //siteuniqueidentifier
                    //districtuniqueidentifier
                    //regionuniqueidentifier
                    //appointmentstartdate


                //smsdata.Guardian1Title = fields[17];
                smsdata.Guardian1FirstName = fields[18];
                if (i == 0 && smsdata.Guardian1FirstName != "guardian1firstname") throw new Exception($"{smsdata.Guardian1FirstName} != 'guardian1firstname'");

                smsdata.Guardian1LastName = fields[19];
                if (i == 0 && smsdata.Guardian1LastName!= "guardian1lastname") throw new Exception($"{smsdata.Guardian1LastName} != 'guardian1lastname'");

                smsdata.Guardian1Email = fields[20];
                if (i == 0 && smsdata.Guardian1Email != "guardian1email") throw new Exception($"{smsdata.Guardian1Email} != 'guardian1email'");

                //smsdata.Guardian1Address = fields[21];
                //smsdata.Guardian1Suburb = fields[22];
                //smsdata.Guardian1State = fields[23];
                //smsdata.Guardian1Postcode = fields[24];
                //smsdata.Guardian1HomeNumber = fields[25];
                //smsdata.Guardian1WorkNumber = fields[26];
                smsdata.Guardian1MobileNumber = fields[27];
                if (i == 0 && smsdata.Guardian1MobileNumber != "guardian1mobile") throw new Exception($"{smsdata.Guardian1MobileNumber} != 'guardian1mobile'");

                //smsdata.Guardian2Title = fields[28];
                //smsdata.Guardian2FirstName = fields[29];
                //smsdata.Guardian2LastName = fields[30];
                //smsdata.Guardian2Email = fields[31];
                //smsdata.Guardian2Address = fields[32];
                //smsdata.Guardian2Suburb = fields[33];
                //smsdata.Guardian2State = fields[34];
                //smsdata.Guardian2Postcode = fields[35];
                //smsdata.Guardian2HomeNumber = fields[36];
                //smsdata.Guardian2WorkNumber = fields[37];
                //smsdata.Guardian2MobileNumber = fields[38];
                //smsdata.EmergencyContactName = fields[39];
                //smsdata.EmergencyContactNumber = fields[40];
                //smsdata.IsEmergencyMedicalAuthorised = fields[41];
                //smsdata.FamilyDoctorName = fields[42];
                //smsdata.FamilyDoctorSurgeryName = fields[43];
                //smsdata.FamilyDoctorAddress = fields[44];
                //smsdata.FamilyDoctorSuburb = fields[45];
                //smsdata.FamilyDoctorState = fields[46];
                //smsdata.FamilyDoctorPostcode = fields[47];
                //smsdata.FamilyDoctorNumber = fields[48];
                smsdata.SiteUniqueIdentifier = fields[49];
                if (i == 0 && smsdata.SiteUniqueIdentifier != "siteuniqueidentifier") throw new Exception($"{smsdata.SiteUniqueIdentifier} != 'siteuniqueidentifier'");

                //smsdata.InstanceLookup = fields[50];
                //smsdata.DistrictUniqueIdentifier = fields[51];
                //smsdata.RegionUniqueIdentifier = fields[52];
                //smsdata.BranchUniqueIdentifier = fields[53];

                // Process the data as needed
                Console.WriteLine($"Loaded {smsdata}");

                // Insert Rover Adult information in as Rover Guardian information if not provided in the CSV
                if (smsdata.SchoolYear == "RS")
                {
                    if (string.IsNullOrEmpty(smsdata.Guardian1FirstName)) smsdata.Guardian1FirstName = smsdata.FirstName;
                    if (string.IsNullOrEmpty(smsdata.Guardian1LastName)) smsdata.Guardian1LastName = smsdata.LastName;
                    if (string.IsNullOrEmpty(smsdata.Guardian1Title)) smsdata.Guardian1Title = smsdata.Title;
                }

                // Remove title row
                if (i>0) smsDataList.Add(smsdata);

                i++;
            }



            return smsDataList;
        }
    }
}
