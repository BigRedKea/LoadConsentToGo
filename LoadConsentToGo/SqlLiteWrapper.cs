
using LoadConsentToGo;
using Microsoft.Data.Sqlite;
using OpenQA.Selenium.BiDi.Script;
using OpenQA.Selenium.DevTools.V147.DOM; // Or System.Data.SQLite


namespace LoadConsentToGo
{

    public class SqlLiteWrapper : IDisposable
    {

        SqliteConnection connection;
        private bool disposedValue;
        private bool disposedValue1;

        internal SqlLiteWrapper(string dbfilepath)
        {
            string connectionString = $"Data Source={dbfilepath};";
            connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        internal static void CreateTable(SqliteConnection connection)
        {
            // 1. Setup a test table with a UNIQUE constraint
            string createTableSql = @" CREATE TABLE IF NOT EXISTS Consent2GoProfiles (
                        SqlId INTEGER DEFAULT 1 PRIMARY KEY,
                        UniqueIdentifier TEXT,
                        Title TEXT,
                        FirstName TEXT,
                        LastName TEXT,
                        Email TEXT,
                        Gender TEXT,
                        BirthDate TEXT,
                        SchoolYear TEXT,
                        HomeRoom TEXT,
                        Parent1LastUpdated TEXT,
                        Parent2LastUpdated TEXT,
                        AboriginalOrTorresStrait TEXT,
                        AdditionalPlayingHistory TEXT,
                        Address TEXT,
                        Age TEXT,
                        Boarder TEXT,
                        BoarderHouse TEXT,
                        CampusName TEXT,
                        ColourBlind TEXT,
                        CountryOfBirth TEXT,
                        CreationDate TEXT,
                        CurriculumNumber TEXT,
                        DietaryNonMedical1 TEXT,
                        DietaryNonMedical2 TEXT,
                        DietaryNonMedical TEXT,
                        DoctorAddress TEXT,
                        DoctorName TEXT,
                        DoctorNumber TEXT,
                        DoctorPostcode TEXT,
                        DoctorPracticeName TEXT,
                        DoctorState TEXT,
                        DoctorSuburb TEXT,
                        EmergencyName TEXT,
                        EmergencyNumber TEXT,
                        Emergency2Name TEXT,
                        Emergency2Number TEXT,
                        Emergency3Name TEXT,
                        Emergency3Number TEXT,
                        ExcursionBlocked TEXT,
                        ExcursionBlockedComment TEXT,
                        ExcursionBlockedReason TEXT,
                        Guardian1Email TEXT,
                        Guardian1EmailVerified TEXT,
                        Guardian1FirstName TEXT,
                        Guardian1HomeNumber TEXT,
                        Guardian1ID TEXT,
                        Guardian1LastName TEXT,
                        Guardian1MobileNumber TEXT,
                        Guardian1ResidentialAddress TEXT,
                        Guardian1ResidentialPostcode TEXT,
                        Guardian1ResidentialState TEXT,
                        Guardian1ResidentialSuburb TEXT,
                        Guardian1ShareInformation TEXT,
                        Guardian1Title TEXT,
                        Guardian1WorkNumber TEXT,
                        Guardian2Email TEXT,
                        Guardian2EmailVerified TEXT,
                        Guardian2FirstName TEXT,
                        Guardian2HomeNumber TEXT,
                        Guardian2ID TEXT,
                        Guardian2LastName TEXT,
                        Guardian2MobileNumber TEXT,
                        Guardian2ResidentialAddress TEXT,
                        Guardian2ResidentialPostcode TEXT,
                        Guardian2ResidentialState TEXT,
                        Guardian2ResidentialSuburb TEXT,
                        Guardian2ShareInformation TEXT,
                        Guardian2Title TEXT,
                        Guardian2WorkNumber TEXT,
                        House TEXT,
                        LanguageSpokenAtHome TEXT,
                        LanguageSpokenAtHomeId TEXT,
                        MedicAlertBracelet TEXT,
                        MiddleName TEXT,
                        MobileNumber TEXT,
                        ModifiedDate TEXT,
                        Paid TEXT,
                        PermissionAntiHistamine TEXT,
                        PermissionAntiInflammatory TEXT,
                        PermissionEmergencyMedical TEXT,
                        PermissionIbuprofen TEXT,
                        PermissionParacetamol TEXT,
                        PhoneNumber TEXT,
                        Postcode TEXT,
                        SportFirstChoice TEXT,
                        SportSecondChoice TEXT,
                        State TEXT,
                        StudentGroupsList TEXT,
                        Suburb TEXT,
                        SwimmingAbility TEXT,
                        SwimmingComments TEXT,
                        TeamsList TEXT,
                        WearsContacts TEXT,
                        WearsGlasses TEXT,
                        WearsHearingAid TEXT,
                        C2gSiteIdentifier TEXT
                    );";

            using var createCommand = new SqliteCommand(createTableSql, connection);
            createCommand.ExecuteNonQuery();
        }

