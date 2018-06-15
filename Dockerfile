FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
COPY server.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release

FROM microsoft/dotnet:2.1.300-sdk-stretch
WORKDIR /app
COPY --from=build-env /app/bin/Release/netcoreapp2.1/* ./
ENTRYPOINT ["dotnet", "server.dll"] 