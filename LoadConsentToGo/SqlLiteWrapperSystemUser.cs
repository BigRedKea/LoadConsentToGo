
using LoadConsentToGo;
using Microsoft.Data.Sqlite;


namespace LoadConsentToGo
{

    public class SqlLiteWrapperSystemUser : IDisposable
    {
        readonly string __tablename = "Consent2GoSystemUsers";

        SqliteConnection connection;
        private bool disposedValue;
        private bool disposedValue1;

        internal SqlLiteWrapperSystemUser(string dbfilepath)
        {
            string connectionString = $"Data Source={dbfilepath};";
            connection = new SqliteConnection(connectionString);
            connection.Open();
        }

        internal static void CreateTable(SqliteConnection connection)
        {
            // 1. Setup a test table with a UNIQUE constraint
            string createTableSql = @" CREATE TABLE IF NOT EXISTS Consent2GoSystemUsers (
                        SqlId INTEGER DEFAULT 1 PRIMARY KEY,
                        FirstName TEXT,
                        LastName TEXT,
                        Email TEXT,
                        Role TEXT,
                        AdditionalPrivileges TEXT,
                        SourceFile TEXT,
                        C2gSiteIdentifier TEXT
                    );";

            using var createCommand = new SqliteCommand(createTableSql, connection);
            createCommand.ExecuteNonQuery();
        }

        internal int Upsert(List<SystemUser> c2gdata)
        {
            var insertedCount = 0;
            CreateTable(connection);

            var existingData = GetData();

            foreach (var c2g in c2gdata)
            {
                if (!existingData.Any(x => x.Email == c2g.Email))
                {
                    Insert(c2g);   
                    insertedCount++;
                }
            }
            return insertedCount;
        }

        internal void Insert(SystemUser c2g)
        {

            // Data doesn't have this record adding it.
            Console.WriteLine($"Inserting Record {c2g.Email} {c2g.FirstName} {c2g.LastName}");


            string createTableSql = @" CREATE TABLE IF NOT EXISTS Consent2GoSystemUsers (
                        SqlId INTEGER DEFAULT 1 PRIMARY KEY,
                        FirstName TEXT,
                        LastName TEXT,
                        Email TEXT,
                        Role TEXT,
                        AdditionalPrivileges TEXT,
                        SourceFile TEXT,
                        C2gSiteIdentifier TEXT
                    );";

            string upsertSql = @"
                        INSERT INTO Consent2GoSystemUsers (
                            FirstName,
                            LastName,
                            Email,
                            Role,
                            AdditionalPrivileges,
                            SourceFile,
                            C2gSiteIdentifier) 
                        VALUES (                         
                            @FirstName,
                            @LastName,
                            @Email,
                            @Role,
                            @AdditionalPrivileges,
                            @SourceFile,
                            @C2gSiteIdentifier)";

            using var command = new SqliteCommand(upsertSql, connection);

            AddParameter(command, "@FirstName", c2g.FirstName);
            AddParameter(command, "@LastName", c2g.LastName);
            AddParameter(command, "@Email", c2g.Email);
            AddParameter(command, "@Role", c2g.Role);
            AddParameter(command, "@AdditionalPrivileges", c2g.AdditionalPrivileges);
            AddParameter(command, "@SourceFile", c2g.SourceFile);
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





