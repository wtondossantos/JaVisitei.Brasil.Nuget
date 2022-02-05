#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
MAINTAINER Wellington dos Santos Castor

WORKDIR /src
COPY . .
COPY NuGet.Config ./
RUN dir
RUN dotnet restore --configfile NuGet.Config -nowarn:msb3202,nu1503 --verbosity diag
RUN dotnet publish --output /output --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /nuget
COPY --from=build /output /nuget
ENTRYPOINT ["dotnet", "JaVisitei.Brasil.Nuget.dll"]

WORKDIR /dotnet31

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build