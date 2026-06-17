using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadConsentToGo
{
    internal class SMSData
    {
        public string Address { get; set; }

        public string FamilyDoctorState { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        //public string InstanceLookup { get; set; }
        public string Title { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string SchoolYear { get; set; }
        public string UniqueIdentifier { get; set; }
        //public string ExamNoSCSA { get; set; }
        //public string Suburb { get; set; }
        //public string State { get; set; }
        //public string Postcode { get; set; }
        //public string MobileNumber { get; set; }
        //public string HomeRoom { get; set; }
        //public string House { get; set; }
        public string Guardian1Title { get; set; }
        public string Guardian1FirstName { get; set; }
        public string Guardian1LastName { get; set; }
        public string Guardian1Email { get; set; }
        //public string Guardian1Address { get; set; }
        //public string Guardian1Suburb { get; set; }
        //public string Guardian1State { get; set; }
        //public string Guardian1Postcode { get; set; }
        //public string Guardian1HomeNumber { get; set; }
        //public string Guardian1WorkNumber { get; set; }
        public string Guardian1MobileNumber { get; set; }
        //public string Guardian2Title { get; set; }
        //public string Guardian2FirstName { get; set; }
        //public string Guardian2LastName { get; set; }
        //public string Guardian2Address { get; set; }
        //public string Guardian2Suburb { get; set; }
        //public string Guardian2Email { get; set; }
        //public string Guardian2State { get; set; }
        //public string Guardian2Postcode { get; set; }
        //public string Guardian2HomeNumber { get; set; }
        //public string Guardian2WorkNumber { get; set; }
        //public string Guardian2MobileNumber { get; set; }
        //public string EmergencyContactName { get; set; }
        //public string EmergencyContactNumber { get; set; }
        //public string IsEmergencyMedicalAuthorised { get; set; }
        //public string FamilyDoctorName { get; set; }
        //public string FamilyDoctorSurgeryName { get; set; }
        //public string FamilyDoctorAddress { get; set; }
        //public string FamilyDoctorSuburb { get; set; }
        //public string FamilyDoctorPostcode { get; set; }
        //public string FamilyDoctorNumber { get; set; }
        public string SiteUniqueIdentifier { get; set; }
        //public string DistrictUniqueIdentifier { get; set; }
        //public string RegionUniqueIdentifier { get; set; }
        //public string BranchUniqueIdentifier { get; set; }
        public GroupLookupData Grouplookup { get; set; }

        public string toString()
        {
            return $"{Title} {FirstName} {LastName} {PhoneNumber} ";
        }
    }
}
