FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore /app/*.sln

WORKDIR /app/client-app

COPY ./client-app .

FROM node:lts

RUN npm install

RUN npm run build

WORKDIR /app/API

RUN dotnet publish -c Release -o out

WORKDIR /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

COPY --from=build /app/API/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]