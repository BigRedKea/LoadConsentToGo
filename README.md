# LoadConsentToGo

LoadConsentToGo is a Windows Forms utility for moving SMS data between Excel/CSV files, SQLite, and Consent2Go. The workflow is organised into four main steps so the same data can be prepared locally, checked, and then downloaded to a local store.

## Four-step workflow

1. Prepare the local configuration
   - Copy the example secrets file to Secrets.json and update the Consent2Go credentials.
   - Ensure GroupLookup.json contains the mapping between SMS identifiers and Consent2Go groups.

2. Import student Excel data into SQLite
   - Run the student import action to merge student spreadsheet data into the local consent2go.db database.
   - This step creates the baseline records used by later uploads.

3. Download and import staff data
   - Download staff records from Consent2Go for the configured groups.
   - Import the staff spreadsheet data into SQLite so it is available for later processing.

4. Upload the prepared data back to Consent2Go
   - Choose the CSV export to upload and send the prepared student or system-user records back to Consent2Go.
   - The app will process the data in groups and report progress in the console.

## Build

From the repository root, build the project with:

```bash
dotnet build LoadConsentToGo.csproj
```

## Run

Run the application from the repository root with:

```bash
dotnet run --project LoadConsentToGo.csproj
```

## Known issues

- Consent2Go may reject or behave unexpectedly when the same email address is used more than once in a batch upload. This is a known integration issue.

## Notes

- The project targets .NET 8 on Windows and uses Windows Forms.
- The application expects the working files such as Secrets.json and GroupLookup.json to be available alongside the executable at runtime.
