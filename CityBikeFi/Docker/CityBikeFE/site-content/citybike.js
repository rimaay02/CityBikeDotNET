function loadJourneys() {
  const urlJourney = "http://localhost:4545/api/journey";
  const urlStation = "http://localhost:4545/api/station";

  fetch(urlJourney, {
    method: "GET",
  })
    .then((response) => response.json())
    .then((journeys) => {
      const tbody = document.getElementById("journeys");
      tbody.innerHTML = "";
      console.log(typeof filteredJourneys);
      let count = 0;

      journeys.forEach((journey) => {
        if (count >= 100) {
          return;
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

        count++;
      });
    })
    .catch((error) => console.error(error));

  fetch(urlStation, {
    method: "GET",
  })
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
        button.addEventListener("click", () => {
          const stationId = station.fid;
          showStationDetailsPopup(stationId);
        });
		
		detailsCell.appendChild(button);

        function showStationDetailsPopup(stationId) {
          const popupWidth = 400; // Adjust the desired width of the popup
          const popupHeight = 300; // Adjust the desired height of the popup

          // Calculate the position to center the popup
          const screenWidth = window.innerWidth;
          const screenHeight = window.innerHeight;
          const popupLeft = (screenWidth - popupWidth) / 2;
          const popupTop = (screenHeight - popupHeight) / 2;

          // Open the popup window with adjustable size and centered position
          let newWin = window.open(
            "about:blank",
            "hello",
            `width=${popupWidth},height=${popupHeight},left=${popupLeft},top=${popupTop}`
          );

          // Create a loading indicator element
          const loadingElement = document.createElement("p");
          loadingElement.textContent = "Loading...";

          // Append the loading indicator to the popup window
          newWin.document.body.appendChild(loadingElement);

          // Fetch the station details using the stationId
          fetch(`http://localhost:4545/api/station/${stationId}`)
            .then((response) => response.json())
            .then((data) => {
              // Remove the loading indicator
              newWin.document.body.removeChild(loadingElement);

              // Append the station information to the popup window
              newWin.document.write(`<h2>Station Details</h2>`);
              newWin.document.write(
                `<p><strong>Name:</strong> ${data.name}</p>`
              );
              newWin.document.write(
                `<p><strong>Address:</strong> ${data.address}</p>`
              );
              newWin.document.write(
                `<p><strong>Total Journeys Starting:</strong> ${data.totalJourneysStarting}</p>`
              );
              newWin.document.write(
                `<p><strong>Total Journeys Ending:</strong> ${data.totalJourneysEnding}</p>`
              );
            })
            .catch((error) => {
              console.error("Error:", error);
            });
        }

        row.appendChild(detailsCell);

        tbody.appendChild(row);

        count++; // increment the counter variable
        document.getElementById("station-list").style.display = "none";
      });
    })
    .catch((error) => console.error(error));

  const searchInput = document.getElementById("search");
  const searchBtn = document.getElementById("search-btn");

  // Listen for form submit event
  searchBtn.addEventListener("click", (e) => {
    e.preventDefault();
    searchJourneys(searchInput.value);
  });

  // Listen for input change event
  searchInput.addEventListener("input", (e) => {
    searchJourneys(e.target.value);
  });

  // Function to filter journeys based on search term
  function searchJourneys(searchTerm) {
    const journeyRows = document.querySelectorAll("#journeys tr");

    // Loop through all journey rows and show/hide based on search term
    journeyRows.forEach((row) => {
      const departureStation = row
        .querySelector("td:first-child")
        .textContent.toLowerCase();
      const returnStation = row
        .querySelector("td:nth-child(2)")
        .textContent.toLowerCase();

      if (
        departureStation.includes(searchTerm.toLowerCase()) ||
        returnStation.includes(searchTerm.toLowerCase())
      ) {
        row.style.display = "";
      } else {
        row.style.display = "none";
      }
    });
  }
  function searchStations(searchTerm) {
    const stationRows = document.querySelectorAll("#stations tr");

    // Loop through all station rows and show/hide based on search term
    stationRows.forEach((row) => {
      const name = row
        .querySelector("td:first-child")
        .textContent.toLowerCase();
      const address = row
        .querySelector("td:nth-child(2)")
        .textContent.toLowerCase();

      if (
        name.includes(searchTerm.toLowerCase()) ||
        address.includes(searchTerm.toLowerCase())
      ) {
        row.style.display = "";
      } else {
        row.style.display = "none";
      }
    });
  }

  var selectElement = document.getElementById("list-select");
  selectElement.onchange = function showList() {
    var selectedValue =
      selectElement.options[selectElement.selectedIndex].value;

    if (selectedValue === "station") {
      document.getElementById("station-list").style.display = "block";
      document.getElementById("journey-list").style.display = "none";
      searchInput.addEventListener("input", (e) => {
        searchStations(e.target.value);
      });
    } else {
      document.getElementById("station-list").style.display = "none";
      document.getElementById("journey-list").style.display = "block";
      searchInput.addEventListener("input", (e) => {
        searchJourneys(e.target.value);
      });
    }
  };
}

window.addEventListener("load", loadJourneys);
