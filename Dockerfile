# Use the official .NET 10.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image using the ASP.NET 10.0 runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configure ASP.NET Core environment variables for containerized execution
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_HTTP_PORTS=8080

# Expose port 8080 for connection
EXPOSE 8080

ENTRYPOINT ["dotnet", "WisdomWall.dll"]
