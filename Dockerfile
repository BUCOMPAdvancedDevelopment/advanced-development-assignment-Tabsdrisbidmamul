FROM node:lts AS Node
WORKDIR /app
COPY . .
WORKDIR /app/client-app
RUN npm install
RUN npm run build

FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY --from=node /app .
RUN dotnet restore /app/*.sln
WORKDIR /app/API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]