//Imports
import express from 'express';
import path from 'path';
import { fileURLToPath } from 'url';
import { addOrUpdateBooking, readSpecificBooking, readAllBookings, deleteBooking } from './Firebase.mjs';
import { dateFormatter } from '../Controller/DateFormatter.mjs';
import { Booking } from '../Controller/Booking.mjs';

//Server setup
const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

const app = express();

app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(express.static(__dirname + '/filer'));

app.set('view engine', 'pug');
app.set('views', path.join(__dirname, '../../GUI/views'));

//Port has to be 8000
const port = 8000;

//Make scripts in model/controller static
app.use(express.static(path.join(__dirname, './Model/Controller')));
app.get('../../GUI', (req, res) => {
    res.sendFile(path.join(__dirname, './Model/Controller', 'BookingBlanket.html'));
});

//Make scripts in model/server static
app.use(express.static((__dirname)));
app.get('/Server', (req, res) => {
    res.sendFile(path.join(__dirname, '/Server', 'BookingBlanket.html'));
});

/*
--------------------------------------------------------------GET FUNCTIONS--------------------------------------------------------------
*/
//Getter for save and get Date functions
app.get('/Model/Controller/sessionStorage.mjs', (req, res) => {
    res.type('application/javascript');
    res.sendFile(path.join(__dirname, '../../Model/Controller/sessionStorage.mjs'));
});

//Getter for Booking accept
app.get('/Model/Controller/BookingAccept.mjs', (req, res) => {
    res.type('application/javascript');
    res.sendFile(path.join(__dirname, '../../Model/Controller/BookingAccept.mjs'));
});

//Getter for kvittering
app.get('/Model/Controller/Kvittering.mjs', (req, res) => {
    res.type('application/javascript');
    res.sendFile(path.join(__dirname, '../../Model/Controller/Kvittering.mjs'));
});

//Getter for Booking object
app.get('/Model/Controller/Booking.mjs', (req, res) => {
    res.type('application/javascript');
    res.sendFile(path.join(__dirname, '../../Model/Controller/Booking.mjs'));
});

//Getter for frontpage
app.get('/', async (req, res) => {
    let bookings = await readAllBookings();
    let bookingsArray = [];
    let bookingsArrayObject = [];

    //Firebase uses timeStamp as date, witch takes seconds and nano seconds
    //This function converts seconds to miliseconds for javascript to use
    for (let ele of bookings) {
        ele.setStart(ele.getStart().seconds * 1000);
        ele.setEnd(ele.getEnd().seconds * 1000);
        if (bookings.indexOf(ele) != bookings.length - 1) {
            bookingsArray.push(ele + "&");
        } else {
            bookingsArray.push(ele + "");
        }
        bookingsArrayObject.push(ele.toObject());
    }

    res.render('frontpage', { bookings: bookingsArray, bookingObject: bookingsArrayObject });
});

//Getter for adminpage
app.get('/admin', async (req, res) => {
    let bookings = await readAllBookings();
    let bookingsArray = [];
    let bookingsArrayObject = [];

    //Firebase uses timeStamp as date, witch takes seconds and nano seconds
    //This function converts seconds to miliseconds for javascript to use
    for (let ele of bookings) {
        ele.setStart(ele.getStart().seconds * 1000);
        ele.setEnd(ele.getEnd().seconds * 1000);
        if (bookings.indexOf(ele) != bookings.length - 1) {
            bookingsArray.push(ele + "&");
        } else {
            bookingsArray.push(ele + "");
        }
        bookingsArrayObject.push(ele.toObject());
    }
    res.render('adminpage', { bookings: bookingsArray, bookingObject: bookingsArrayObject });
});

//Getter for BookingBlanket.html
app.use(express.static(path.join(__dirname, '../../GUI')));
app.get('/BookingBlanket', (req, res) => {
    res.sendFile(path.join(__dirname, "../../GUI", "BookingBlanket.html"));
});

//Getter for BookingConfirmation.html
app.use(express.static(path.join(__dirname, '../../GUI')));
app.get('/BookingConfirmation', (req, res) => {
    res.sendFile(path.join(__dirname, "../../GUI", "BookingConfirmation.html"));
});

/*
---------------------------------------------------------------POST FUNCTIONS---------------------------------------------------------------
*/

//getBooking post
app.post("/getbooking", async (req, res) => {
    let bookingID = req.body.bookingid;
    let result = await readSpecificBooking(bookingID);
    //Change timestamp to seconds, and not the stamp (*1000 - So Date class can reformat)
    result.setStart(result.getStart().seconds * 1000);
    result.setEnd(result.getEnd().seconds * 1000);
    let resultAsText = result.toString();
    res.status(200);
    res.send(resultAsText);
});

