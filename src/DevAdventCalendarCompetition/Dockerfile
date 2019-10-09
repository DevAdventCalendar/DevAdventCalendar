FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# restore packages
COPY *.sln .
COPY ./DevAdventCalendarCompetition/*.csproj ./DevAdventCalendarCompetition/
COPY ./DevAdventCalendarCompetition.Repository/*.csproj ./DevAdventCalendarCompetition.Repository/
COPY ./DevAdventCalendarCompetition.Services/*.csproj ./DevAdventCalendarCompetition.Services/
COPY ./DevAdventCalendarCompetition.Tests/*.csproj ./DevAdventCalendarCompetition.Tests/
RUN dotnet restore

# copy everything else
COPY . .

# run unit tests
WORKDIR /app/DevAdventCalendarCompetition.Tests
RUN dotnet test -c Release

# publish app
WORKDIR /app/DevAdventCalendarCompetition
RUN dotnet publish -c Release -o /out
WORKDIR /out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /out
COPY --from=build /out .

ENTRYPOINT ["dotnet", "DevAdventCalendarCompetition.dll"]