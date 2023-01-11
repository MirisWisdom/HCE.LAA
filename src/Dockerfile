FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HCE.LAA.csproj", "./"]
RUN dotnet restore "HCE.LAA.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "HCE.LAA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HCE.LAA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HCE.LAA.dll"]
