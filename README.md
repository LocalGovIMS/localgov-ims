# Introduction 
This is an open, collaborative local authority project funded by the Ministry of Housing, Communities and Local Government (MCHLG) Local Digital Fund, and led by Barnsley Metropolitan Borough Council (BMBC) & Partners.

# Solution Architecture

The solution is made up of a mixture of web applications and microservices.
This repository contains the two main UIs for running the IMS, and an API providing access to core IMS data.

# Projects

The code in this repository targets .NET Framework 4.8.
It follows a clean architecture style: the business logic sits at the core and the dependencies from the data access and web projects flow into it.

There are three web applications:

1. PaymentPortal (MVC)
2. Admin (MVC)
3. Api (WebApi)

## Payment Portal
A UI allowing a customer to pay for items via a payment basket.

## Admin
A UI allowing staff to manage income management.

## API
An API allowing other services to integrate with the IMS

# Development Guides

[Quick Start](\docs\quick-start.md)  
<br>
[Prerequisites](\docs\prerequisites.md)  
[Application settings](\docs\app-settings.md)  
[Database initialisation & seed data](\docs\database-initialisation-and-seed-data.md)  
[Database migrations](\docs\database-migrations.md)  
[Setting up a user account](\docs\setting-up-a-user-account.md)  
[Adding a payment integration](\docs\adding-a-payment-integration.md)  










# Payment Integrations

The Payment Portal uses 3rd party payment services to process payments.
These payment services are provided through payment integrations.

To be able to take a payment through the Payment Portal or Admin you will either need to configure and run one of the existing payment integrations, or write a new one.

There are two aspects to configuring a payment integration. 
1. Configuring the integration itself (see the readme of the integrtion(s) you are using)
2. Create a PaymentIntegrtaion record within the IMS database.

PaymentIntegrations within IMS require some manual configuration.
1. Run the Admin application
2. Navigate to Settings > Payment Intgrations
3. Add a new entry for the integrations you are using. Note its Id when you return to the list of Payment Integrations
4. Navigate to Settings > Methods of Payemnt (MOP)
5. For each of the MOPs you want to process payment through the new integration add a meta data entry 'PaymentIntegrationId' with the Id





# Build and Run

After cloning the repository:

1. Open the solution and switch the build configuration to 'Local'
TODO: Check how the Local build configuration works with no Web.Local.config - does it try and create one?
2. Add a Web.Local.config file for each of the web projects you plan to run. 
3. Add transformations to the configs for AppSettings - see the relevant Web.Base.config files for lists of the settings.
**Note**: Some settings are common across applications, such as ReferenceSalt, so ensure they are identical.</i>
4. Ensure  transformation for the IncomeDb connection string is specified
5. Right click on the solution and 'Restore NuGet pakages'
6. Build the solution 
7. Set the project yo wish to run as the Start Project.
8. Press F5 to run. 



# Unit Tests
Unit tests can be ran from the Test Explorer window.

# UI Tests
Before running any tests:
1. Configure the run settings (Test > Configure Run Settings) to use [Test.runsettings](\src\Test.runsettings)
2. Update the settings within that file to reflect values appropriate for your local environment

To run UI tests you will need to open two instances of Visual Studio.
The first will be used to run the application you are wanting to test. 
The second will be used to run the tests via the Test Explorer window.


# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

