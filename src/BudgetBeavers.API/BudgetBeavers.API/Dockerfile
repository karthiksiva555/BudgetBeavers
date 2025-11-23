FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000
ARG APP_GID=1000
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
EXPOSE 8080 8081

# create non-root user and switch
RUN groupadd -g ${APP_GID} appgroup \
  && useradd -m -u ${APP_UID} -g appgroup appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BudgetBeavers.API/BudgetBeavers.API/BudgetBeavers.API.csproj", "BudgetBeavers.API/"]
COPY ["BudgetBeavers.API/BudgetBeavers.Application/BudgetBeavers.Application.csproj", "BudgetBeavers.Application/"]
COPY ["BudgetBeavers.API/BudgetBeavers.Core/BudgetBeavers.Core.csproj", "BudgetBeavers.Core/"]
COPY ["BudgetBeavers.API/BudgetBeavers.Persistence/BudgetBeavers.Persistence.csproj", "BudgetBeavers.Persistence/"]

RUN dotnet restore "BudgetBeavers.API/BudgetBeavers.API.csproj"
COPY . .
RUN echo "=== CURRENT WORKDIR (/src) AFTER COPY ===" && ls -a .
WORKDIR "/src/BudgetBeavers.API/BudgetBeavers.API"
RUN dotnet publish "./BudgetBeavers.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
ARG APP_UID=1000
ARG APP_GID=1000
ENV ASPNETCORE_URLS=http://+:8080
WORKDIR /app
# create same user in final stage (so UID exists) and switch
RUN groupadd -g ${APP_GID} appgroup \
  && useradd -m -u ${APP_UID} -g appgroup appuser
USER appuser
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BudgetBeavers.API.dll"]
