# Prerequisites

Before you can begin to develop the IMS it is recommended that the following are in place:

1. You have somewhere to create a SQL database
2. You have added the LocalGov IMS package feed as a package source

## Accessing a SQL instance

You'll need access to either a remote and local SQL instance.
<br>
You'll require **dbowner** privileges

## Adding a NuGet package source

Two simple options for this are to add it via the Visual Studio UI, or command line

### Setup NuGet package sources via Visual Studio

1. Open  Tools > NuGet Package Manager > Package Manager Settings
2. In the 'Options' dialog that's shown navigate to NuGet Package Manager > Package Sources
3. Click the green plus sign
4. On the new entry, edit the properties:
   * Name: LocalGov IMS
   * Source: https://nuget.pkg.github.com/LocalGovIMS/index.json
5. Click 'OK'

### Setup NuGet pacakge sources via command line

1. [Create a Personal Access Token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) in Github
2. Open a command prompt
2. Run the following command, replacing the placeholders with the appropriate details:
```
   dotnet nuget add source "https://nuget.pkg.github.com/LocalGovIMS/index.json" --name "LocalGov IMS" --username [YOUR GIT USERNAME] --password [YOUR PAT]
```