# ==============ビルドステージ=============
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ./Shared/*.csproj ./Shared/
COPY ./realtime-server/*.csproj ./realtime-server/
RUN dotnet restore ./realtime-server/realtime-server.csproj

COPY ./Shared ./Shared
COPY ./realtime-server ./realtime-server
WORKDIR /src/realtime-server
RUN dotnet publish -c Release -o /app/publish

# ==============実行ステージ=============
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:5001
ENV ASPNETCORE_Kestrel__EndpointDefaults__Protocols=Http2
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5001

ENTRYPOINT ["dotnet", "realtime-server.dll"]