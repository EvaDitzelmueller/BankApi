In order to run this we need to create a valid self-signed certificate first:

dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <password>
dotnet dev-certs https --trust

This then also needs to be replaced inside the environment variables in the compose.yaml for local development and testing.


      - ASPNETCORE_Kestrel__Certificates__Default__Password=<password> <- password goes here

Then simply run

docker compose up
