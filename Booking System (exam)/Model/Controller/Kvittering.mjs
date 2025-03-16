//[0] = yyyy, [1] = mm, [2] = dd
//Gets date from sessionStorage
let dateSplitter = sessionStorage.getItem("Dato").split("-");

let tempFirstName = "";
let tempLastName = "";

//Gets firstname from sessionStorage
let fornavnSplitter = sessionStorage.getItem("Fornavn").split(" ");
fornavnSplitter.forEach(name => {
    tempFirstName += name.trim();
});

//Gets lastname from sessionStorage
let efternavnSplitter = sessionStorage.getItem("Efternavn").split(" ");
efternavnSplitter.forEach(name => {
    tempLastName += name.trim();
});

//Create bookingID from date, firstname and lastname
let bookingID = String(dateSplitter[2] + dateSplitter[1] + dateSplitter[0] + tempFirstName + tempLastName);

//getBooking from bookingID
async function getBooking(bookingID) {
    //Stringify bookingID, to call on express server
    let jsonString = JSON.stringify({ bookingid: bookingID });
    try {
        //Fetch with express server
        const response = await fetch("http://localhost:8000/getbooking", {
            method: "POST",
            body: jsonString,
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return await response.text();
    }
    catch (error) {
        console.log(error);
    }
};

//Set information with the given information returned form database.
function setEventInfo(commaSepString) {
    let resultSplitter = commaSepString.split(", ");

    //Sets the field with info from pressed event
    let startDate = new Date(Number(resultSplitter[8]));
    let endDate = new Date(Number(resultSplitter[9]));

    document.querySelector("#navn").innerHTML = `${resultSplitter[1]} ${resultSplitter[2]}`;
    document.querySelector("#telefonnr").innerHTML = resultSplitter[3];
    document.querySelector("#email").innerHTML = resultSplitter[4];
    document.querySelector("#holdnr").innerHTML = resultSplitter[5];
    document.querySelector("#uddannelse").innerHTML = resultSplitter[6];
    document.querySelector("#antalGaester").innerHTML = resultSplitter[7];
    document.querySelector("#starttidspunkt").innerHTML = `${startDate.getDate()}-${startDate.getMonth()}-${startDate.getFullYear()} ${(startDate.getHours() < 10) ? "0" + startDate.getHours() : startDate.getHours()}:${(startDate.getMinutes() < 10) ? "0" + startDate.getMinutes() : startDate.getMinutes()}:${(startDate.getSeconds() < 10) ? "0" + startDate.getSeconds() : startDate.getSeconds()}`;
    document.querySelector("#sluttidspunkt").innerHTML = `${endDate.getDate()}-${endDate.getMonth()}-${endDate.getFullYear()} ${(endDate.getHours() < 10) ? "0" + endDate.getHours() : endDate.getHours()}:${(endDate.getMinutes() < 10) ? "0" + endDate.getMinutes() : endDate.getMinutes()}:${(endDate.getSeconds() < 10) ? "0" + endDate.getSeconds() : endDate.getSeconds()}`;
    document.querySelector("#beskrivelse").innerHTML = resultSplitter[10];
    document.querySelector("#specielleOensker").innerHTML = resultSplitter[11];
    document.querySelector("#diverse").innerHTML = resultSplitter[12];
}

//Gets info from booking and prints on receipt
getBooking(bookingID)
    .then(result => {
        setEventInfo(result);
    }).catch(error => { console.log(error) });