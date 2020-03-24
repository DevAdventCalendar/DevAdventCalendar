FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# restore packages
COPY *.sln .
COPY ./DevAdventCalendarCompetition/*.csproj ./DevAdventCalendarCompetition/
COPY ./DevAdventCalendarCompetition.Repository/*.csproj ./DevAdventCalendarCompetition.Repository/
COPY ./DevAdventCalendarCompetition.Services/*.csproj ./DevAdventCalendarCompetition.Services/
COPY ./DevAdventCalendarCompetition.TestResultService/*.csproj ./DevAdventCalendarCompetition.TestResultService/
COPY ./DevAdventCalendarCompetition.Tests/*.csproj ./DevAdventCalendarCompetition.Tests/
COPY ./DevAdventCalendarCompetition.TestResultService.Tests/*.csproj ./DevAdventCalendarCompetition.TestResultService.Tests/
RUN dotnet restore

# copy everything else
COPY . .

# publish app
WORKDIR /app/DevAdventCalendarCompetition
RUN dotnet publish -c Release -o /out
WORKDIR /out

LABEL maintainer="DevAdventCalendar (devadventcalendar@gmail.com)"

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /out
COPY --from=build /out .

ENTRYPOINT ["dotnet", "DevAdventCalendarCompetition.dll"]
