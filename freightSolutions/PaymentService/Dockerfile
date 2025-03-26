# Use the official .NET 8 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the Contracts project
COPY ./Contracts ./Contracts

# Copy the PaymentService project file and restore dependencies
COPY ./PaymentService/PaymentService.csproj ./PaymentService/
RUN dotnet restore ./PaymentService/PaymentService.csproj

# Copy the rest of the PaymentService application and build it
COPY ./PaymentService ./PaymentService
RUN dotnet publish ./PaymentService -c Release -o out

# Use the official .NET runtime image for the runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "PaymentService.dll"]