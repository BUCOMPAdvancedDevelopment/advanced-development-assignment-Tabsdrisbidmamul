# Advanced Development Project

- Using .NET 6 Web API backend and Angular Frontend
- Using EF framework - being SQL DB for backend
- NoSQL DB for blobs

## Project Setup

### prerequisite

- .NET6 6.0.9 runtime
- Node 16.17.1
- Docker

### setting up

#### Postgres Setup

The backend connects to a postgres db, run this command <br>

```docker
docker run --name shogun-postgres -e POSTGRES_PASSWORD=12345 -e POSTGRES_USER=sa -e POSTGRES_DB=shogun -p 5432:5432 -d postgres
```

This should start up a postgres instance, which your backend can connect to and store data in

#### API Setup

- Clone the project
- `cd \API`
- `dotnet watch run`
- This will build and migrate the seed data into an SQLite db for dev purposes, and will start up the final build in localhost:5000
- We will contacting localhost:5000 for developement and view the final site on localhost:5000 to ensure everything looks okay before deploying to production

#### Frontend Setup

- `cd client-app`
- `npm i`
- `npm run start`
- This will install node modules and start up the frontend on localhost:4200

## Deployment

The current CI pipeline will build and test dotnet, then follows to upload the artefact for separate job runners and angular frontend will be built and placed into the wwwroot in the /API solution

Github Actions will run tests to ensure that the build is all okay

The CD pipeline is hooked up to Google Build and builds must be approved before being deployed to Cloud run on a public URL
