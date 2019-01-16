FROM microsoft/dotnet:3.0-runtime-stretch-slim AS base
WORKDIR /app

FROM microsoft/dotnet:3.0-sdk-stretch AS build
WORKDIR /src
COPY 01Generic/Generic.csproj 01Generic/
RUN dotnet restore 01Generic/Generic.csproj
COPY . .
WORKDIR /src/01Generic
RUN dotnet build Generic.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Generic.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Generic.dll"]
