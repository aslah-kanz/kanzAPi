FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /kanzapi
EXPOSE 80
EXPOSE 443

# Copy the certificates
COPY --from=source /certs /app/certs

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KanzApi.csproj", "./"]
RUN dotnet restore "KanzApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "KanzApi.csproj" -c Release -o /kanzapi/build

FROM build AS publish
RUN dotnet publish "KanzApi.csproj" -c Release -o /kanzapi/publish

FROM base AS final
WORKDIR /kanzapi
COPY --from=publish /kanzapi/publish .
COPY appsettings.json /kanzapi/appsettings.json
COPY appsettings.Development.json /kanzapi/appsettings.Development.json
ENTRYPOINT ["dotnet", "KanzApi.dll"]
