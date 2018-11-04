FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.sln .
COPY ./DevAdventCalendarCompetition/*.csproj ./DevAdventCalendarCompetition/
COPY ./DevAdventCalendarCompetition.Repository/*.csproj ./DevAdventCalendarCompetition.Repository/
COPY ./DevAdventCalendarCompetition.Services/*.csproj ./DevAdventCalendarCompetition.Services/
RUN dotnet restore


# copy everything else and build app
COPY . .
WORKDIR /app/DevAdventCalendarCompetition
RUN dotnet publish -c Release -o /out


FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /out
COPY --from=build /out .
ENTRYPOINT ["dotnet", "DevAdventCalendarCompetition.dll"]