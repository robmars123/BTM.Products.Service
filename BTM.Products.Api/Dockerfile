# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BTM.Products.Api/BTM.Products.Api.csproj", "BTM.Products.Api/"]
COPY ["BTM.Products.ApiClient/BTM.Products.ApiClient.csproj", "BTM.Products.ApiClient/"]
COPY ["BTM.Products.Application/BTM.Products.Application.csproj", "BTM.Products.Application/"]
COPY ["BTM.Products.Contracts/BTM.Products.Contracts.csproj", "BTM.Products.Contracts/"]
COPY ["BTM.Products.Domain/BTM.Products.Domain.csproj", "BTM.Products.Domain/"]
COPY ["BTM.Products.Infrastructure/BTM.Products.Infrastructure.csproj", "BTM.Products.Infrastructure/"]
COPY ["BTM.Products.Service.ServiceDefaults/BTM.Products.Service.ServiceDefaults.csproj", "BTM.Products.Service.ServiceDefaults/"]
RUN dotnet restore "./BTM.Products.Api/BTM.Products.Api.csproj"
COPY . .
WORKDIR "/src/BTM.Products.Api"
RUN dotnet build "./BTM.Products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BTM.Products.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BTM.Products.Api.dll"]