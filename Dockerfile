FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5189

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdGeneratorServer.Web/IdGeneratorServer.Web.csproj", "IdGeneratorServer.Web/"]
COPY ["IdGeneratorServer.Application/IdGeneratorServer.Application.csproj", "IdGeneratorServer.Application/"]
COPY ["IdGeneratorServer.Application.Constant/IdGeneratorServer.Application.Constant.csproj", "IdGeneratorServer.Application.Constant/"]
COPY ["IdGeneratorServer.Snowflake.YitterId/IdGeneratorServer.Snowflake.YitterId.csproj", "IdGeneratorServer.Snowflake.YitterId/"]
COPY ["IdGeneratorServer.Snowflake/IdGeneratorServer.Snowflake.csproj", "IdGeneratorServer.Snowflake/"]
RUN dotnet restore "IdGeneratorServer.Web/IdGeneratorServer.Web.csproj"
COPY . .
WORKDIR "/src/IdGeneratorServer.Web"
RUN dotnet build "IdGeneratorServer.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IdGeneratorServer.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdGeneratorServer.Web.dll"]
