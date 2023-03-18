FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Duende.AdminPanel.STS.Identity/Duende.AdminPanel.STS.Identity.csproj", "src/Duende.AdminPanel.STS.Identity/"]
COPY ["src/Duende.AdminPanel.Admin.EntityFramework.Shared/Duende.AdminPanel.Admin.EntityFramework.Shared.csproj", "src/Duende.AdminPanel.Admin.EntityFramework.Shared/"]
COPY ["src/Duende.AdminPanel.Shared/Duende.AdminPanel.Shared.csproj", "src/Duende.AdminPanel.Shared/"]
RUN dotnet restore "src/Duende.AdminPanel.STS.Identity/Duende.AdminPanel.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/Duende.AdminPanel.STS.Identity"
RUN dotnet build "Duende.AdminPanel.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Duende.AdminPanel.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Duende.AdminPanel.STS.Identity.dll"]