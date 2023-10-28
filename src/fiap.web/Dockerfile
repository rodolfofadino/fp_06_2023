#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/fiap.web/fiap.web.csproj", "src/fiap.web/"]
COPY ["src/fiap.application/fiap.application/fiap.application.csproj", "src/fiap.application/fiap.application/"]
COPY ["src/fiap.domain/fiap.domain/fiap.domain.csproj", "src/fiap.domain/fiap.domain/"]
COPY ["src/fiap.infrastructure/fiap.infrastructure/fiap.infrastructure.csproj", "src/fiap.infrastructure/fiap.infrastructure/"]
COPY ["src/fiap.IoC/fiap.IoC/fiap.IoC.csproj", "src/fiap.IoC/fiap.IoC/"]
COPY ["src/fiap.persistence/fiap.persistence/fiap.persistence.csproj", "src/fiap.persistence/fiap.persistence/"]
RUN dotnet restore "src/fiap.web/fiap.web.csproj"
COPY . .
WORKDIR "/src/src/fiap.web"
RUN dotnet build "fiap.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fiap.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fiap.web.dll"]