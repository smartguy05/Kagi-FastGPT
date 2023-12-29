FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Kagi_gpt/Kagi_gpt.csproj", "Kagi_gpt/"]
RUN dotnet restore "Kagi_gpt/Kagi_gpt.csproj"
COPY . .
WORKDIR "/src/Kagi_gpt"
RUN dotnet build "Kagi_gpt.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kagi_gpt.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kagi_gpt.dll"]
