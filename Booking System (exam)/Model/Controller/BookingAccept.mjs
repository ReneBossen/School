import { Booking } from './Booking.mjs'

let booking = null;

//Function for BookingBlanket, to confirm booking with the input fields
export function handleBookingButtonClick() {
    //Retrieves data from input
    const firstName = document.getElementById("fornavn").value;
    sessionStorage.setItem("Fornavn", firstName);
    const lastName = document.getElementById("efternavn").value;
    sessionStorage.setItem("Efternavn", lastName);
    const phone = document.getElementById("tlfnr").value;
    const mail = document.getElementById("email").value;
    const education = document.getElementById("uddannelse").value;
    const classNumber = document.getElementById("holdnr").value;
    const participants = parseInt(document.getElementById("antal").value, 10);
    const description = document.getElementById("beskrivelse").value;
    const requests = document.getElementById("ønsker").value;
    const miscellaneous = document.getElementById("diverse").value;
    const status = "waiting";
    const tempdate = sessionStorage.getItem("Dato").split("-");
    const tempstart = document.getElementById("starttidspunkt").value.split(":");
    const tempend = document.getElementById("sluttidspunkt").value.split(":");
    const startdate = new Date(tempdate[0], (tempdate[1] - 1), tempdate[2], tempstart[0], tempstart[1]);
    const enddate = new Date(tempdate[0], (tempdate[1] - 1), tempdate[2], tempend[0], tempend[1]);

    //Creates a temp booking object from given input
    booking = new Booking(status, firstName, lastName, phone, mail, classNumber,
        education, participants, startdate, enddate, description, requests, miscellaneous
    );
}

//Creates an eventlistener for AcceptButton
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("AcceptButton").addEventListener("click", async function (event) {
        event.preventDefault();

        //Checks if all input fields are filled
        switch (true) {
            case document.getElementById("fornavn").value.trim() === "":
                alert("Fornavn er ikke udfyldt");
                break;
            case document.getElementById("efternavn").value.trim() === "":
                alert("Efternavn er ikke udfyldt");
                break;
            case document.getElementById("tlfnr").value.trim() === "":
                alert("Tlfnr er ikke udfyldt");
                break;
            case document.getElementById("email").value.trim() === "":
                alert("Email er ikke udfyldt");
                break;
            case document.getElementById("uddannelse").value.trim() === "":
                alert("Uddannelse er ikke udfyldt");
                break;
            case document.getElementById("holdnr").value.trim() === "":
                alert("Holdnr er ikke udfyldt");
                break;
            case document.getElementById("starttidspunkt").value === "":
                alert("Starttidspunkt er ikke sat");
                break;
            case document.getElementById("sluttidspunkt").value === "":
                alert("Sluttidspunkt er ikke sat");
                break;
            case document.getElementById("antal").value.trim() === "":
                alert("Antal er ikke sat");
                break;
            case document.getElementById("beskrivelse").value.trim() === "":
                alert("Indtast venligst en beskrivelse for dit arrangement");
                break;
            case document.getElementById("ønsker").value.trim() === "":
                alert("Indtast venligst specielle ønsker for dit arrangement");
                break;
            case !document.getElementById("reglement").checked:
                alert("Du har ikke accepteret Basements reglement")
                break;

            //If every input field is fullfilled, stringify object and send to express server
            default:
                handleBookingButtonClick();
                let bookingObject = booking.toObject();
                let jsonString = JSON.stringify({ bookingObj: bookingObject });
                try {
                    //Fetch with express server, to save booking in database with "waiting" status
                    const response = await fetch("http://localhost:8000/save-booking", {
                        method: "POST",
                        body: jsonString,
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    });
                    let status = response.status;
                    if (status === 200) {
                        window.location.href = 'BookingConfirmation.html';
                    }
                }
                catch (error) {
                    console.log(error);
                }
        };
    })
})
