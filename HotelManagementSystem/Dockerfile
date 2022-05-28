FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HotelManagementSystem/HotelManagementSystem.csproj", "HotelManagementSystem/"]
RUN dotnet restore "HotelManagementSystem/HotelManagementSystem.csproj"

RUN dotnet ef --project .\HotelManagementSystem\ database update

COPY . .
WORKDIR "/src/HotelManagementSystem"
RUN dotnet build "HotelManagementSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelManagementSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelManagementSystem.dll"]
