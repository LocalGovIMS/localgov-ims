# Introduction
NuGet packages are mechanism through which developers can create, share, and consume useful code.
This documents describes how to create a NuGet package for an API client.

# Suggested reading

Before you start, please read the following:

1. An [MSDN article](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli#create-the-package) describing how to create and publich a pacakge. 
2. A [GitHub article](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry) discusing working with the NuGet registry
3. A [GitHub article](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/creating-a-personal-access-token) about creating Personal Acces Tokens in GitHub

# Packaging your client

1. Open your client solution
2. Update the package properties of the client project (not the test project):
    * Open the project properties of the client project
    * Open the 'Package' tab
    * Update the repository URL - set it to the repository link that you have created for this solution
    * Update the Author and Description
3. Change the build configuration to 'Release'
4. Open the project file in a text editor
   * Update the \<Version> element - increasing the version number
   * Save the file
4. Return to Visual Studio and build the solution
5. Right click on the client project and select 'Pack'

The package will be output to the bin folder. It should contain the version number in the filename.

# Publish your client to GitHub Packages

1. Open the Developer Powershell from Visual Studio (Tools > Command Line > Developer Powershell)
2. Add the LocalGov IMS package source:
    * Run: dotnet nuget add source "https://nuget.pkg.github.com/LocalGovIMS/index.json" --name "LocalGov IMS" --username [YOUR GIT USERNAME] --password [YOUR PAT]
3. Publish the package
    * Run: dotnet nuget push "[Location of your package]" --api-key [YOUR PAT] --source "https://nuget.pkg.github.com/LocalGovIMS/index.json"

# Previous steps

Read about how to [create an API client](creating-an-api-client-from-an-openapi-specification.md) from an OpenAPI specification

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

