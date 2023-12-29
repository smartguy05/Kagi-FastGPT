FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Test_Plugin/Test_Plugin.csproj", "Test_Plugin/"]
RUN dotnet restore "Test_Plugin/Test_Plugin.csproj"
COPY . .
WORKDIR "/src/Test_Plugin"
RUN dotnet build "Test_Plugin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Test_Plugin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Test_Plugin.dll"]
