FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["RaceDaySln.slnx", "./"]
COPY ["RaceDay.Api/RaceDay.Api.csproj", "RaceDay.Api/"]
COPY ["RaceDay.Web/RaceDay.Web.csproj", "RaceDay.Web/"]
COPY ["RaceDay.Tests/RaceDay.Tests.csproj", "RaceDay.Tests/"]

RUN dotnet restore "RaceDaySln.slnx"
COPY . .

WORKDIR "/src/RaceDay.Web"
RUN dotnet build "RaceDay.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RaceDay.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RaceDay.Web.dll"]
