# Stage 1: Build C# SDK and Demo
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Copy the library
COPY ButterCMS/ ./ButterCMS/

# Copy the demo app
COPY Demo/ ./Demo/

WORKDIR /src/Demo
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Stage 2: Run the Demo
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build /app/out .

ENV API_KEY=your_api_key
ENV API_BASE_URL=https://api.buttercms.com/

CMD ["dotnet", "buttercms-demo.dll"]