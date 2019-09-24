FROM microsoft/dotnet:2.2-sdk AS build
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


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /out
COPY --from=build /out .

ENTRYPOINT ["dotnet", "DevAdventCalendarCompetition.dll"]