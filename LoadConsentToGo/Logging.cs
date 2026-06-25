
namespace LoadConsentToGo
{
    internal class Logging
    {

        internal static readonly Logging Instance = new ();

        internal string logfilepath;

        internal Logging()
        {
            logfilepath= Path.Combine(Consent2GoFunctions.consent2gopathuploadlog, $"consent2golog{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.log");
            var dir = Path.GetDirectoryName(logfilepath) ?? throw new NullReferenceException($"Directory {logfilepath} is null");

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        internal void Log(string message, bool showmessagebox = false)
        {
            try
            {
                Console.WriteLine(message);
                var entry = $"{DateTime.UtcNow:O} {message}{Environment.NewLine}";
                File.AppendAllText(logfilepath, entry);

                if (showmessagebox) MessageBox.Show(message, "Load Consent2Go", MessageBoxButtons.OK);
            }
            catch
            {
                // Swallow logging errors so logging never breaks execution.
            }
        }
    }
}
