

namespace LoadConsentToGo
{
    internal class MergeExcelSystemUserData
    {
        public static List<SystemUser> Execute()
        {

            var result = new List<SystemUser>();
            var openFileDialog = new FolderBrowserDialog()
            {
                Description = "Select the directory containing the xlsx files to merge",

                InitialDirectory = Consent2GoFunctions.consent2gopathupdownloads
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
                            var systemuser = new SystemUser();
                            int c = 0;
                            systemuser.FirstName = reader.GetString(c++);
                            systemuser.LastName = reader.GetString(c++);
                            systemuser.Email = reader.GetString(c++);
                            systemuser.Role = reader.GetString(c++);
                            systemuser.SourceFile = file;
                            systemuser.C2gSiteIdentifier = null;

                            result.Add(systemuser);
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
        }

    }
}
