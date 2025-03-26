# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Contracts project
COPY ./Contracts ./Contracts

# Copy the InventoryService project file and restore dependencies
COPY ./InventoryService/InventoryService.csproj ./InventoryService/
RUN dotnet restore ./InventoryService/InventoryService.csproj

# Copy the rest of the InventoryService application and build it
COPY ./InventoryService ./InventoryService
RUN dotnet publish ./InventoryService -c Release -o out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "InventoryService.dll"]