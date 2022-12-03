#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AvaliaAe/AvaliaAe.csproj", "AvaliaAe/"]
RUN dotnet restore "AvaliaAe/AvaliaAe.csproj"
COPY . .
WORKDIR "/src/AvaliaAe"
RUN dotnet build "AvaliaAe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvaliaAe.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN Update-Database

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvaliaAe.dll"]