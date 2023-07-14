# GatewayManagement
Management system for Gateways and Peripheral devices.
## Instalation
* Install Net Core SDK 7 de [Net Core](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Download project from Github
```
git clone https://github.com/JoannyCuba/GatewayManagement.git
```
* Configure the appsettings.json properly with the HTTP and HTTPS ports of your preference. By default, they are set to 6800 and 6801, respectively.
* Properly configure the appsettings.json with the connection string to the database (SQL Server 2019).

## Publish
* Publish the project. Default source after publish: GatewayManagementAPI\bin\Release\net7.0\publish
* Execute file GatewayManagementAPI.exe
