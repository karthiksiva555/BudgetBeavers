# ==========================
# Stage 1: Build
# ==========================
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

# ==========================
# Stage 2: Final / Runtime
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Non-root user setup
ARG APP_UID=1000
ARG APP_GID=1000
WORKDIR /app
RUN groupadd -g ${APP_GID} appgroup \
  && useradd -m -u ${APP_UID} -g appgroup appuser
USER appuser

# Environment variable for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:5000

# Expose the port your app listens on
EXPOSE 5000

# Copy the published output from build stage
COPY --from=build /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "BudgetBeavers.API.dll"]
