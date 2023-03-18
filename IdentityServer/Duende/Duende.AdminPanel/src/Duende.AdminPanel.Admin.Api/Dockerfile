FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Duende.AdminPanel.Admin.Api/Duende.AdminPanel.Admin.Api.csproj", "src/Duende.AdminPanel.Admin.Api/"]
COPY ["src/Duende.AdminPanel.Admin.EntityFramework.Shared/Duende.AdminPanel.Admin.EntityFramework.Shared.csproj", "src/Duende.AdminPanel.Admin.EntityFramework.Shared/"]
COPY ["src/Duende.AdminPanel.Admin.EntityFramework.SqlServer/Duende.AdminPanel.Admin.EntityFramework.SqlServer.csproj", "src/Duende.AdminPanel.Admin.EntityFramework.SqlServer/"]
COPY ["src/Duende.AdminPanel.Shared/Duende.AdminPanel.Shared.csproj", "src/Duende.AdminPanel.Shared/"]
COPY ["src/Duende.AdminPanel.Admin.EntityFramework.PostgreSQL/Duende.AdminPanel.Admin.EntityFramework.PostgreSQL.csproj", "src/Duende.AdminPanel.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/Duende.AdminPanel.Admin.EntityFramework.MySql/Duende.AdminPanel.Admin.EntityFramework.MySql.csproj", "src/Duende.AdminPanel.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/Duende.AdminPanel.Admin.Api/Duende.AdminPanel.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/Duende.AdminPanel.Admin.Api"
RUN dotnet build "Duende.AdminPanel.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Duende.AdminPanel.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "Duende.AdminPanel.Admin.Api.dll"]