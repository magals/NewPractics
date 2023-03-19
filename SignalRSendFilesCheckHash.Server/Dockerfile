#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SignalRSendFilesCheckHash.Server/SignalRSendFilesCheckHash.Server.csproj", "SignalRSendFilesCheckHash.Server/"]
COPY ["SignalRSendFilesCheckHash.Models/SignalRSendFilesCheckHash.Models.csproj", "SignalRSendFilesCheckHash.Models/"]
RUN dotnet restore "SignalRSendFilesCheckHash.Server/SignalRSendFilesCheckHash.Server.csproj"
COPY . .
WORKDIR "/src/SignalRSendFilesCheckHash.Server"
RUN dotnet build "SignalRSendFilesCheckHash.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRSendFilesCheckHash.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalRSendFilesCheckHash.Server.dll"]