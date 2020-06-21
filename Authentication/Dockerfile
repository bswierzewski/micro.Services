FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Authentication/AuthenticationService.csproj", ""]
RUN dotnet restore "./AuthenticationService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Authentication/AuthenticationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Authentication/AuthenticationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationService.dll"]