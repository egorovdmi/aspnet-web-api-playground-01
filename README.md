ASP.NET Web API Starter-Kit (aspnet-web-api-playground-01)
==========================================================

Project was developed using Visual Studio 2015. 
Here's a list of perks that have been used:

- Namespaces versioning for controllers with the same name: 

For example, using this aproach we can have two DiffController classes.
One in V1 namespace, other in V2 namespace and URLs will be like:

    http://localhost:52627/v1/diff/1         V1.DiffController.Get method
    http://localhost:52627/v2/diff/1         V2.DiffController.Get method

- Castle Windsor DI container.
- OWIN as Self-Hosting environment for integration tests.
- Log4net as logging for Unit tests.


CONTENTS
--------

Files and directories:

      RESTPlayground01.Core/                   Shared classes, Interfaces
      RESTPlayground01.IntegrationTests/       Integration tests
      RESTPlayground01.Tests/                  Unit tests
      RESTPlayground01/                        Web API service application 
      README                                   this file
      RESTPlayground01.sln                     the solution file


REQUIREMENTS
------------

- Visual Studio 2013/2015.
- Internet access for packages downloading.


IMPORTANT!!! QUICK START
------------------------

Visual Studio has to be started as Administrator, 
otherwise integration tests can't run.

Within Visual Studio you can run:

- RESTPlayground01 project. Just run the project. Will be opened a page with instructions sample.
- Unit and integration tests. In toolbar: Test -> Run -> All Tests. 

For running integration tests you don't need to run Web API service separetly,
because they run their own Self-Hosting environment.


SELF-HOSTING ENVIRONMENT
------------------------

This approach has a wide range of advantages:

- You don’t have to deploy the web API project.
- The test and web API code run in the same process.
- Breakpoints can be added to the web API code.
- The entire ASP.NET pipeline can be tested with routing, filters, configuration, etc.
- Any web API dependencies that will otherwise be injected from a DI container, can be faked from the test code.
- Tests are quick to run!
