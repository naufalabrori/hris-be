FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy semua file solusi dan proyek
COPY ["HRIS.BE.sln", "."]
COPY ["Api/Api.csproj", "Api/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

# Restore semua dependensi
RUN dotnet restore "Api/Api.csproj"

# Copy seluruh source code
COPY . .

# Build seluruh proyek
RUN dotnet build "Api/Api.csproj" -c Release --no-restore -o /app/build

FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release --no-build -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
