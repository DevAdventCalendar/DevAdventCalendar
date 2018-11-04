# DevAdventCalendar
DevAdventCalendar web app for online competition for programmers.

# Getting started
1. Clone repository
```
git clone https://github.com/WTobor/DevAdventCalendar.git
```
2. Open `DevAdventCalendar/src/DevAdventCalendarCompetition/DevAdventCalendarCompetition.sln` in VisualStudio 2017.
3. Install CodeMaid http://www.codemaid.net/ to ceanup files
(use default config or just import the one from solution `DevAdventCalendar/src/DevAdventCalendarCompetition/DevAdventCalendarCompetition/CodeMaid.config`)

# Contributing
1. Fork it!
2. Checkout branch `develop`
```
git checkout develop
```
3. Create your branch (from branch `develop`)
```
git checkout -b my-new-feature
```
we are using git-flow, so create branch `feature/new-feature` (for new features) or `hotfix/new-hotfix` (for fixing bugs)

4. Commit your changes (remember to check if code compiles without errors and tests pass)
```
git commit -m 'Add some feature'
```
5. Push to the branch
```
git push origin my-new-feature
```
6. Create a pull request (the template has already been created, simply complete it)
 

# Used Tools

## Swagger

Useful tool to check api endpoints. It is  generated based on Controllers and attributes and can test any rest calls from this page. It is very helpful if you are using not razer page  (Angular etc) or for mobile apps.

Can be access by; pagedomain (or localhost)/swagger/ 

![Swagger](docs/Pictures/swagger.PNG/?raw=true "Swagger")


Full documentation:  https://docs.microsoft.com/pl-pl/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1 

## Docker

Docker is an open platform that enables developers and administrators to build images, ship, and run distributed applications in a loosely isolated environment called a container. This approach enables efficient application lifecycle management between development, QA, and production environments.

Application is using Docker to work  on VPS. Additional Docker-compose helping with configure for all of this

Full documentation: https://docs.microsoft.com/pl-pl/dotnet/core/docker/intro-net-docker
