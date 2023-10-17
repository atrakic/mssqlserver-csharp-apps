# syntax=docker/dockerfile:1
ARG BASE_IMAGE_TAG=7.0
ARG BASE_IMAGE_REPO=mcr.microsoft.com
ARG BASE_IMAGE_BUILD=dotnet/sdk
ARG BASE_IMAGE_RUNTIME=dotnet/runtime

FROM ${BASE_IMAGE_REPO}/${BASE_IMAGE_BUILD}:${BASE_IMAGE_TAG} as base
WORKDIR /app
ARG PROJNAME="app.csproj"

FROM base AS build-env
WORKDIR /src
COPY . .
RUN dotnet build "$PROJFILE" -c Release -o /app/build

FROM build-env AS publish
RUN dotnet publish "$PROJFILE" -c Release -o /app/publish

# https://stackoverflow.com/a/71046731/5744510
FROM ${BASE_IMAGE_REPO}/${BASE_IMAGE_RUNTIME}:${BASE_IMAGE_TAG}-alpine AS final
LABEL maintainer="Admir Trakic <atrakic@users.noreply.github.com>"
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "app.dll"]
