// Imports
import { initializeApp } from "firebase/app";
import { getFirestore, collection, getDocs, doc, deleteDoc, getDoc, setDoc } from 'firebase/firestore';
import { Booking } from '../Controller/Booking.mjs';
import { dateFormatter } from "../Controller/DateFormatter.mjs";

//Firebase setup
const firebaseConfig = {
    apiKey: "",
    authDomain: "",
    projectId: "",
    storageBucket: "",
    messagingSenderId: "",
    appId: ""
};

//Initialize Firebase
const firebase_app = initializeApp(firebaseConfig);
const db = getFirestore(firebase_app);

//Convert Firestore data to and from a Booking object
const bookingConverter = {
    //Converts a booking object to a Firestore document with the given fields
    toFirestore: (booking) => {
        return {
            status: booking.getStatus(),
            firstName: booking.getFirstName(),
            lastName: booking.getLastName(),
            phone: booking.getPhone(),
            mail: booking.getMail(),
            classNr: booking.getClassNr(),
            education: booking.getEducation(),
            participants: booking.getParticipants(),
            start: booking.getStart(),
            end: booking.getEnd(),
            description: booking.getDescription(),
            requests: booking.getRequests(),
            miscellaneous: booking.getMiscellaneous()
        };
    },
    //Converts a Firestore document to a new booking object and returns it
    fromFirestore: (snapshot, options) => {
        const data = snapshot.data(options);
        return new Booking(
            data.status,
            data.firstName,
            data.lastName,
            data.phone,
            data.mail,
            data.classNr,
            data.education,
            data.participants,
            data.start,
            data.end,
            data.description,
            data.requests,
            data.miscellaneous
        );
    }
};

/*
--------------------------------------------------CREATE & UPDATE FUNCTION--------------------------------------------------
*/
//Creates or updates a booking object in our database
//If there is an existing booking with the bookingID, it updates it
//If there is no booking with the bookingID, it creates a new
export async function addOrUpdateBooking(bookingObject) {
    console.log(bookingObject.getFirstName());
    if (bookingObject.getFirstName() == undefined || bookingObject.getFirstName() == null) {
        throw new Error("firstName cannot be null");
    }
    if (bookingObject.getLastName() == undefined || bookingObject.getLastName() == null) {
        throw new Error("lastName cannot be null");
    }
    if (bookingObject.getStart() == undefined || bookingObject.getStart() == null) {
        throw new Error("startDato cannot be null");
    }
    let dato = dateFormatter(bookingObject.getStart());
    //Make bookingID by splitting and trimming first and last name to remove all spaces
    let tempFirstName = "";
    let tempLastName = "";

    let splittedFirstName = bookingObject.getFirstName().split(" ");
    splittedFirstName.forEach(name => {
        tempFirstName += name.trim();
    });

    let splittedLastName = bookingObject.getLastName().split(" ");
    splittedLastName.forEach(name => {
        tempLastName += name.trim();
    });

    //Make bookingID from date, firstname and lastname
    const bookingID = `${dato}${tempFirstName}${tempLastName}`;

    //Create/Update booking
    //If there is a booking with the given BookingID, then update. Otherwise create a new booking
    const ref = doc(db, "Booking", bookingID).withConverter(bookingConverter);
    //setDoc calls toFireStore in the bookingConverter
    await setDoc(ref, new Booking(
        bookingObject.getStatus(),
        bookingObject.getFirstName(),
        bookingObject.getLastName(),
        bookingObject.getPhone(),
        bookingObject.getMail(),
        bookingObject.getClassNr(),
        bookingObject.getEducation(),
        bookingObject.getParticipants(),
        bookingObject.getStart(),
        bookingObject.getEnd(),
        bookingObject.getDescription(),
        bookingObject.getRequests(),
        bookingObject.getMiscellaneous()));
}

/*
-------------------------------------------------------READ FUNCTIONS-------------------------------------------------------
*/

//Read one specific booking from Firestore
//BookingID is the date and name of a booking in Firestore database as a string
export async function readSpecificBooking(bookingID) {
    if (bookingID == null) {
        throw new Error("bookingID cannot be null");
    }
    //Read booking from Firestore, with the given bookingID using bookingConverter to call fromFireStore
    const ref = doc(db, "Booking", bookingID).withConverter(bookingConverter);
    //getDoc calls fromFireStore in the bookingConverter
    const docSnap = await getDoc(ref);

    //If there is a booking with the given bookingID, then return data as a booking object
    if (docSnap.exists()) {
        const booking = docSnap.data();

        let b = new Booking(
            booking.getStatus(),
            booking.getFirstName(),
            booking.getLastName(),
            booking.getPhone(),
            booking.getMail(),
            booking.getClassNr(),
            booking.getEducation(),
            booking.getParticipants(),
            booking.getStart(),
            booking.getEnd(),
            booking.getDescription(),
            booking.getRequests(),
            booking.getMiscellaneous()
        );
        return b;
    } else {
        console.error("Ingen dokumenter for ID: ", bookingID);
    }
}

//Read all bookings from Firestore
export async function readAllBookings() {
    //Read all bookings from Firestore
    const bookingSnapshot = await getDocs(collection(db, "Booking"));
    let bookingData = [];

    //For each element in bookings from Firestore, read all info and run through bookingConverter to call fromFirestore
    for (const element of bookingSnapshot.docs) {
        const ref = doc(db, "Booking", element.id).withConverter(bookingConverter);
        const docSnap = await getDoc(ref);

        //If there is a booking with the given bookingID, then return data as a booking object
        if (docSnap.exists()) {
            const booking = docSnap.data();

            bookingData.push(new Booking(
                booking.getStatus(),
                booking.getFirstName(),
                booking.getLastName(),
                booking.getPhone(),
                booking.getMail(),
                booking.getClassNr(),
                booking.getEducation(),
                booking.getParticipants(),
                booking.getStart(),
                booking.getEnd(),
                booking.getDescription(),
                booking.getRequests(),
                booking.getMiscellaneous()
            ));
        } else {
            console.log("Ingen dokumenter for ID: ", element.id);
        }
    }
    return bookingData;
}

/*
------------------------------------------------------DELETE FUNCTIONS------------------------------------------------------
*/

//Deletes a booking by the given bookingID, from the database
export async function deleteBooking(bookingID) {
    await deleteDoc(doc(db, "Booking", bookingID)).then(result => console.log("result: " + result)).catch(e => console.log("error: " + e));
}