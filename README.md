# DevAdventCalendar

DevAdventCalendar web app for online competition for programmers.

[![Follow DevAdventCalendar](https://img.shields.io/twitter/follow/dev_advent_cal?label=Follow%20%40dev_advent_cal&style=social)](https://twitter.com/dev_advent_cal)
[![Follow DevAdventCalendar](https://img.shields.io/badge/FB-Dev%20Advent%20Calendar-blue)](https://www.facebook.com/devadventcalendar/)

|Build  |Deployment| Quality |
|:-----:|:--------:|:-------:|
|[![Build](https://github.com/DevAdventCalendar/DevAdventCalendar/workflows/Docker%20Image%20CI/badge.svg)](https://github.com/DevAdventCalendar/DevAdventCalendar/actions?query=workflow%3A%22Docker+Image+CI%22)|[![Deploy](https://vsrm.dev.azure.com/plotzwi/_apis/public/Release/badge/e2ad85fa-38da-4937-a85f-997b254f4cda/1/1)](https://dev.azure.com/plotzwi/DevAdventCalendar/_release?_a=releases&view=mine&definitionId=1)|[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=DevAdventCalendar_DevAdventCalendar&metric=alert_status)](https://sonarcloud.io/dashboard?id=DevAdventCalendar_DevAdventCalendar)|

## Getting started

1. Clone repository

    ```git
    git clone https://github.com/WTobor/DevAdventCalendar.git
    ```

2. Open `/src/DevAdventCalendarCompetition/DevAdventCalendarCompetition.sln` in VisualStudio 2019.
3. Install [CodeMaid](http://www.codemaid.net/) to cleanup files
(use default config or just import the one from solution `/src/DevAdventCalendarCompetition/CodeMaid.config`).
4. Install [.Net Core SDK v3.1(https://dotnet.microsoft.com/download/dotnet-core/3.1).

## Contributing

1. Fork it!
2. Checkout branch `develop`

    ```git
    git checkout develop
    ```

3. Create your branch (from branch `develop`)

    ```git
    git checkout -b my-new-feature
    ```

    We are using git-flow, so create branch `feature/new-feature` (for new features) or `hotfix/new-hotfix` (for fixing bugs).

4. Commit your changes (remember to check if code compiles without errors and tests pass)

    ```git
    git commit -m 'Add some feature'
    ```

5. Push to the branch

    ```git
    git push origin my-new-feature
    ```

6. Create a pull request to `develop` branch (the template has already been created, simply complete it)

## Debugging

In order to send e-mails:

* set up an e-mail account in `appsettings.development.json` file

```json
"Email": {
    "Smtp": {	
      "Host": "smtp.gmail.com",	
      "Port": 587,	
      "Username": "email_to_change",
      "Password": "password_to_change"
    }
}
```

* OR execute `docker container run -d -p 8025:8025 -p 1025:1025 mailhog/mailhog` command to run fake SMTP server in [Docker](#docker) container, then you can visit `http://localhost:8025/` to watch your e-mails

Also, it is necessary to add new environment variable `ASPNETCORE_ENVIRONMENT`: `Development` for the main `DevAdventCalendarCompetition` project.

![ENVIRONMENT](docs/Pictures/screen.png/?raw=true)

## Start the application

Just run `docker-compose up` command in the `/src/DevAdventCalendarCompetition/` directory and after successful start of services visit `http://localhost:8081/` in your browser.

## Used Tools

### Swagger

Useful tool to check api endpoints. It is  generated based on Controllers and attributes and can test any rest calls from this page. It is very helpful if you are using not razer page  (Angular etc) or for mobile apps.

Can be access by: pagedomain (or localhost)/swagger/

![Swagger](docs/Pictures/swagger.PNG/?raw=true "Swagger")

[Swagger documentation](https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1)

### Docker

Docker is an open platform that enables developers and administrators to build images, ship, and run distributed applications in a loosely isolated environment called a container. This approach enables efficient application lifecycle management between development, QA, and production environments.

Application is using Docker to work on VPS. Additional Docker-compose helping with configure for all of this

[Docker documentation](https://docs.microsoft.com/pl-pl/dotnet/core/docker/intro-net-docker)

#### MailHog

MailHog is an email testing tool for developers. More info on [GitHub](https://github.com/mailhog/MailHog) page.
