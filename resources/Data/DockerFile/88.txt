FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine3.10 AS build

# this addes .NET Core 2.1 to the build image so that 
# Aspect Injector can do its thing.
RUN wget -O dotnet.tar.gz https://download.visualstudio.microsoft.com/download/pr/64d8b249-f3d0-4ad9-89f9-9adfc909d3b0/aad4f4a97b6e9dcc97263d986eec1627/aspnetcore-runtime-2.1.14-linux-musl-x64.tar.gz \
    && tar -C /usr/share/dotnet -xzf dotnet.tar.gz \
    && rm dotnet.tar.gz

WORKDIR /app

ENV DOTNET_CLI_TELEMETRY_OPTOUT 1

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -r linux-musl-x64 -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine3.10 AS publish

# add any env vars needed here
ENV APP_ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
EXPOSE 80 443
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "<your app's name>.dll"]