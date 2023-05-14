function loadJourneys() {
  fetch("http://localhost:5000/api/journey")
    .then((response) => response.json())
    .then((journeys) => {
      const tbody = document.getElementById("journeys");
      tbody.innerHTML = "";

      let count = 0; // initialize the counter variable

      journeys.forEach((journey) => {
        if (count >= 100) {
          // check if the maximum number of iterations has been reached
          return; // exit the forEach loop
        }

        const row = document.createElement("tr");
        const departureCell = document.createElement("td");
        departureCell.textContent = journey.departureStation;
        row.appendChild(departureCell);

        const returnCell = document.createElement("td");
        returnCell.textContent = journey.returnStation;
        row.appendChild(returnCell);

        const distanceCell = document.createElement("td");
        distanceCell.textContent = journey.coveredDistance;
        row.appendChild(distanceCell);

        const durationCell = document.createElement("td");
        durationCell.textContent = journey.duration;
        row.appendChild(durationCell);

        tbody.appendChild(row);

        count++; // increment the counter variable
      });
    })
    .catch((error) => console.error(error));

    fetch("http://localhost:5000/api/station")
    .then((response) => response.json())
    .then((stations) => {
      const tbody = document.getElementById("stations");
      tbody.innerHTML = "";

      let count = 0; // initialize the counter variable

      stations.forEach((station) => {
        if (count >= 300) {
          return; 
        }

        const row = document.createElement("tr");
        const nameCell = document.createElement("td");
        nameCell.textContent = station.name;
        row.appendChild(nameCell);

        const addressCell = document.createElement("td");
        addressCell.textContent = station.adress;
        row.appendChild(addressCell);

        const stadCell = document.createElement("td");
        stadCell.textContent = station.stad;
        row.appendChild(stadCell);

        const operatorCell = document.createElement("td");
        operatorCell.textContent = station.operaattor;
        row.appendChild(operatorCell);
        
        const capacityCell = document.createElement("td");
        capacityCell.textContent = station.kapasiteet;
        row.appendChild(capacityCell);

        const xCell = document.createElement("td");
        xCell.textContent = station.x;
        row.appendChild(xCell);
        
        const yCell = document.createElement("td");
        yCell.textContent = station.y;
        row.appendChild(yCell);

        const detailsCell = document.createElement("td");
        const button = document.createElement("button");
        button.textContent = "View Details";
        button.classList.add("station-details");
        detailsCell.appendChild(button);
        button.addEventListener('click', () => {
          const stationId = station.fid;
          window.location.href = `station.html?id=${stationId}`;
        });
        row.appendChild(detailsCell);

        tbody.appendChild(row);


        count++; // increment the counter variable
        document.getElementById("station-list").style.display = "none";
      });
    })
    .catch((error) => console.error(error));

    const searchInput = document.getElementById('search');
    const searchBtn = document.getElementById('search-btn');

    // Listen for form submit event
    searchBtn.addEventListener('click', (e) => {
      e.preventDefault();
      searchJourneys(searchInput.value);
    });

    // Listen for input change event
    searchInput.addEventListener('input', (e) => {
      searchJourneys(e.target.value);
    });

    // Function to filter journeys based on search term
    function searchJourneys(searchTerm) {
      const journeyRows = document.querySelectorAll('#journeys tr');

      // Loop through all journey rows and show/hide based on search term
      journeyRows.forEach((row) => {
        const departureStation = row.querySelector('td:first-child').textContent.toLowerCase();
        const returnStation = row.querySelector('td:nth-child(2)').textContent.toLowerCase();

        if (departureStation.includes(searchTerm.toLowerCase()) || returnStation.includes(searchTerm.toLowerCase())) {
          row.style.display = '';
        } else {
          row.style.display = 'none';
        }
      });
    }
    function searchStations(searchTerm) {
      const stationRows = document.querySelectorAll('#stations tr');
    
      // Loop through all station rows and show/hide based on search term
      stationRows.forEach((row) => {
        const name = row.querySelector('td:first-child').textContent.toLowerCase();
        const address = row.querySelector('td:nth-child(2)').textContent.toLowerCase();
    
        if (name.includes(searchTerm.toLowerCase()) || address.includes(searchTerm.toLowerCase())) {
          row.style.display = '';
        } else {
          row.style.display = 'none';
        }
      });
    }
    
    var selectElement = document.getElementById("list-select");
    selectElement.onchange =  function showList() {
      var selectedValue = selectElement.options[selectElement.selectedIndex].value;
      
      if (selectedValue === "station") {
        document.getElementById("station-list").style.display = "block";
        document.getElementById("journey-list").style.display = "none";
        searchInput.addEventListener('input', (e) => {
          searchStations(e.target.value);
        });
      } else {
        document.getElementById("station-list").style.display = "none";
        document.getElementById("journey-list").style.display = "block";
        searchInput.addEventListener('input', (e) => {
          searchJourneys(e.target.value);
        });
      }
    }
    
    
}

window.addEventListener("load", loadJourneys);
