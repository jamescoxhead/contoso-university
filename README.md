# Contoso University

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jamescoxhead_contoso-university&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=jamescoxhead_contoso-university)

Sandbox application for testing patterns, libraries, tools, etc. Based on the [Contoso University](https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-8.0&tabs=visual-studio) tutorial application by Microsoft.

## Getting started
This application requires .NET 8. The database uses [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) which only runs on Windows.

To run the application, either run the *ContosoUniversity.Web* project from Visual Studio, or from your command line go to the *src/ContosoUniversity.Web* folder and run `dotnet run`.

## Workflow
This project tries to follow [Trunk-based development](https://trunkbaseddevelopment.com/). Small changes in the main branch are fine if they do not break the build. Larger changes and external contributions should be in a feature branch off of the main branch. External contributions are merged in via a pull request.

Commit messages follow [conventional commits](https://www.conventionalcommits.org/en/v1.0.0/) guidelines. Commit messages should be prefixed with one of the following:

* ⭐ feat(feature-name) - a new feature
* 🔨 fix - a bugfix
* 🥱 chore - updates to the repo
* 🏗️ ci - updates to CI/CD pipelines
* 📄 docs - documentation updates
* 🔁 refactor - updates and improvements to existing code
* 🖌️ style - formatting updates
* 🧪 test - changes to automated tests

## Technologies
For now, this project uses:
- .NET 8
- Entity Framework Core
- Razor Pages
