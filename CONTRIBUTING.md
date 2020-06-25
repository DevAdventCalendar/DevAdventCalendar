# DevAdventCalendar - Contributing

## Contributing

When contributing to this repository, please first discuss the change you wish to make via issue, email `devadventcalendar@gmail.com`, or any other method with the owners of this repository before making a change.

## Code of conduct

Please note we have a [CODE_OF_CONDUCT.md](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/develop/CODE_OF_CONDUCT.md), please follow it in all your interactions with the project.

## Board

There is prepared [board](https://github.com/DevAdventCalendar/DevAdventCalendar/projects/1) with tasks grouped by column `To do`, `In progress`, `Needs review`, `Reviewer approved` and `Done`.

## Getting started

1. Clone repository

    ```git
    git clone https://github.com/DevAdventCalendar/DevAdventCalendar.git
    ```

2. Install [.Net Core SDK v3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1). 

3. Open `/src/DevAdventCalendarCompetition/DevAdventCalendarCompetition.sln` in VisualStudio 2019.

## Pull Request

1. Checkout branch `develop`

    ```git
    git checkout develop
    ```

2. Create your feature branch (from branch `develop`)

    ```git
    git checkout -b feature/my-new-feature
    ```

    We are using git-flow, so create branch `feature/new-feature` (for new features) or `bugfix/new-bugfix` (for fixing bugs).

3. Commit your changes (remember to check if code compiles without errors and tests pass)

    ```git
    git commit -m 'Add some feature'
    ```

4. Push to the branch

    ```git
    git push origin my-new-feature
    ```

5. If you've added code that should be tested, add tests.

6. If necessary, update the documentation ([README.md](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/master/README.md)).

7. Make sure your code compiles without errors and tests pass.

8. Create a pull request to the `develop` branch (the template has already been created, simply complete it).

## Start the application

To run the application locally, use the option A) or B).

### A) Visual Studio

Set `DevAdventCalendarCompetition` as a startup project and start debugging (do not use IIS Express).

### B) Docker

Open PowerShell with administrator rights. Go to path `cd <your_path>/src/DevAdventCalendarCompetition/` run `docker-compose build` and `docker-compose up` command in the `/src/DevAdventCalendarCompetition/` directory and after successful start of services visit `http://localhost:8081/` in your browser.

## Test user

There is already created test user with admin rights:

Login: `devadventcalendar@gmail.com`

Password: `P@ssw0rd`

## Test environment

Test environment is running on dev.devadventcalendar.pl with automatic deployment from develop_uat branch.

## Sending emails

Application sends emails with registration informations and notifications. In order to test these functionalities, you must configure the mailbox that the application will use - use option A) or B).

It is necessary to add new environment variable `ASPNETCORE_ENVIRONMENT`: `Development` for the main `DevAdventCalendarCompetition` project.

![ENVIRONMENT](docs/Pictures/screen.png/?raw=true)

### A) Connect your private email

Set up an e-mail account in `appsettings.development.json` file (remember to enable the `"Access less secure applications"` option for the selected mailbox)

```json
"Email": {
    "Smtp": {
      "Host": "smtp.gmail.com",
      "Port": 587,
      "Username": "email_to_change",
      "Password": "password_to_change",
      "From": "email_to_change",
      "Ssl": yes
    }
}
```

### B) Run Docker container

 execute `docker container run -d -p 8025:8025 -p 1025:1025 mailhog/mailhog` command to run fake SMTP server in [Docker](#docker) container, then you can visit `http://localhost:8025/` to watch your e-mails

## Static code analysis

We are using StyleCop and FxCop in every project with rules described in [Analysers.ruleset](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/develop/src/DevAdventCalendarCompetition/Analysers.ruleset) (for non-test projects) and [AnalysersTests.ruleset](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/develop/src/DevAdventCalendarCompetition/AnalysersTests.ruleset) (for test projects).

## License

By contributing, you agree that your contributions will be licensed under MIT License (See [LICENSE](https://github.com/DevAdventCalendar/DevAdventCalendar/blob/develop/LICENSE) for more information).
