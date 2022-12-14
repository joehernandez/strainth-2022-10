# Strainth Development

## Strainth.DataService
- Add or update entities
- Update `StrainthContext`
- Add tests as needed

## Strainth.DataService.Migrations
### Migrations and scripts
- First, create a migration based on changes to entites and `StrainthContext`
    - `dotnet ef migrations add Name-Of-Migration`
- Then generate a script based off of existing migrations
    - `dotnet ef migrations script -o Scripts/_MVP/0000_initial-create.sql -i`
        - `-o`: output
        - `-i`: idempotent
        - Can also add FROM and TO parameters to limit the changes. More info on additional parameters [here](https://www.tektutorialshub.com/entity-framework-core/ef-core-script-migration)
- After generating the script, right-click it in **Solution Explorer**, then **Properties > Advanced > Build Action > Embedded resource**
- To apply the migrations to the database specified in the connection string (see **Configuration** below), run the console app
### Configuration
Connection string kept in User Secrets for local development. Use the command line to add user secrets.
- CD into project directory to add User Secrets to
- Initialize user secrets: `dotnet user-secrets init`
- Add a ConnectionStrings setting: `dotnet user-secrets set "ConnectionStrings:StrainthConnection" "Server=.;Database=Strainth;User Id=sa;Password=<Password-here>;"`
- Get list of user secrets for project: `dotnet user-secrets list`

## SQL Server
### There is a fork of a github repo that has the needed files [here](https://github.com/joehernandez/SqlServerDockerCompose)
#### This repo enables using `docker compose` with a `Dockerfile` and compose file to run SQL Server locally without installing it
- The repo includes a `sapassword.env.example` file. Rename this to `sapassword.env` file and change the password 
- `docker compose build`: only the first time
- `docker compose up`
- Connect using SSMS
    - **Server name:** localhost
    - **Authentication:** SQL Server Authentication
    - **Login:** sa
    - **Password**: *Get this from `sapassword.env` file*

## Visual Studio
- Customize private class field generation when **Rename** is selected: SO answer [here](https://stackoverflow.com/questions/45736659/how-do-i-customize-visual-studios-private-field-generation-shortcut-for-constru#:~:text=Firstly%20you%20need%20to%20define%20a%20new%20naming,it%20will%20be%20named%20with%20a%20leading%20underscore.)

## Markdown help available [here](https://www.markdownguide.org/cheat-sheet)