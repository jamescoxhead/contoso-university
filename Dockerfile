ARG BASE_VERSION=8.0-alpine

# Enable globalization for EF Core
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
FROM mcr.microsoft.com/dotnet/aspnet:${BASE_VERSION} AS base
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8
RUN apk update && \
    apk add --no-cache icu-data-full icu-libs
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:${BASE_VERSION} AS build
WORKDIR /source
COPY ./Directory.Build.props ./Directory.Build.props
COPY ./Directory.Packages.props ./Directory.Packages.props
RUN mkdir -p Source/ContosoUniversity.Web && chown app:app Source/ContosoUniversity.Web
COPY ./Source/ContosoUniversity.Web/*.csproj ./Source/ContosoUniversity.Web
RUN dotnet restore ./Source/ContosoUniversity.Web/ContosoUniversity.Web.csproj
COPY . .
RUN dotnet build ./Source/ContosoUniversity.Web/ContosoUniversity.Web.csproj -o /app --no-restore

FROM build AS publish
RUN dotnet publish ./Source/ContosoUniversity.Web/ContosoUniversity.Web.csproj -o /publish --no-restore

FROM base AS final
COPY --from=publish /publish .
ENTRYPOINT [ "./ContosoUniversity.Web" ]
