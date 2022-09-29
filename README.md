# Advanced Development Project

- Using .NET 6 Web API backend and Angular Frontend
- Using EF framework - being SQL DB for backend
- NoSQl DB for blobs

## Project Setup

### prerequisite

- You will need .NET6 6.0.9 runtime

### setting up

- Clone the project
- `cd \API`
- `dotnet watch run`
- This will build and migrate the seed data into an SQLite db for dev purposes
- Later on we will use a docker Postgress SQL once we have added Identity to the database

## CI/CD Pipelines

The current CI pipeline will build and test dotnet, then follows to upload the artefact for separate job runners and angular frontend will be built and placed into the wwwroot in the /API solution
