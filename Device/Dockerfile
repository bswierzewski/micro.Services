FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Device/DeviceService.csproj", ""]
RUN dotnet restore "./DeviceService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Device/DeviceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Device/DeviceService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeviceService.dll"]