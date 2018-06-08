FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY server.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release

FROM microsoft/aspnetcore:2.1
WORKDIR /app
COPY --from=build-env /app/bin/Release/netcoreapp2.1/* ./
ENTRYPOINT ["dotnet", "server.dll"]