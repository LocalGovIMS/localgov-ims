# Introduction 
The following document describes how to setup your development environment so that you can auto-geneate API client libraries.

## Why auto-generate API client libraries
There are many reasons to auto-generate API client libraries, such as:
1. It reduces the amount of code devs have to write to integrate with an API
2. It improves the developer experience
3. Using the tooling available makes it easy to generate clients in multiple languages

# Tooling
A number of technologies and tools are used in this process:
1. [Node.js](https://nodejs.org/en/): a javascript runtime
2. [OpenApi Specification](https://swagger.io/specification/): used to describe a web service
3. [OpenAPI Genearator](https://openapi-generator.tech/): used to auto-generate API client code from an OpenAPI specification
  
# Setting Up Your Machine
This guide explains how to setup a Windows PC ready for API client generation

1. Install [Node.js](https://nodejs.org/en/)
2. Install the [OpenAPI Genearator](https://openapi-generator.tech/)
    * Open a command prompt
    * Run: npm install @openapitools/openapi-generator-cli -g

# Client creation prerequisites 

1. You will need to [create the OpenAPI specification](generating-an-openapi-spec-for-the-localgov-ims-api.md) for the API you're creating a client for
2. Not required, but creating a repository for the client code at this stage will help if you wish to create a NuGet package for you client at a later stage

# Creating a client

1. Save the OpenAPI spec YAML file to a fodler on your device
2. Open a command prompt and navigate to that folder
3. Run the [OpenAPI Genearator](https://openapi-generator.tech/) to create the client:
    * Run: npx @openapitools/openapi-generator-cli generate -i [Name of your YAML file].yaml -g csharp-netcore -o [Location to save to e.g. /Dev/MyClient/] --package-name [Name of your client]
4. Open the solution that was created
5. Update the client to .NET Standard 2.1 (This is so it can be consumed in .NET 5+ projects later on):
    * Open the project properties of the client project
    * Open the 'Application' tab
    * Change the 'Target framework' value to '.NET Standard 2.1'

# Next steps

Read about how to [package your API client](packaging-your-api-client.md) as a NuGet package

# Previsou steps

Read about how to [create an OpenAPI specification](generating-an-openapi-spec-for-the-localgov-ims-api.md) for your API


# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

