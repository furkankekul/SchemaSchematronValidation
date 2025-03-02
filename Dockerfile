FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore && dotnet publish -c Release -o /publish 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "C#BitirmeOdevi.dll"]