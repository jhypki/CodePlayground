# Base runtime image for the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage using the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Step 1: Copy the project file and restore dependencies
COPY ["CodePlayground/CodePlayground.API/CodePlayground.API.csproj", "CodePlayground.API/"]
RUN dotnet restore "CodePlayground.API/CodePlayground.API.csproj"

# Step 2: Copy the entire source code
COPY . .

# Step 3: Set the working directory to where the .csproj file is located
WORKDIR "/src/CodePlayground/CodePlayground.API"

# Step 4: Build the application
RUN dotnet build "CodePlayground.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CodePlayground.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodePlayground.API.dll"]
