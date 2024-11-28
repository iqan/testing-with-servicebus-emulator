FROM mcr.microsoft.com/dotnet/sdk:8.0 AS tests
LABEL maintainer="Iqan"
WORKDIR /app
COPY EndToEndTests .
RUN dotnet build
ENTRYPOINT sleep 60 && dotnet test

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Iqan"
WORKDIR /app
COPY FunctionApp .
RUN dotnet publish --configuration Release --output publish --no-self-contained --runtime linux-musl-x64

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0-slim
LABEL maintainer="Iqan"
WORKDIR /home/site/wwwroot
COPY --from=build /app/publish .
