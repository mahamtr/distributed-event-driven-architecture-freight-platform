# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Contracts project
COPY ./Contracts ./Contracts

# Copy the OrchestratorService project file and restore dependencies
COPY ./OrchestratorService/OrchestratorService.csproj ./OrchestratorService/
RUN dotnet restore ./OrchestratorService/OrchestratorService.csproj

# Copy the rest of the OrchestratorService application and build it
COPY ./OrchestratorService ./OrchestratorService
RUN dotnet publish ./OrchestratorService -c Release -o out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "OrchestratorService.dll"]