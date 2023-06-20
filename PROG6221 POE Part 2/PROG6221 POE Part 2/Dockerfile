FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PROG6221 POE Part 2/PROG6221 POE Part 2.csproj", "PROG6221 POE Part 2/"]
RUN dotnet restore "PROG6221 POE Part 2/PROG6221 POE Part 2.csproj"
COPY . .
WORKDIR "/src/PROG6221 POE Part 2"
RUN dotnet build "PROG6221 POE Part 2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PROG6221 POE Part 2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PROG6221 POE Part 2.dll"]
