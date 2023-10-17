# syntax=docker/dockerfile:1
ARG VERSION=7.0

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env
#FROM ${BASE_IMAGE_REPO}/${BASE_IMAGE_RUNTIME}:${BASE_IMAGE_TAG} as base
WORKDIR /src

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:$VERSION as final
LABEL maintainer="Admir Trakic <atrakic@users.noreply.github.com>"
WORKDIR /app
COPY --from=build-env /src/out .

RUN set -x \
    && apt-get update \
    && apt-get install --no-install-recommends --no-install-suggests -y curl \
    && apt-get remove --purge --auto-remove -y && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "api.dll"]
