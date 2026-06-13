using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadConsentToGo
{
    internal class SMSData
    {
        internal string Address;
        internal string FamilyDoctorState;

        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string PhoneNumber { get; internal set; }
        //public string InstanceLookup { get; internal set; }
        public string Title { get; internal set; }
        public string DateofBirth { get; internal set; }
        public string Gender { get; internal set; }
        public string Email { get; internal set; }
        public string SchoolYear { get; internal set; }
        public string UniqueIdentifier { get; internal set; }
        //public string ExamNoSCSA { get; internal set; }
        //public string Suburb { get; internal set; }
        //public string State { get; internal set; }
        //public string Postcode { get; internal set; }
        //public string MobileNumber { get; internal set; }
        //public string HomeRoom { get; internal set; }
        //public string House { get; internal set; }
        public string Guardian1Title { get; internal set; }
        public string Guardian1FirstName { get; internal set; }
        public string Guardian1LastName { get; internal set; }
        public string Guardian1Email { get; internal set; }
        //public string Guardian1Address { get; internal set; }
        //public string Guardian1Suburb { get; internal set; }
        //public string Guardian1State { get; internal set; }
        //public string Guardian1Postcode { get; internal set; }
        //public string Guardian1HomeNumber { get; internal set; }
        //public string Guardian1WorkNumber { get; internal set; }
        public string Guardian1MobileNumber { get; internal set; }
        //public string Guardian2Title { get; internal set; }
        //public string Guardian2FirstName { get; internal set; }
        //public string Guardian2LastName { get; internal set; }
        //public string Guardian2Address { get; internal set; }
        //public string Guardian2Suburb { get; internal set; }
        //public string Guardian2Email { get; internal set; }
        //public string Guardian2State { get; internal set; }
        //public string Guardian2Postcode { get; internal set; }
        //public string Guardian2HomeNumber { get; internal set; }
        //public string Guardian2WorkNumber { get; internal set; }
        //public string Guardian2MobileNumber { get; internal set; }
        //public string EmergencyContactName { get; internal set; }
        //public string EmergencyContactNumber { get; internal set; }
        //public string IsEmergencyMedicalAuthorised { get; internal set; }
        //public string FamilyDoctorName { get; internal set; }
        //public string FamilyDoctorSurgeryName { get; internal set; }
        //public string FamilyDoctorAddress { get; internal set; }
        //public string FamilyDoctorSuburb { get; internal set; }
        //public string FamilyDoctorPostcode { get; internal set; }
        //public string FamilyDoctorNumber { get; internal set; }
        public string SiteUniqueIdentifier { get; internal set; }
        //public string DistrictUniqueIdentifier { get; internal set; }
        //public string RegionUniqueIdentifier { get; internal set; }
        //public string BranchUniqueIdentifier { get; internal set; }
        public GroupLookupData Grouplookup { get; internal set; }

        public string toString()
        {
            return $"{Title} {FirstName} {LastName} {PhoneNumber} ";
        }
    }
}
