# PortfolioHub Assignment

# following are the steps to run the applicaiton 

1. Please verify connectionstring in below file
.	PortfolioHub\PortfolioHub.Api\appsettings.json

2. To create database(SkyFriPortfolioHub) in SQL Server, Please open package manager console and execute below migration command 
   and please ensure default project should be "PortfolioHub.Infrastructure.Efcore"
   # execute this command =>  update-database 
3. Basic endpoints are created using C# , REST API, Entity Framework, AutoMapper, Repository Pattern with Code First Approach.   