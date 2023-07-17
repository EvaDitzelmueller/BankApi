# Banking API
## Description

A small demo application that mimicks the workings of a bank. It is built with .NET 6 and .NET Core Web.

The development process was based on TDD/BDD and loosely DDD.

It covers the following use-cases: 
- CRUD Users
- CRUD Accounts
- Deposit/Withdraw funds
- Several business logic rules (max. withdrawal amount, min. account balance, ...)

## Project Structure
- API
  - Domain
  - Controller
  - Services
- Testing
  - UnitTests (xUnit)

## How to run

In order to run this we need to create a valid self-signed certificate first:
```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <password>
dotnet dev-certs https --trust
```
This then also needs to be replaced inside the environment variables in the compose.yaml for local development and testing.
```
- ASPNETCORE_Kestrel__Certificates__Default__Password=<password> <- password goes here
```
Then simply run

```
docker compose up
```
