#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/ProductCatalog/ProductCatalog.API/ProductCatalog.API.csproj", "Services/ProductCatalog/ProductCatalog.API/"]
COPY ["Services/ProductCatalog/ProductCatalog.BusinessObjects/ProductCatalog.BusinessObjects.csproj", "Services/ProductCatalog/ProductCatalog.BusinessObjects/"]
COPY ["Services/ProductCatalog/ProductCatalog.Domain/ProductCatalog.Domain.csproj", "Services/ProductCatalog/ProductCatalog.Domain/"]
COPY ["Services/ProductCatalog/ProductCatalog.Repositories/ProductCatalog.Repositories.csproj", "Services/ProductCatalog/ProductCatalog.Repositories/"]
COPY ["Services/ProductCatalog/ProductCatalog.EFRepositories/ProductCatalog.EFRepositories.csproj", "Services/ProductCatalog/ProductCatalog.EFRepositories/"]
RUN dotnet restore "Services/ProductCatalog/ProductCatalog.API/ProductCatalog.API.csproj"
COPY . .
WORKDIR "/src/Services/ProductCatalog/ProductCatalog.API"
RUN dotnet build "ProductCatalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCatalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.API.dll"]
