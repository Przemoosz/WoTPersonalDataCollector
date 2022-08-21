# World Of Tanks Personal Data Collector

## Description
World Of Tanks Personal Data Collector or WPD in brief is a 
.Net project build as time-triggered Azure Function. This project allows
a user to collect the latest data about account like battles, average damage, etc. using [World Of Tanks REST API](https://developers.wargaming.net/documentation/guide/principles/).

At this time function crawl data about user id after providing username as a local variable.
In the future, this program will also crawl data about a given user, Ex. battles, average damage, etc.
and it will store this data in CosmosDB. The project is ready to deploy as an azure app, but you can run it locally. 
Solution include also 40+ unit tests written in the NUnit testing framework

For more information go to section called: Versions 
## About Solution

Solution is build in .net 6.0 with C# 10, used azure function version is 4, and everything was created using Visual Studio Enterprise 2022 

Used NuGet packages:

- Microsoft.Azure.Functions.Extensions ver. 1.1.0
- Microsoft.Extensions.DependencyInjection ver. 6.0.0
- Microsoft.NET.Sdk.Functions ver. 4.1.1
- Guard.Net ver. 2.0.0
- Any ver. 7.0.0
- FluentAssertions ver. 6.7.0
- Microsoft.NET.Test.Sdk ver. 16.11.0
- NSubstitute ver. 4.4.0
- NUnit ver. 3.13.2
- NUnit3TestAdapter ver. 4.0.0
- coverlet.collector ver. 3.1.0
- TddXt.Any.Extensibility ver 6.7.0

## Versions
Version name - Description - Status
- WPD-1-CrawlDataAboutUser - Crawling Data About User id using WOT REST API - Finished and merged
- WPD-2-ImplementChainOfResponsibility - Refactored azure function to implement chain of responsibility design pattern - Finished and merged
- WPD-3-CrawlSpecificUserData - Crawling specific data from WOT REST API about given user - In Development
- WPD-4-SaveDataToCosmosDB - Saving data crawled from WOT REST API to CosmosDB - Planned

## Installation

To use this app locally you have to install Visual Studio 2022. After this clone solution and add "local.settings.json" 
inside WotPersonalDataCollector folder (the same folder which contains azure function)
In this file add and fill missing data. File should look like this:

```json
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ApplicationId": "Type your own WOT API Application Id",
    "WotUserName" : "type your own username " 
  }
}
```

After adding this file run your azure app using VS 2022. 
To run tests you can use VS 2022 or type in powershell:
```bash
dotnet test
```
To build project type:


```bash
dotnet build
```