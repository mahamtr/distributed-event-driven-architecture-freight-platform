# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Contracts project
COPY ./Contracts ./Contracts

# Copy the NotificationService project file and restore dependencies
COPY ./NotificationService/NotificationService.csproj ./NotificationService/
RUN dotnet restore ./NotificationService/NotificationService.csproj

# Copy the rest of the NotificationService application and build it
COPY ./NotificationService ./NotificationService
RUN dotnet publish ./NotificationService -c Release -o out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "NotificationService.dll"]