        internal int Upsert(List<C2GDownload> c2gdata)
        {
            var insertedCount = 0;
            CreateTable(connection);

            var existingData = GetData();

            foreach (var c2g in c2gdata)
            {
                if (!existingData.Any(x => x.UniqueIdentifier == c2g.UniqueIdentifier))
                {
                    Insert(c2g);   
                    insertedCount++;
                }
            }
            return insertedCount;
        }

        internal void Insert(C2GDownload c2g)
        {

            // Data doesn't have this record adding it.
            Console.WriteLine($"Inserting Record {c2g.UniqueIdentifier} {c2g.Title} {c2g.FirstName} {c2g.LastName}");

            string upsertSql = @"
                        INSERT INTO Consent2GoProfiles (
                            UniqueIdentifier,
                            Title,
                            FirstName,
                            LastName,
                            Email,
                            Gender,
                            BirthDate,
                            SchoolYear,
                            HomeRoom,
                            Parent1LastUpdated,
                            Parent2LastUpdated,
                            AboriginalOrTorresStrait,
                            AdditionalPlayingHistory,
                            Address,
                            Age,
                            Boarder,
                            BoarderHouse,
                            CampusName,
                            ColourBlind,
                            CountryOfBirth,
                            CreationDate,
                            CurriculumNumber,
                            DietaryNonMedical1,
                            DietaryNonMedical2,
                            DietaryNonMedical,
                            DoctorAddress,
                            DoctorName,
                            DoctorNumber,
                            DoctorPostcode,
                            DoctorPracticeName,
                            DoctorState,
                            DoctorSuburb,
                            EmergencyName,
                            EmergencyNumber,
                            Emergency2Name,
                            Emergency2Number,
                            Emergency3Name,
                            Emergency3Number,
                            ExcursionBlocked,
                            ExcursionBlockedComment,
                            ExcursionBlockedReason,
                            Guardian1Email,
                            Guardian1EmailVerified,
                            Guardian1FirstName,
                            Guardian1HomeNumber,
                            Guardian1ID,
                            Guardian1LastName,
                            Guardian1MobileNumber,
                            Guardian1ResidentialAddress,
                            Guardian1ResidentialPostcode,
                            Guardian1ResidentialState,
                            Guardian1ResidentialSuburb,
                            Guardian1ShareInformation,
                            Guardian1Title,
                            Guardian1WorkNumber,
                            Guardian2Email,
                            Guardian2EmailVerified,
                            Guardian2FirstName,
                            Guardian2HomeNumber,
                            Guardian2ID,
                            Guardian2LastName,
                            Guardian2MobileNumber,
                            Guardian2ResidentialAddress,
                            Guardian2ResidentialPostcode,
                            Guardian2ResidentialState,
                            Guardian2ResidentialSuburb,
                            Guardian2ShareInformation,
                            Guardian2Title,
                            Guardian2WorkNumber,
                            House,
                            LanguageSpokenAtHome,
                            LanguageSpokenAtHomeId,
                            MedicAlertBracelet,
                            MiddleName,
                            MobileNumber,
                            ModifiedDate,
                            Paid,
                            PermissionAntiHistamine,
                            PermissionAntiInflammatory,
                            PermissionEmergencyMedical,
                            PermissionIbuprofen,
                            PermissionParacetamol,
                            PhoneNumber,
                            Postcode,
                            SportFirstChoice,
                            SportSecondChoice,
                            State,
                            StudentGroupsList,
                            Suburb,
                            SwimmingAbility,
                            SwimmingComments,
                            TeamsList,
                            WearsContacts,
                            WearsGlasses,
                            WearsHearingAid,
                            C2gSiteIdentifier) 
                        VALUES (                         
                            @UniqueIdentifier,
                            @Title,
                            @FirstName,
                            @LastName,
                            @Email,
                            @Gender,
                            @BirthDate,
                            @SchoolYear,
                            @HomeRoom,
                            @Parent1LastUpdated,
                            @Parent2LastUpdated,
                            @AboriginalOrTorresStrait,
                            @AdditionalPlayingHistory,
                            @Address,
                            @Age,
                            @Boarder,
                            @BoarderHouse,
                            @CampusName,
                            @ColourBlind,
                            @CountryOfBirth,
                            @CreationDate,
                            @CurriculumNumber,
                            @DietaryNonMedical1,
                            @DietaryNonMedical2,
                            @DietaryNonMedical,
                            @DoctorAddress,
                            @DoctorName,
                            @DoctorNumber,
                            @DoctorPostcode,
                            @DoctorPracticeName,
                            @DoctorState,
                            @DoctorSuburb,
                            @EmergencyName,
                            @EmergencyNumber,
                            @Emergency2Name,
                            @Emergency2Number,
                            @Emergency3Name,
                            @Emergency3Number,
                            @ExcursionBlocked,
                            @ExcursionBlockedComment,
                            @ExcursionBlockedReason,
                            @Guardian1Email,
                            @Guardian1EmailVerified,
                            @Guardian1FirstName,
                            @Guardian1HomeNumber,
                            @Guardian1ID,
                            @Guardian1LastName,
                            @Guardian1MobileNumber,
                            @Guardian1ResidentialAddress,
                            @Guardian1ResidentialPostcode,
                            @Guardian1ResidentialState,
                            @Guardian1ResidentialSuburb,
                            @Guardian1ShareInformation,
                            @Guardian1Title,
                            @Guardian1WorkNumber,
                            @Guardian2Email,
                            @Guardian2EmailVerified,
                            @Guardian2FirstName,
                            @Guardian2HomeNumber,
                            @Guardian2ID,
                            @Guardian2LastName,
                            @Guardian2MobileNumber,
                            @Guardian2ResidentialAddress,
                            @Guardian2ResidentialPostcode,
                            @Guardian2ResidentialState,
                            @Guardian2ResidentialSuburb,
                            @Guardian2ShareInformation,
                            @Guardian2Title,
                            @Guardian2WorkNumber,
                            @House,
                            @LanguageSpokenAtHome,
                            @LanguageSpokenAtHomeId,
                            @MedicAlertBracelet,
                            @MiddleName,
                            @MobileNumber,
                            @ModifiedDate,
                            @Paid,
                            @PermissionAntiHistamine,
                            @PermissionAntiInflammatory,
                            @PermissionEmergencyMedical,
                            @PermissionIbuprofen,
                            @PermissionParacetamol,
                            @PhoneNumber,
                            @Postcode,
                            @SportFirstChoice,
                            @SportSecondChoice,
                            @State,
                            @StudentGroupsList,
                            @Suburb,
                            @SwimmingAbility,
                            @SwimmingComments,
                            @TeamsList,
                            @WearsContacts,
                            @WearsGlasses,
                            @WearsHearingAid,
                            @C2gSiteIdentifier)";

            using var command = new SqliteCommand(upsertSql, connection);


            AddParameter(command, "@UniqueIdentifier", c2g.UniqueIdentifier);
            AddParameter(command, "@Title", c2g.Title);
            AddParameter(command, "@FirstName", c2g.FirstName);
            AddParameter(command, "@LastName", c2g.LastName);
            AddParameter(command, "@Email", c2g.Email);
            AddParameter(command, "@Gender", c2g.Gender);
            AddParameter(command, "@BirthDate", c2g.BirthDate);
            AddParameter(command, "@SchoolYear", c2g.SchoolYear);
            AddParameter(command, "@HomeRoom", c2g.HomeRoom);
            AddParameter(command, "@Parent1LastUpdated", c2g.Parent1LastUpdated);
            AddParameter(command, "@Parent2LastUpdated", c2g.Parent2LastUpdated);
            AddParameter(command, "@AboriginalOrTorresStrait", c2g.AboriginalOrTorresStrait);
            AddParameter(command, "@AdditionalPlayingHistory", c2g.AdditionalPlayingHistory);
            AddParameter(command, "@Address", c2g.Address);
            AddParameter(command, "@Age", c2g.Age);
            AddParameter(command, "@Boarder", c2g.Boarder);
            AddParameter(command, "@BoarderHouse", c2g.BoarderHouse);
            AddParameter(command, "@CampusName", c2g.CampusName);
            AddParameter(command, "@ColourBlind", c2g.ColourBlind);
            AddParameter(command, "@CountryOfBirth", c2g.CountryOfBirth);
            AddParameter(command, "@CreationDate", c2g.CreationDate);
            AddParameter(command, "@CurriculumNumber", c2g.CurriculumNumber);
            AddParameter(command, "@DietaryNonMedical1", c2g.DietaryNonMedical1);
            AddParameter(command, "@DietaryNonMedical2", c2g.DietaryNonMedical2);
            AddParameter(command, "@DietaryNonMedical", c2g.DietaryNonMedical);
            AddParameter(command, "@DoctorAddress", c2g.DoctorAddress);
            AddParameter(command, "@DoctorName", c2g.DoctorName);
            AddParameter(command, "@DoctorNumber", c2g.DoctorNumber);
            AddParameter(command, "@DoctorPostcode", c2g.DoctorPostcode);
            AddParameter(command, "@DoctorPracticeName", c2g.DoctorPracticeName);
            AddParameter(command, "@DoctorState", c2g.DoctorState);
            AddParameter(command, "@DoctorSuburb", c2g.DoctorSuburb);
            AddParameter(command, "@EmergencyName", c2g.EmergencyName);
            AddParameter(command, "@EmergencyNumber", c2g.@EmergencyNumber);
            AddParameter(command, "@Emergency2Name", c2g.Emergency2Name);
            AddParameter(command, "@Emergency2Number", c2g.Emergency2Number);
            AddParameter(command, "@Emergency3Name", c2g.Emergency3Name);
            AddParameter(command, "@Emergency3Number", c2g.Emergency3Number);
            AddParameter(command, "@ExcursionBlocked", c2g.ExcursionBlocked);
            AddParameter(command, "@ExcursionBlockedComment", c2g.ExcursionBlockedComment);
            AddParameter(command, "@ExcursionBlockedReason", c2g.ExcursionBlockedReason);
            AddParameter(command, "@Guardian1Email", c2g.Guardian1Email);
            AddParameter(command, "@Guardian1EmailVerified", c2g.Guardian1EmailVerified);
            AddParameter(command, "@Guardian1FirstName", c2g.Guardian1FirstName);
            AddParameter(command, "@Guardian1HomeNumber", c2g.Guardian1HomeNumber);
            AddParameter(command, "@Guardian1ID", c2g.Guardian1ID);
            AddParameter(command, "@Guardian1LastName", c2g.Guardian1LastName);
            AddParameter(command, "@Guardian1MobileNumber", c2g.Guardian1MobileNumber);
            AddParameter(command, "@Guardian1ResidentialAddress", c2g.Guardian1ResidentialAddress);
            AddParameter(command, "@Guardian1ResidentialPostcode", c2g.Guardian1ResidentialPostcode);
            AddParameter(command, "@Guardian1ResidentialState", c2g.Guardian1ResidentialState);
            AddParameter(command, "@Guardian1ResidentialSuburb", c2g.Guardian1ResidentialSuburb);
            AddParameter(command, "@Guardian1ShareInformation", c2g.Guardian1ShareInformation);
            AddParameter(command, "@Guardian1Title", c2g.Guardian1Title);
            AddParameter(command, "@Guardian1WorkNumber", c2g.Guardian1WorkNumber);
            AddParameter(command, "@Guardian2Email", c2g.Guardian2Email);
            AddParameter(command, "@Guardian2EmailVerified", c2g.Guardian2EmailVerified);
            AddParameter(command, "@Guardian2FirstName", c2g.Guardian2FirstName);
            AddParameter(command, "@Guardian2HomeNumber", c2g.Guardian2HomeNumber);
            AddParameter(command, "@Guardian2ID", c2g.Guardian2ID);
            AddParameter(command, "@Guardian2LastName", c2g.Guardian2LastName);
            AddParameter(command, "@Guardian2MobileNumber", c2g.Guardian2MobileNumber);
            AddParameter(command, "@Guardian2ResidentialAddress", c2g.Guardian2ResidentialAddress);
            AddParameter(command, "@Guardian2ResidentialPostcode", c2g.Guardian2ResidentialPostcode);
            AddParameter(command, "@Guardian2ResidentialState", c2g.Guardian2ResidentialState);
            AddParameter(command, "@Guardian2ResidentialSuburb", c2g.Guardian2ResidentialSuburb);
            AddParameter(command, "@Guardian2ShareInformation", c2g.Guardian2ShareInformation);
            AddParameter(command, "@Guardian2Title", c2g.Guardian2Title);
            AddParameter(command, "@Guardian2WorkNumber", c2g.Guardian2WorkNumber);
            AddParameter(command, "@House", c2g.House);
            AddParameter(command, "@LanguageSpokenAtHome", c2g.LanguageSpokenAtHome);
            AddParameter(command, "@LanguageSpokenAtHomeId", c2g.LanguageSpokenAtHomeId);
            AddParameter(command, "@MedicAlertBracelet", c2g.MedicAlertBracelet);
            AddParameter(command, "@MiddleName", c2g.MiddleName);
            AddParameter(command, "@MobileNumber", c2g.MobileNumber);
            AddParameter(command, "@ModifiedDate", c2g.ModifiedDate);
            AddParameter(command, "@Paid", c2g.Paid);
            AddParameter(command, "@PermissionAntiHistamine", c2g.PermissionAntiHistamine);
            AddParameter(command, "@PermissionAntiInflammatory", c2g.PermissionAntiInflammatory);
            AddParameter(command, "@PermissionEmergencyMedical", c2g.PermissionEmergencyMedical);
            AddParameter(command, "@PermissionIbuprofen", c2g.PermissionIbuprofen);
            AddParameter(command, "@PermissionParacetamol", c2g.PermissionParacetamol);
            AddParameter(command, "@PhoneNumber", c2g.PhoneNumber);
            AddParameter(command, "@Postcode", c2g.Postcode);
            AddParameter(command, "@SportFirstChoice", c2g.SportFirstChoice);
            AddParameter(command, "@SportSecondChoice", c2g.SportSecondChoice);
            AddParameter(command, "@State", c2g.State);
            AddParameter(command, "@StudentGroupsList", c2g.StudentGroupsList);
            AddParameter(command, "@Suburb", c2g.Suburb);
            AddParameter(command, "@SwimmingAbility", c2g.SwimmingAbility);
            AddParameter(command, "@SwimmingComments", c2g.SwimmingComments);
            AddParameter(command, "@TeamsList", c2g.TeamsList);
            AddParameter(command, "@WearsContacts", c2g.WearsContacts);
            AddParameter(command, "@WearsGlasses", c2g.WearsGlasses);
            AddParameter(command, "@WearsHearingAid", c2g.WearsHearingAid);
            AddParameter(command, "@C2gSiteIdentifier", c2g.C2gSiteIdentifier);

            // First execution: Inserts the new record
            command.ExecuteNonQuery();
        }


        internal void AddParameter(SqliteCommand command, string name, object? obj)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;

            if (obj == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = obj;
            }
            command.Parameters.Add(parameter);
        }

        internal List<C2GDownload> GetData()
        {
            string sql = "SELECT * FROM Consent2GoProfiles";
            var existingdata = new List<C2GDownload>();
            using var command = new SqliteCommand(sql, connection);

            // Execute the reader to get a forward-only stream of rows
            using var reader = command.ExecuteReader();

            // Loop through the records row by row

            while (reader.Read())
            {
                // Use attribute-based mapping to avoid manual name typos
                var c2g = DbMapper.MapRowTo<C2GDownload>(reader);
                existingdata.Add(c2g);
            }

            return existingdata;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue1)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue1 = true;
            }
        }
    }
}





