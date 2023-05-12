FROM mysql:latest

ENV MYSQL_ROOT_PASSWORD=1234

WORKDIR /var/lib/mysql-files/data

ADD https://dev.hsl.fi/citybikes/od-trips-2021/2021-05.csv .
ADD https://dev.hsl.fi/citybikes/od-trips-2021/2021-06.csv .
ADD https://dev.hsl.fi/citybikes/od-trips-2021/2021-07.csv .
ADD https://opendata.arcgis.com/datasets/726277c507ef4914b0aec3cbcfcbfafc_0.csv .

# Set appropriate file permissions
RUN chown -R mysql:mysql /var/lib/mysql-files/data && \
    chmod -R 777 /var/lib/mysql-files/data
	
COPY ./CityBikeDb/create_tables.sql /docker-entrypoint-initdb.d/

