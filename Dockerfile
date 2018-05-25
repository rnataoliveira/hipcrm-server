FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
COPY server.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/bin/Release/netcoreapp2.0/server.dll .
ENTRYPOINT ["dotnet", "server.dll"]