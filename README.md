# DevAdventCalendar

DevAdventCalendar web app for online competition for programmers.

|Build  |Deployment
|:-----:|:--------:|
|[![Build](https://github.com/DevAdventCalendar/DevAdventCalendar/workflows/Docker%20Image%20CI/badge.svg)](https://github.com/DevAdventCalendar/DevAdventCalendar/actions?query=workflow%3A%22Docker+Image+CI%22)|[![Deploy](https://vsrm.dev.azure.com/plotzwi/_apis/public/Release/badge/e2ad85fa-38da-4937-a85f-997b254f4cda/1/1)](https://dev.azure.com/plotzwi/DevAdventCalendar/_release?_a=releases&view=mine&definitionId=1)

## Getting started

1. Clone repository

    ```git
    git clone https://github.com/WTobor/DevAdventCalendar.git
    ```

2. Open `/src/DevAdventCalendarCompetition/DevAdventCalendarCompetition.sln` in VisualStudio 2019.
3. Install [CodeMaid](http://www.codemaid.net/) to cleanup files
(use default config or just import the one from solution `/src/DevAdventCalendarCompetition/CodeMaid.config`).
4. Install [.Net Core SDK v2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2).

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

## Used Tools

### Swagger

Useful tool to check api endpoints. It is  generated based on Controllers and attributes and can test any rest calls from this page. It is very helpful if you are using not razer page  (Angular etc) or for mobile apps.

Can be access by: pagedomain (or localhost)/swagger/

![Swagger](docs/Pictures/swagger.PNG/?raw=true "Swagger")

[Swagger documentation](https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1)

### Docker

Docker is an open platform that enables developers and administrators to build images, ship, and run distributed applications in a loosely isolated environment called a container. This approach enables efficient application lifecycle management between development, QA, and production environments.

Application is using Docker to work  on VPS. Additional Docker-compose helping with configure for all of this

[Docker documentation](https://docs.microsoft.com/pl-pl/dotnet/core/docker/intro-net-docker)
