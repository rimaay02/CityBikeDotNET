const urlParams = new URLSearchParams(window.location.search);
      const stationId = urlParams.get('id');

      // Get the station data from the API and populate the table
      fetch(`http://localhost:5000/api/station/${stationId}`)
        .then(response => response.json())
        .then(data => {
          document.getElementById('station-name').textContent = data.name;
          document.getElementById('station-address').textContent = data.address;
          document.getElementById('total-journeys-starting').textContent = data.totalJourneysStarting;
          document.getElementById('total-journeys-ending').textContent = data.totalJourneysEnding;
        })
        .catch(error => {
          console.error('Error:', error);
        });

        