FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KanzApi/KanzApi.csproj", "KanzApi/"]
RUN dotnet restore "KanzApi/KanzApi.csproj"
COPY . .
WORKDIR "/src/KanzApi"
RUN dotnet build "KanzApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KanzApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KanzApi.dll"]