//save-booking post
app.post('/save-booking', async (req, res) => {
    //save req booking as data
    const bookingData = req.body.bookingObj;

    //Create new start and end time
    let tempStart = new Date(bookingData.start)
    let tempEnd = new Date(bookingData.end)
    //If the startTime is less than endTime, +1 day on endTime
    //Ex. Start 02/02/2023 17:00:00, End 02/02/2023 02:00:00
    //This means the user tried to book from 02/02/2023 17:00:00 to 03/02/2023 02:00:00
    if (bookingData.start > bookingData.end) {
        tempEnd.setDate(tempEnd.getDate() + 1);
    }
    let tempPhone = Number(bookingData.phone);

    //Create a temp booking object from req data
    let booking = new Booking(bookingData.status, bookingData.firstName, bookingData.lastName, tempPhone, bookingData.mail, bookingData.classNr,
        bookingData.education, bookingData.participants, tempStart, tempEnd, bookingData.description, bookingData.requests, bookingData.miscellaneous
    );

    //Save booking to the database
    addOrUpdateBooking(booking);
    res.sendStatus(200);
});

//deleteBooking post
app.post('/deleteBooking', (req, res) => {
    //Formats the req booking date with dateFormatter, to recieve correct date format for the database
    let time = dateFormatter(new Date(req.body.bookingDate));
    let name = req.body.bookingName;
    //Make ID from time and name
    let id = time + name;
    //Delete booking from database by bookingID
    deleteBooking(id);
    res.sendStatus(201);
});

//updateBookingStatus post
app.post('/updateBookingStatus', (req, res) => {
    //Formats the req booking date with dateFormatter, to recieve correct date format for the database
    let time = dateFormatter(new Date(Number(req.body.bookingDate)));

    //Create temp first and last name to split and trim, to remove all spaces from name and last name
    let tempFirstName = "";
    let tempLastName = "";

    let splittedFirstName = req.body.bookingFirstName.split(" ");
    splittedFirstName.forEach(name => {
        tempFirstName += name.trim();
    });

    let splittedLastName = req.body.bookingLastName.split(" ");
    splittedLastName.forEach(name => {
        tempLastName += name.trim();
    });

    //Create name and ID
    let name = tempFirstName + tempLastName;
    let id = time + name;
    //Get new status from req status
    let newStatus = req.body.bookingStatus;
    //Read the specific booking from database with given ID.
    try {
        readSpecificBooking(id).then((tempBooking) => {
            //Once found, set tempBooking with new status and makes sure the dateformat is correct
            tempBooking.setStatus(newStatus);
            tempBooking.setStart(tempBooking.getStart().toDate());
            tempBooking.setEnd(tempBooking.getEnd().toDate());
            //Delete the old booking from database
            deleteBooking(id);
            //Create new booking in database with correct status and date
            addOrUpdateBooking(tempBooking);
        });
        res.sendStatus(201);
    }
    catch (e) {
        console.log("Fejl: " + e);
    }
});

//updateBooking post
app.post('/updateBooking', async (req, res) => {
    //Get start date from req body and format to correct date
    //req.body.start format is MM/DD/YYYY. We need YYYY/MM/DD
    let tempStartDato = req.body.start.split(" ");
    let tempStartDag = tempStartDato[0].split("-");
    let tempStartTid = tempStartDato[1].split(":");
    let startTime = new Date(tempStartDag[2], tempStartDag[1] - 1, tempStartDag[0], tempStartTid[0], tempStartTid[1], tempStartTid[2]);

    //Get end date from req body and format to correct date
    //req.body.end format is MM/DD/YYYY. We need YYYY/MM/DD
    let tempEndDato = req.body.end.split(" ");
    let tempEndDag = tempEndDato[0].split("-");
    let tempEndTid = tempEndDato[1].split(":");
    let endTime = new Date(tempEndDag[2], tempEndDag[1] - 1, tempEndDag[0], tempEndTid[0], tempEndTid[1], tempEndTid[2]);

    //Create tempBooking object from req input
    let tempBooking = new Booking(
        req.body.status, req.body.firstName, req.body.lastName, req.body.phone, req.body.mail, req.body.classNr, req.body.education, req.body.participants, startTime, endTime, req.body.description, req.body.requests, req.body.miscellaneous
    )
    //Update booking with tempBooking.
    //Firebase creates bookingID from date, firstname and lastname
    try {
        await addOrUpdateBooking(tempBooking);
        res.sendStatus(201);
    }
    catch (e) {
        console.log("Fejl: " + e);
    }
});

//blockDate post
app.post('/blockDate', async (req, res) => {
    //Get tempDay from req
    let tempDay = new Date(req.body.blockDate);
    let tempDate = tempDay.getDate();
    let tempMonth = tempDay.getMonth();
    let tempYear = tempDay.getFullYear();

    //Make start and end time, to block the whole day
    let tempStart = new Date(tempYear, tempMonth, tempDate, 0, 0, 1)
    let tempEnd = new Date(tempYear, tempMonth, tempDate, 23, 59, 0)

    //Create temp booking with specific information, to create a blocked booking object
    let blockBookingDate = new Booking("blocked", "Administrativ blokering", "", 0, "", "", "", 0, tempStart, tempEnd, "", "", "");

    //Add blocked booking date to our database
    await addOrUpdateBooking(blockBookingDate);
    res.sendStatus(201);
})

app.listen(port);

console.log('Lytter på port ' + port);