# LoadConsentToGo

LoadConsentToGo is a Windows Forms utility for moving SMS data between Excel/CSV files, SQLite, and Consent2Go. The workflow is organised into four main steps so the same data can be prepared locally, checked, and then downloaded to a local store.

## Four-step workflow

1. Prepare the local configuration
   - Copy the example secrets file to Secrets.json and update the Consent2Go credentials.
   - Ensure GroupLookup.json contains the mapping between SMS identifiers and Consent2Go groups.

2. Download student data from SMS into CSV files using the reporting functionality in SMS

3. Run the Upload Student
    - It will open consent2Go webpage using selenium and will 
        - Open each group
        - Check against a SQL lite database to see if the member (number) has already been loaded in another pass.
        - Check for existing Surnames
        - Ask if a new record is to be inserted or skiped
        - Pause after creating the email (sometimes the web page will reject the code) to allow corrrection
        - Check if ok to commit
        - If the person has been added manually the Member number may not be loaded. This can be manually added
        - After each group is loaded it will automatically download all the data for the group to your download folder. Copy these across to a download folder ready for loading into a SqlLite instance

4. Cache the download from consentToGo into a local database
   - Run the student upload action to merge student spreadsheet data into the local consent2go.db database.
   - This step creates the baseline records used by later uploads.


3. Download and import staff data
   - Follow a similar process for loading staff into consent to go.

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

- Consent2Go may reject an application when the same email address is used more than once in a batch upload. This is a known integration issue. The current fix is the code will pause for a manual check... Add an additional charactor before proceeding.

## Notes

- The project targets .NET 8 on Windows and uses Windows Forms.
- The application expects the working files such as Secrets.json and GroupLookup.json to be available alongside the executable at runtime.
