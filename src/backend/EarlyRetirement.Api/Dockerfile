FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EarlyRetirement.Api/EarlyRetirement.Api.csproj", "EarlyRetirement.Api/"]
COPY ["EarlyRetirement.Domain/EarlyRetirement.Domain.csproj", "EarlyRetirement.Domain/"]
COPY ["EarlyRetirement.Infrastructure/EarlyRetirement.Infrastructure.csproj", "EarlyRetirement.Infrastructure/"]
COPY ["EarlyRetirement.Application/EarlyRetirement.Application.csproj", "EarlyRetirement.Application/"]
RUN dotnet restore "EarlyRetirement.Api/EarlyRetirement.Api.csproj"
COPY . .
WORKDIR "/src/EarlyRetirement.Api"
RUN dotnet build "EarlyRetirement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EarlyRetirement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EarlyRetirement.Api.dll"]
