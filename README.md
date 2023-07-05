# City Bike App
Thi City Bike App using .NET core as the rest API and MYSQL as its database. The frontend using HTML,CSS and JavaScript.
The unit tests and integration tests also has been done for this app.
NGINX used as reverse proxy. The app allows the user to view journey and station list that is retrieved from City Bike Finland.
Additionally, the user can also search the station using search box above the table that contains in both station and journey tables.
In Station table the user can view the details of the station by using "view details" button.

Instruction:
1. make sure docker and docker compose is installed in your local machine.
2. download the folder and go inside the Docker folder inside this project in your terminal
3. Run these commands
```
docker-compose down -v
docker-compose up
```
4. wait until the screen in the terminal show this state
<img width="410" alt="image" src="https://github.com/rimaay02/SolitaDevAcademyAssignment/assets/70073803/d588ed41-4609-4562-86ef-4d672e45466c">

5. The user can access the app on http://localhost:5500/
6. [optional] the user also can run the tests at this stage using Visual Studio, providing the user has the source code.
