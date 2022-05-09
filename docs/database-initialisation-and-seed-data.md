# Database initialisation & Seed data

Entity Framework uses database initialisers to manage how and when code first model changes are applied to the database.
There are serveral initialisers available and it's possible to create your own custom initialiser.
By default Entity Framework uses an initialiser which will create the database if it doesn't exist.

# Suggested reading

1. [Code First and Database Initialization](https://docs.microsoft.com/en-us/archive/msdn-magazine/2016/november/cutting-edge-code-first-and-database-initialization)

# Custom initialiser

A custom initialiser has been used to allow us to run db initialisation SQL scripts depending on what environment we're targetting.

# How to use an initiliser

First you can decide whether to use it or not.
If you don't want to use it, do nothing.

If you do want to use it, you need to do two things:

1. Add a web.config transformation to enable it
2. Make sure the environment you are running it for is set.

## Enabling via web.config

Add the following to you web.config transformation file:

```
<entityFramework xdt:Transform="Replace">
  <contexts>
    <context type="DataAccess.Persistence.IncomeDbContext, DataAccess">
      <databaseInitializer type="DataAccess.DatabaseInitialisers.CreateDatabaseIfNotExists, DataAccess" />
    </context>
  </contexts>
</entityFramework> 
```

## Setting the environment

The environment can be configured by setting the appSetting for "Environment" like so:

```
<appSettings>
  <add key="Environment" value="Demo" xdt:Transform="SetAttributes" xdt:Locator="Match(key)" />
</appSettings>
```

The following environments are supported:

* Demo
* UI Test
* Development 
* Test
* Live

# Seed data

The solution contains several seed data scripts. 

* SeedData.sql - Creates reference data
* Indexes.sql - Creates some indexes which are too complex to generate via code first configuration
* DemoData.sql - Adds some settings data (e.g. Fund codes) allowing you to run the solution and log in.
* UITestData.sql - Adds data required by some UI Tests

The SeedData.sql and Indexes.sql are run regardless of environemnt.

If the environment is set to "Demo" the following scripts will also be ran:
* DemoData.sql

If the environment is set to "Demo" the following scripts will also be ran:
* DemoData.sql
* UITestData.sql



