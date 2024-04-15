FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /SeatAllocationOptimizer

# Copy the csproj file and restore any dependencies (via NuGet)
COPY *.csproj ./
RUN dotnet restore

# Copy the project files and build the release version
COPY . ./
RUN dotnet publish -c Release -o out

# Generate the runtime image using the official Microsoft ASP.NET Core runtime image.
# This is a lighter image that doesn't include the SDK.
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /SeatAllocationOptimizer

# Copy the binaries from the build-env to the new image
COPY --from=build-env /SeatAllocationOptimizer/out .

# Set the command to run the application when the Docker container starts
ENTRYPOINT ["dotnet", "SeatAllocationOptimizer.dll"]
