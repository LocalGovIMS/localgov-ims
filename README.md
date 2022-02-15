# Introduction 
This is an open, collaborative local authority project funded by the Ministry of Housing, Communities and Local Government (MCHLG) Local Digital Fund, and led by Barnsley Metropolitan Borough Council (BMBC) & Partners.

# Getting Started

This solution targets .NET Framework 4.8.
It follows a clean architecture style: the business logic sits at the core and the dependencies from the data access and web projects flow into it.

There are three web applications:

1. PaymentPortal
2. Admin
3. Api

## Payment Portal
A UI allowing a customer to pay for items via a payment basket.

## Admin
A UI allowing staff to manage income management.

## API
An API allowing other services to integrate with the IMS

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

# Payment Integrations

TODO: This whole section needs reviewing a rewriting - it may be best to extract it into it's own document

The Payment Portal uses 3rd party payment services to process payments.
These services are utilised through payment integrations.

To be able to take a payment through the Payment Portal or Admin you will need to configure and run one of the existing payment integrations, or write a new one.

There are two aspects to configuring a payment integration. 
1. Configuring the integration itself (see the readme of the integrtion(s) you are using)
2. Create a PaymentIntegrtaion record within the IMS database.

PaymentIntegrations within IMS require some manual configuration.
1. Manually add a new entry into the PaymentIntegrations table
3. Run the Admin application and configure those Method of Payemnt (MOP) codes you wish to take payment through the payment integrations by adding a meta data entry 'PaymentIntegrationId' with the ID of the payment integration record you just created

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

