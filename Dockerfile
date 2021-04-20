FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /app

COPY STA.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c release -o /app/output --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app/output ./
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "STA.dll"]