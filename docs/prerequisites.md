# Prerequisites

Before you can begin to develop the IMS it is recommended that the following are in place:

1. You have somewhere to create a SQL database
2. You have added the LocalGov IMS package feed as a package source (see below for instructions on how to do this)

# Accessing a SQL Instance

You'll need access to a SQL instance with an account which has **dbowner** privileges

# Adding a NuGet Package Source

There are two options for this:
1. Add it via the Visual Studio UI
2. Add it via the command line

## Setup NuGet Package Sources via Visual Studio

1. Open  Tools > NuGet Package Manager > Package Manager Settings
2. In the 'Options' dialog that's shown navigate to NuGet Package Manager > Package Sources
3. Click the green plus sign
4. On the new entry, edit the properties:
   * Name: LocalGov IMS
   * Source: https://nuget.pkg.github.com/LocalGovIMS/index.json
5. Click 'OK'

## Setup NuGet Package Sources via Command Line

1. [Create a Personal Access Token](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) in GitHub
2. Open a command prompt
2. Run the following command, replacing the placeholders with the appropriate details:
```
   dotnet nuget add source "https://nuget.pkg.github.com/LocalGovIMS/index.json" --name "LocalGov IMS" --username [YOUR GITHUB USERNAME] --password [YOUR PAT]
```