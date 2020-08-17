# DevAdventCalendar

<p align="center">
<img src="docs/Pictures/dac2019-logo.png" width="400">
</p>

[![Follow DevAdventCalendar](https://img.shields.io/twitter/follow/dev_advent_cal?label=Follow%20%40dev_advent_cal&style=social)](https://twitter.com/dev_advent_cal)
[![Follow DevAdventCalendar](https://img.shields.io/badge/FB-Dev%20Advent%20Calendar-blue)](https://www.facebook.com/devadventcalendar/)

DevAdventCalendar web app for online competition for programmers : [www.devadventcalendar.pl](www.devadventcalenar.pl)

![GitHub issues](https://img.shields.io/github/issues-raw/DevAdventCalendar/DevAdventCalendar)
![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/DevAdventCalendar/DevAdventCalendar)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed-raw/DevAdventCalendar/DevAdventCalendar)

![GitHub contributors](https://img.shields.io/github/contributors/DevAdventCalendar/DevAdventCalendar)
![GitHub last commit](https://img.shields.io/github/last-commit/DevAdventCalendar/DevAdventCalendar)


|Environment |Build  |Deployment| Quality | Coverage |
|:----------:|:-----:|:--------:|:-------:|:--------:|
| PROD |[![Build](https://github.com/DevAdventCalendar/DevAdventCalendar/workflows/Docker%20Image%20CI/badge.svg)](https://github.com/DevAdventCalendar/DevAdventCalendar/actions?query=workflow%3A%22Docker+Image+CI%22) |[![Deploy](https://vsrm.dev.azure.com/plotzwi/_apis/public/Release/badge/e2ad85fa-38da-4937-a85f-997b254f4cda/1/1)](https://dev.azure.com/plotzwi/DevAdventCalendar/_release?_a=releases&view=mine&definitionId=1) |[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=DevAdventCalendar_DevAdventCalendar&metric=alert_status)](https://sonarcloud.io/dashboard?id=DevAdventCalendar_DevAdventCalendar) |[![codecov](https://codecov.io/gh/DevAdventCalendar/DevAdventCalendar/branch/develop/graph/badge.svg)](https://codecov.io/gh/DevAdventCalendar/DevAdventCalendar/branch/develop)|
| DEV |[![Build](https://github.com/DevAdventCalendar/DevAdventCalendar/workflows/Docker%20Image%20CI%20DEV/badge.svg)](https://github.com/DevAdventCalendar/DevAdventCalendar/actions?query=workflow%3A%22Docker+Image+CI+DEV%22) |[![Deploy](https://vsrm.dev.azure.com/plotzwi/_apis/public/Release/badge/e2ad85fa-38da-4937-a85f-997b254f4cda/2/2)](https://dev.azure.com/plotzwi/DevAdventCalendar/_release?_a=releases&view=mine&definitionId=2) | |[![codecov](https://codecov.io/gh/DevAdventCalendar/DevAdventCalendar/branch/develop/graph/badge.svg)](https://codecov.io/gh/DevAdventCalendar/DevAdventCalendar/branch/develop)|


## Projects in solution

- **DevAdventCalendarCompetition** - main project with Controllers and Views
- **DevAdventCalendarCompetition.Services** - class library for logic operations
- **DevAdventCalendarCompetition.Repository** - class library for database operations
- **DevAdventCalendarCompetition.Tests** - unit and integration tests that covers services and controllers (xUnit)

- **DevAdventCalendarCompetition.TestResultService** - external service calculating user points based on a custom algorithm
- **DevAdventCalendarCompetition.TestResultService.Tests** - unit tests for TestResultService (xUnit)

## Contributing

Please read [CONTRIBUTING.md](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/develop/CONTRIBUTING.md) for details.

## Suggest a new feature

We use GitHub issues to track public features. Suggest a new feature by opening a new issue (the template has already been created, simply complete it).

## Report bug

We use GitHub issues to track public bugs. Report a bug by opening a new issue of type bug (the template has already been created, simply complete it).

## Used Tools

### Docker

Docker is an open platform that enables developers and administrators to build images, ship, and run distributed applications in a loosely isolated environment called a container. This approach enables efficient application lifecycle management between development, QA, and production environments.

Application is using Docker to work on VPS. Additional Docker-compose helping with configure for all of this

[Docker documentation](https://docs.microsoft.com/pl-pl/dotnet/core/docker/intro-net-docker)

### Swagger

Useful tool to check api endpoints. It is  generated based on Controllers and attributes and can test any rest calls from this page.
Can be access by: pagedomain (or localhost)/swagger/. [Swagger documentation](https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1)

#### MailHog

MailHog is an email testing tool for developers. More info on [GitHub](https://github.com/mailhog/MailHog) page.
