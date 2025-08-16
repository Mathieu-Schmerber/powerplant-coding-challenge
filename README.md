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

```bash
dotnet run --project CodingChallenge/CodingChallenge.csproj
```

The API will start and listen on http://localhost:8888.

You can find some sample POST requests within `CodingChallenge/CodingChallenge.http`