#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Elbek.MContent.WebHost/Elbek.MContent.WebHost.csproj", "Elbek.MContent.WebHost/"]
RUN dotnet restore "Elbek.MContent.WebHost/Elbek.MContent.WebHost.csproj"
COPY . .
WORKDIR "/src/Elbek.MContent.WebHost"
RUN dotnet build "Elbek.MContent.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elbek.MContent.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elbek.MContent.WebHost.dll"]