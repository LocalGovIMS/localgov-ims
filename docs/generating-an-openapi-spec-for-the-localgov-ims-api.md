# Introduction 
The following document describes what is requied to generate an [OpenApi specification](https://swagger.io/specification/) for the LocalGov IMS API

The LocalGov IMS API is written in .NET Framework 4.8, and as such, the tooling that's available to create an OpenAPI specification is limited.

# Tooling
A number of technologies and tools are used in this process:
1. [Swagger](https://swagger.io/): used to TODO: what
2. [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.WebApi): used to TODO: what?
3. [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio): used to generate an OpenAPI specification from an API. You can read more about this on [MSDN](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-5.0&tabs=visual-studio)

# Setting Up Your Machine
This guide explains how to setup a Windows PC ready for API client generation

1. Install [Java](https://www.java.com/en/)
2. Setup Java environment variables:
    * Navigate to My Computer
    * Right click and choose 'Properties'
    * Select 'Advanced System Settings'
    * In the 'System Properties' dialog box select 'Environmental Variables' in the 'Advanced' tab
    * In the 'System Variables' section edit the Path value, adding in an entry pointing to the bin folder of the Java runtime environment, such as 'C:\Program Files (x86)\Java\jre1.8.0_321\bin'
    * In the 'System Variables' section add a new entry with a name of 'JAVA_HOME' and a value pointing to the bin folder of the Java runtime environment, such as 'C:\Program Files (x86)\Java\jre1.8.0_321\bin'
3. Install [NSwagStudio](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)

# Creating the OpanAPI Specification

1. Open NSwagStudio
2. Load the [Nsawg project file](src\Api\LocalGovImsApi.nsawg)
3. Click the 'Generate Outputs' button
4. Copy the JSON output
5. Goto the [Swagger Editor](https://editor.swagger.io/) site
	* Click into the left had pane, press ctr-A (to select all), ctrl-V (to paste)
    * When the popup asks 'Would you like to convert your JSON to YAML', choose 'OK' 
6. Save the YAML file to your PC

# Next steps

Read about how to [create an API client](creating-an-api-client-from-an-openapi-specification.md) from an OpenAPI specification

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

