FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

ARG BUILD_CONFIGURATION=Development

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["WalletService/WalletService.csproj", "WalletService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "WalletService/WalletService.csproj"
COPY . .
WORKDIR "/src/WalletService"
RUN dotnet build "./WalletService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./WalletService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletService.dll"]
