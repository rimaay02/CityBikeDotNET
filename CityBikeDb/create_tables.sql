CREATE DATABASE citybike_fi;
USE citybike_fi;

CREATE TABLE Stations (
  FID int,
  ID int,
  Nimi varchar(255),
  Namn varchar(255),
  Name varchar(255),
  Osoite varchar(255),
  Adress varchar(255),
  Kaupunki varchar(255),
  Stad varchar(255),
  Operaattor varchar(255),
  Kapasiteet int,
  x decimal(10,6),
  y decimal(10,6)
);
CREATE TABLE Journey (
  departure timestamp,
  `return` timestamp,
  departure_station_id int,
  departure_station_name varchar(255),
  return_station_id int,
  return_station_name varchar(255),
  covered_distance int,
  duration int
);
LOAD DATA INFILE '/var/lib/mysql-files/data/726277c507ef4914b0aec3cbcfcbfafc_0.csv'
IGNORE
INTO TABLE Stations
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 ROWS
(FID, ID, Nimi, Namn, Name, Osoite, Adress, Kaupunki, Stad, Operaattor, Kapasiteet, x, y);

LOAD DATA INFILE '/var/lib/mysql-files/data/2021-05.csv'
IGNORE
INTO TABLE Journey
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 ROWS
(departure, `return`, departure_station_id, departure_station_name, return_station_id, return_station_name, covered_distance, duration);

LOAD DATA INFILE '/var/lib/mysql-files/data/2021-06.csv'
IGNORE
INTO TABLE Journey
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 ROWS
(departure, `return`, departure_station_id, departure_station_name, return_station_id, return_station_name, covered_distance, duration);

LOAD DATA INFILE '/var/lib/mysql-files/data/2021-07.csv'
IGNORE
INTO TABLE Journey
FIELDS TERMINATED BY ','
LINES TERMINATED BY '\n'
IGNORE 1 ROWS
(departure, `return`, departure_station_id, departure_station_name, return_station_id, return_station_name, covered_distance, duration);

SET
  FID = IF(FID REGEXP '^[0-9]+$' AND FID >= 0, FID, NULL),
  ID = IF(ID REGEXP '^[0-9]+$' AND ID >= 0, ID, NULL),
  Nimi = IF(CHAR_LENGTH(Nimi) <= 255, Nimi, NULL),
  Namn = IF(CHAR_LENGTH(Namn) <= 255, Namn, NULL),
  Name = IF(CHAR_LENGTH(Name) <= 255, Name, NULL),
  Osoite = IF(CHAR_LENGTH(Osoite) <= 255, Osoite, NULL),
  Adress = IF(CHAR_LENGTH(Adress) <= 255, Adress, NULL),
  Kaupunki = IF(CHAR_LENGTH(Kaupunki) <= 255, Kaupunki, NULL),
  Stad = IF(CHAR_LENGTH(Stad) <= 255, Stad, NULL),
  Operaattor = IF(CHAR_LENGTH(Operaattor) <= 255, Operaattor, NULL),
  Kapasiteet = IF(Kapasiteet REGEXP '^[0-9]+$' AND Kapasiteet >= 0, Kapasiteet, NULL),
  x = IF(x REGEXP '^[0-9]+(\\.[0-9]+)?$' AND x >= -180 AND x <= 180, x, NULL),
  y = IF(y REGEXP '^[0-9]+(\\.[0-9]+)?$' AND y >= -90 AND y <= 90, y, NULL)
  departure = STR_TO_DATE(departure, '%Y-%m-%d %H:%i:%s'),
  `return` = STR_TO_DATE(`return`, '%Y-%m-%d %H:%i:%s'),
  departure_station_id = IF(departure_station_id REGEXP '^[0-9]+$',
                            departure_station_id, NULL),
  departure_station_name = IF(CHAR_LENGTH(departure_station_name) <= 255,
                              departure_station_name, NULL),
  return_station_id = IF(return_station_id REGEXP '^[0-9]+$',
                         return_station_id, NULL),
  return_station_name = IF(CHAR_LENGTH(return_station_name) <= 255,
                           return_station_name, NULL),
  covered_distance = IF(covered_distance REGEXP '^[0-9]+$' AND covered_distance >= 10,
                        covered_distance, NULL),
  duration = IF(duration REGEXP '^[0-9]+$' AND duration >= 10,
                 duration, NULL);
  
				 
