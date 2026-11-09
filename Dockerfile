FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy everything and restore using the project path directly
COPY . .
RUN dotnet restore "RaceDay.Web/RaceDay.Web.csproj"

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