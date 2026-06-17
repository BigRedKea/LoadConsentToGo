using ExcelDataReader.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LoadConsentToGo
{
    internal class MergeExcelData
    {
        public static List<C2GDownload> Execute()
        {

            var result = new List<C2GDownload>();
            var openFileDialog = new FolderBrowserDialog()
            {
                Description = "Select the directory containing the xlsx files to merge",

                InitialDirectory = Consent2GoFunctions.consent2gopath
            };
            openFileDialog.ShowDialog();


            var files = Directory.GetFiles(openFileDialog.SelectedPath, "*.xlsx");

            var mergedData = new List<string[]>();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            foreach (var file in files)
            {
                var f = new FileInfo(file);
                if (f.Length ==0 )
                {
                    Console.WriteLine($"{file} is empty");
                    continue;
                }
                try
                {
                    var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(File.Open(file, FileMode.Open, FileAccess.Read));
                    int rowcount = 0;
                    while (reader.Read())
                    {

                        if (rowcount > 0)
                        {
                            var c2g = new C2GDownload();
                            int c = 0;
                            c2g.UniqueIdentifier = reader.GetString(c++);
                            c2g.Title = reader.GetString(c++);
                            c2g.FirstName = reader.GetString(c++);
                            c2g.LastName = reader.GetString(c++);
                            c2g.Email = reader.GetString(c++);
                            c2g.Gender = reader.GetString(c++);
                            c2g.BirthDate = reader.GetString(c++);
                            c2g.SchoolYear = reader.GetString(c++);
                            c2g.HomeRoom = reader.GetString(c++);
                            c2g.Parent1LastUpdated = reader.GetString(c++);
                            c2g.Parent2LastUpdated = reader.GetString(c++);
                            c2g.AboriginalOrTorresStrait = reader.GetString(c++);
                            c2g.AdditionalPlayingHistory = reader.GetString(c++);
                            c2g.Address = reader.GetString(c++);
                            c2g.Age = reader.GetString(c++);
                            c2g.Boarder = reader.GetString(c++);
                            c2g.BoarderHouse = reader.GetString(c++);
                            c2g.CampusName = reader.GetString(c++);
                            c2g.ColourBlind = reader.GetString(c++);
                            c2g.CountryOfBirth = reader.GetString(c++);
                            c2g.CreationDate = reader.GetString(c++);
                            c2g.CurriculumNumber = reader.GetString(c++);
                            c2g.DietaryNonMedical1 = reader.GetString(c++);
                            c2g.DietaryNonMedical2 = reader.GetString(c++);
                            c2g.DietaryNonMedical = reader.GetString(c++);
                            c2g.DoctorAddress = reader.GetString(c++);
                            c2g.DoctorName = reader.GetString(c++);
                            c2g.DoctorNumber = reader.GetString(c++);
                            c2g.DoctorPostcode = reader.GetString(c++);
                            c2g.DoctorPracticeName = reader.GetString(c++);
                            c2g.DoctorState = reader.GetString(c++);
                            c2g.DoctorSuburb = reader.GetString(c++);
                            c2g.EmergencyName = reader.GetString(c++);
                            c2g.EmergencyNumber = reader.GetString(c++);
                            c2g.Emergency2Name = reader.GetString(c++);
                            c2g.Emergency2Number = reader.GetString(c++);
                            c2g.Emergency3Name = reader.GetString(c++);
                            c2g.Emergency3Number = reader.GetString(c++);
                            c2g.ExcursionBlocked = reader.GetString(c++);
                            c2g.ExcursionBlockedComment = reader.GetString(c++);
                            c2g.ExcursionBlockedReason = reader.GetString(c++);
                            c2g.Guardian1Email = reader.GetString(c++);
                            c2g.Guardian1EmailVerified = reader.GetString(c++);
                            c2g.Guardian1FirstName = reader.GetString(c++);
                            c2g.Guardian1HomeNumber = reader.GetString(c++);
                            c2g.Guardian1ID = reader.GetString(c++);
                            c2g.Guardian1LastName = reader.GetString(c++);
                            c2g.Guardian1MobileNumber = reader.GetString(c++);
                            c2g.Guardian1ResidentialAddress = reader.GetString(c++);
                            c2g.Guardian1ResidentialPostcode = reader.GetString(c++);
                            c2g.Guardian1ResidentialState = reader.GetString(c++);
                            c2g.Guardian1ResidentialSuburb = reader.GetString(c++);
                            c2g.Guardian1ShareInformation = reader.GetString(c++);
                            c2g.Guardian1Title = reader.GetString(c++);
                            c2g.Guardian1WorkNumber = reader.GetString(c++);
                            c2g.Guardian2Email = reader.GetString(c++);
                            c2g.Guardian2EmailVerified = reader.GetString(c++);
                            c2g.Guardian2FirstName = reader.GetString(c++);
                            c2g.Guardian2HomeNumber = reader.GetString(c++);
                            c2g.Guardian2ID = reader.GetString(c++);
                            c2g.Guardian2LastName = reader.GetString(c++);
                            c2g.Guardian2MobileNumber = reader.GetString(c++);
                            c2g.Guardian2ResidentialAddress = reader.GetString(c++);
                            c2g.Guardian2ResidentialPostcode = reader.GetString(c++);
                            c2g.Guardian2ResidentialState = reader.GetString(c++);
                            c2g.Guardian2ResidentialSuburb = reader.GetString(c++);
                            c2g.Guardian2ShareInformation = reader.GetString(c++);
                            c2g.Guardian2Title = reader.GetString(c++);
                            c2g.Guardian2WorkNumber = reader.GetString(c++);
                            c2g.House = reader.GetString(c++);
                            c2g.LanguageSpokenAtHome = reader.GetString(c++);
                            c2g.LanguageSpokenAtHomeId = reader.GetString(c++);
                            c2g.MedicAlertBracelet = reader.GetString(c++);
                            c2g.MiddleName = reader.GetString(c++);
                            c2g.MobileNumber = reader.GetString(c++);
                            c2g.ModifiedDate = reader.GetString(c++);
                            c2g.Paid = reader.GetString(c++);
                            c2g.PermissionAntiHistamine = reader.GetString(c++);
                            c2g.PermissionAntiInflammatory = reader.GetString(c++);
                            c2g.PermissionEmergencyMedical = reader.GetString(c++);
                            c2g.PermissionIbuprofen = reader.GetString(c++);
                            c2g.PermissionParacetamol = reader.GetString(c++);
                            c2g.PhoneNumber = reader.GetString(c++);
                            c2g.Postcode = reader.GetString(c++);
                            c2g.SportFirstChoice = reader.GetString(c++);
                            c2g.SportSecondChoice = reader.GetString(c++);
                            c2g.State = reader.GetString(c++);
                            c2g.StudentGroupsList = reader.GetString(c++);
                            c2g.Suburb = reader.GetString(c++);
                            c2g.SwimmingAbility = reader.GetString(c++);
                            c2g.SwimmingComments = reader.GetString(c++);
                            c2g.TeamsList = reader.GetString(c++);
                            c2g.WearsContacts = reader.GetString(c++);
                            c2g.WearsGlasses = reader.GetString(c++);
                            c2g.WearsHearingAid = reader.GetString(c++);
                            c2g.C2gSiteIdentifier = null;

                            result.Add(c2g);
                        }
                        rowcount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading file {file} {f.Length}: {ex.Message}");
                }
            }

            return result;

            //// Write merged data to a new CSV file
            //var outputFilePath = Path.Combine(downloaddirectory, "MergedData.csv");
            //using (var writer = new StreamWriter(outputFilePath))
            //{
            //    // Write header
            //    writer.WriteLine("Column1,Column2,Column3"); // Adjust header as needed

            //    // Write merged data
            //    foreach (var row in mergedData)
            //    {
            //        writer.WriteLine(string.Join(",", row));
            //    }
            //}

            //Console.WriteLine($"Merged data has been written to {outputFilePath}");
        }

    }
}
