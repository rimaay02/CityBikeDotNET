FROM mcr.microsoft.com/dotnet/sdk:6.0
ARG WEBAPP_VERSION=0.0.1
LABEL maintainer=rima.ayusinta@gmail.com \
    Name=webapp\
    Version=${WEBAPP_VERSION}
ARG URL_PORT
WORKDIR /app
ENV NUGET_XMLDOC_MODE skip
ENV ASPNETCORE_URLS http://*:${URL_PORT}
ENTRYPOINT [ "dotnet", "CityBikeFiAPI.dll" ]