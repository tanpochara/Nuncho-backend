FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Nuncho.WebApi/Nuncho.WebApi.csproj", "Nuncho.WebApi/"]
RUN dotnet restore "Nuncho.WebApi/Nuncho.WebApi.csproj"
COPY . .
WORKDIR "/src/Nuncho.WebApi"
RUN dotnet build "Nuncho.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nuncho.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nuncho.WebApi.dll"]
