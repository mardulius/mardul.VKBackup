#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mardul.VKBackup/mardul.VKBackup.csproj", "mardul.VKBackup/"]
RUN dotnet restore "mardul.VKBackup/mardul.VKBackup.csproj"
COPY . .
WORKDIR "/src/mardul.VKBackup"
RUN dotnet build "mardul.VKBackup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mardul.VKBackup.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mardul.VKBackup.dll"]