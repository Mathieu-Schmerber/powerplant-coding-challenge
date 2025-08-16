# powerplant-coding-challenge

## Running the C# Minimal API

This solution is a **C# Minimal API**

### Prerequisites
- [.NET SDK 5 or higher](https://dotnet.microsoft.com/en-us/download)

### Steps to run locally
1. Open a command prompt in the root folder (where `CodingChallenge.sln` is located).
2. Restore dependencies:

```bash
dotnet restore
```

3. Build the project:

```bash
dotnet build
```

4. Run the API:

**Development environment (HTTP):**
```bash
dotnet run --project CodingChallenge/CodingChallenge.csproj --launch-profile Development
```

**Production environment (HTTPS):**
```bash
dotnet run --project CodingChallenge/CodingChallenge.csproj --launch-profile Production
```

**Alternative: Set environment variable directly:**
```bash
# Development (default)
dotnet run --project CodingChallenge/CodingChallenge.csproj

# Production (Windows)
set ASPNETCORE_ENVIRONMENT=Production && dotnet run --project CodingChallenge/CodingChallenge.csproj

# Production (Unix/Mac/Linux)
ASPNETCORE_ENVIRONMENT=Production dotnet run --project CodingChallenge/CodingChallenge.csproj
```

The API will start and listen on:
- **Development**: http://localhost:8888
- **Production**: https://localhost:8888

You can find some sample POST requests within `CodingChallenge/CodingChallenge.http`