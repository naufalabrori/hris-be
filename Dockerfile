# Gunakan image .NET Runtime sebagai base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Gunakan image .NET SDK untuk build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy file solusi dan proyek
COPY ["HRIS.BE.sln", "."]
COPY ["Api/Api.csproj", "Api/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restore semua dependensi
RUN dotnet restore "HRIS.BE.sln"

# Copy seluruh source code
COPY . .

# Build seluruh solusi
RUN dotnet build "HRIS.BE.sln" -c Release --no-restore -o /app/build

# Publish proyek Api
FROM build AS publish
RUN dotnet publish "HRIS.BE.sln" -c Release -o /app/publish --no-build

# Gunakan image base untuk stage final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]