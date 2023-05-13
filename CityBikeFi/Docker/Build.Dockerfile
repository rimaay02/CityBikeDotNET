FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIG=Debug
ARG BUILDER_VERSION=0.0.1
LABEL maintainer=rima.ayusinta@gmail.com \
    Name=webapp-build-${BUILD_CONFIG} \
    Version=${BUILDER_VERSION}
ARG BUILD_LOCATION=/app/out
ENV NUGET_XMLDOC_MODE skip
WORKDIR /app
COPY CityBikeFiAPI/CityBikeFiAPI.csproj .
COPY CityBikeFiAPITests/CityBikeFiAPIUnitTests.csproj .
RUN dotnet restore CityBikeFiAPI.csproj
COPY . /app
RUN dotnet publish /app/CityBikeFiAPI/CityBikeFiAPI.sln --output ${BUILD_LOCATION} --configuration ${BUILD_CONFIG}