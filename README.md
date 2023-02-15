# Introduction 
This repository contains a solution comprising the core applications for the Income Management System (IMS):

1. PaymentPortal (MVC)
2. Admin (MVC)
3. Api (Web API)

The projects target the .NET Framework version 4.8.1.
The implementation stores data in a SQL database.

## Payment Portal
A UI allowing a customer to pay for items via a payment basket.

## Admin
A UI allowing staff to manage income management.

## API
An API allowing other services to integrate with the IMS

# Development Guides

[Prerequisites](/docs/prerequisites.md)  
[Application Settings](/docs/application-settings.md)  
[Database Initialisation & Seed Data](/docs/database-initialisation-and-seed-data.md)  
[Database Migrations](/docs/database-migrations.md)  
[Setting Up a User Account](/docs/setting-up-a-user-account.md)  
[Adding a Payment Integration](/docs/adding-a-payment-integration.md)  

# Build and Run

After reading the documentaion and cloning the repository:

1. Open the solution and switch the build configuration to 'Local'
2. Add a Web.Local.config file for each of the web projects you plan to run (typically all three). 
3. Add transformations to the configs for AppSettings - see the relevant Web.Base.config files for lists of the settings.
**Note**: Some settings are common across applications, such as ReferenceSalt, so ensure they are identical.</i>
4. Ensure a transformation for the IncomeDb connection string is specified
5. Ensure the database initialiser is specified.
5. Right click on the solution and 'Restore NuGet packages'
6. Build the solution 
7. Set the project you wish to run as the Start Project. If this is your first run it's recommended to set this to the PaymentPortal project.
8. Press F5 to run. 

# Unit Tests
Unit tests can be run from the Test Explorer window.

# UI Tests
Before running any tests:
1. Configure the run settings (Test > Configure Run Settings) to use [Test.runsettings](/Test.runsettings)
2. Update the settings within that file to reflect values appropriate for your local environment

To run UI tests you will need to open two instances of Visual Studio.
The first will be used to run the application you are wanting to test. 
The second will be used to run the tests via the Test Explorer window.