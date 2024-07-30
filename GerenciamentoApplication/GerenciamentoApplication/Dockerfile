#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GerenciamentoApplication/GerenciamentoApplication.csproj", "GerenciamentoApplication/"]
COPY ["Gerenciamento.Application/Gerenciamento.Application.csproj", "Gerenciamento.Application/"]
COPY ["Gerenciamento.Domain/Gerenciamento.Domain.csproj", "Gerenciamento.Domain/"]
COPY ["Gerenciamento.DbAdapter/Gerenciamento.DbAdapter.csproj", "Gerenciamento.DbAdapter/"]
RUN dotnet restore "./GerenciamentoApplication/./GerenciamentoApplication.csproj"
COPY . .
WORKDIR "/src/GerenciamentoApplication"
RUN dotnet build "./GerenciamentoApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GerenciamentoApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciamentoApplication.dll"]
