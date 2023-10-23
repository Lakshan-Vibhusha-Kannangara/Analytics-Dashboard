# Use the official .NET SDK 6.0 image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the .csproj file and restore any dependencies
COPY ./Api/Api.csproj ./Api/
RUN dotnet restore ./Api/Api.csproj

# Copy the rest of the application source code
COPY ./Api ./Api/
WORKDIR /app/Api

# Build the application
RUN dotnet publish -c Release -o out

# Create the final image with the .NET 6.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Api/out ./

# Set the entry point
ENTRYPOINT ["dotnet", "Api.dll"]
