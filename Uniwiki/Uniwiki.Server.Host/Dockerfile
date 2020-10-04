#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Uniwiki.Server.Host/Uniwiki.Server.Host.csproj", "Uniwiki.Server.Host/"]
COPY ["Uniwiki.Server.Application/Uniwiki.Server.Application.csproj", "Uniwiki.Server.Application/"]
COPY ["Uniwiki.Server.Persistence/Uniwiki.Server.Persistence.csproj", "Uniwiki.Server.Persistence/"]
COPY ["Server.Persistence/Server.Persistence.csproj", "Server.Persistence/"]
COPY ["Uniwiki.Shared/Uniwiki.Shared.csproj", "Uniwiki.Shared/"]
COPY ["Server.Shared/Shared.csproj", "Server.Shared/"]
COPY ["Uniwiki.Server.Shared/Uniwiki.Server.Shared.csproj", "Uniwiki.Server.Shared/"]
COPY ["Server.Appliaction.Base/Server.Appliaction.csproj", "Server.Appliaction.Base/"]
COPY ["Server.Base.Host/Server.Host.csproj", "Server.Base.Host/"]
COPY ["Uniwiki.Client.Host/Uniwiki.Client.Host.csproj", "Uniwiki.Client.Host/"]
RUN dotnet restore "Uniwiki.Server.Host/Uniwiki.Server.Host.csproj"
COPY . .
WORKDIR "/src/Uniwiki.Server.Host"
RUN dotnet build "Uniwiki.Server.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Uniwiki.Server.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uniwiki.Server.Host.dll"]