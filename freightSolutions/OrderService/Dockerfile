# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Contracts project
COPY ./Contracts ./Contracts

# Copy the OrderService project file and restore dependencies
COPY ./OrderService/OrderService.csproj ./OrderService/
RUN dotnet restore ./OrderService/OrderService.csproj

# Copy the rest of the OrderService application and build it
COPY ./OrderService ./OrderService
RUN dotnet publish ./OrderService -c Release -o out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "OrderService.dll"]