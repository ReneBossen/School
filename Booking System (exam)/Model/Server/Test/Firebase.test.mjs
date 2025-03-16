import { assert } from 'chai';
import { Booking } from '../../Controller/Booking.mjs';
import { addOrUpdateBooking, readSpecificBooking, readAllBookings, deleteBooking } from '../Firebase.mjs';

//Create 10 test booking objects
const b = new Booking(
    "waiting",
    "Jonas",
    "Christensen",
    23764209,
    "jonas@eaaa.test.dk",
    "22S",
    "Datamatiker",
    10,
    new Date(2024, 0, 24, 17, 0, 0),
    new Date(2024, 0, 24, 23, 59, 0),
    "Fødselsdagsfest med klassen",
    "Rundborde midt i lokalet og beerpong borde i kanten",
    "Jeg fylder 31"
);

const b1 = new Booking(
    "waiting",
    "Daniel",
    "Andersen",
    34557643,
    "daniel@eaaa.test.dk",
    "22S",
    "Datamatiker",
    25,
    new Date(2024, 1, 24, 17, 0, 0),
    new Date(2024, 1, 24, 23, 59, 0),
    "Opstartsfest med klassen",
    "Vi ønsker der er beerpong og købt ind til at der kan laves brandbil",
    ""
);

const b2 = new Booking(
    "waiting",
    "René",
    "Bossen",
    23356243,
    "rene@eaaa.test.dk",
    "22T",
    "Datamatiker",
    12,
    new Date(2024, 1, 2, 17, 0, 0),
    new Date(2024, 1, 2, 23, 59, 0),
    "Opstartsfest med klassen",
    "",
    ""
);

const b3 = new Booking(
    "confirmed",
    "Henning",
    "Andersen",
    34577333,
    "henning@eaaa.test.dk",
    "22S",
    "Datamatiker",
    25,
    new Date(2023, 11, 12, 17, 0, 0),
    new Date(2023, 11, 12, 23, 59, 0),
    "Julefrokost med klassen",
    "Vi ønsker at spise til festen",
    ""
);

const b4 = new Booking(
    "confirmed",
    "Kurt",
    "Nielsen",
    45275637,
    "kurt@eaaa.test.dk",
    "21X",
    "Multimedie Designer",
    25,
    new Date(2024, 0, 13, 17, 0, 0),
    new Date(2024, 0, 13, 23, 59, 0),
    "Julefrokost med klassen",
    "Vi ønsker at I står for maden, kan det lade sig gøre?",
    ""
);

const b5 = new Booking(
    "denied",
    "Ludvig",
    "Jensen",
    74764634,
    "ludvig@eaaa.test.dk",
    "21T",
    "Erhvervsøkonom",
    22,
    new Date(2024, 1, 14, 17, 0, 0),
    new Date(2024, 1, 14, 23, 59, 0),
    "Fødselsdagsfest for klassen",
    "Jeg holder fødselsdagsfest, og skal bruge masser shots. Der må gerne være til isbjørn også",
    "Intet specielt"
);

const b6 = new Booking(
    "blocked",
    "Administrativ blokering",
    "",
    0,
    "",
    "",
    "",
    0,
    new Date(2023, 11, 23, 0, 0, 1),
    new Date(2023, 11, 23, 23, 59, 59),
    "",
    "",
    ""
);

const b7 = new Booking(
    "waiting",
    "Lone",
    "Johnsen",
    53453632,
    "lone@eaaa.test.dk",
    "24S",
    "Datamatiker",
    21,
    new Date(2024, 1, 21, 17, 0, 0),
    new Date(2024, 1, 22, 3, 0, 0),
    "Vi er en nyopstartet klasse som ønsker at holde opstartsfest",
    "Ingen specielle ønsker",
    "Intet"
);

const b8 = new Booking(
    "waiting",
    "Hans",
    "Kurtsen",
    57378443,
    "hans@eaaa.test.dk",
    "23H",
    "Multimedie Designer",
    30,
    new Date(2024, 2, 22, 12, 0, 0),
    new Date(2024, 2, 22, 18, 0, 0),
    "Vi ønsker at skyde det nye semester igang med en fest",
    "Beerpong og brandbil",
    ""
);

const b9 = new Booking(
    "waiting",
    "Bodil",
    "Bentzen",
    62351623,
    "bodil@eaaa.test.dk",
    "21X",
    "Multi medie designer",
    25,
    new Date(2024, 2, 12, 12, 0, 0),
    new Date(2024, 2, 12, 23, 59, 0),
    "Fodbold tema fest for mine venner hjemmefra",
    "Vi bliver en masse venner hjemmefra som jeg mangler lokale til at holde temafest med. Kan vi booke hos jer?",
    "Der må gerne være pyntet op til fodboldtema"
);

//Initialize testObjects to database
addOrUpdateBooking(b);
addOrUpdateBooking(b1);
addOrUpdateBooking(b2);
addOrUpdateBooking(b3);
addOrUpdateBooking(b4);
addOrUpdateBooking(b5);
addOrUpdateBooking(b6);
addOrUpdateBooking(b7);
addOrUpdateBooking(b8);
addOrUpdateBooking(b9);

//Run script through Package.json in server folder
//month is based from 0-11

//Read booking test
describe("Read booking tests", () => {
    it("TC1: Read Booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            await readSpecificBooking("24012024JonasChristensen").then((specificBooking) => {
                assert.deepEqual(specificBooking.getLastName(), b.getLastName());
            });
        }
        catch (error) {
            console.log(error);
        }
    })
    it("TC2: Read Booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            await readSpecificBooking("24/01/2023 Jonas Christensen").then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
        }
        catch (error) {
            console.log(error);
        }
    })
    it("TC3: Read Booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            await readSpecificBooking("24012023 JonasChristensen").then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
        }
        catch (error) {
            console.log(error);
        }
    })
    it("TC4: Read Booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            await readSpecificBooking(null).then((specificBooking) => {
                assert.deepEqual(specificBooking, null);
            });
        } catch (error) {
            console.log(error);
        }
    })
});

//Create and update booking test
describe("Create and update booking test", () => {
    it("TC5: Update or add booking", async () => {
        const b = new Booking(
            null,
            "Jonas Test",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        return addOrUpdateBooking(b)
            .then(() => {
                return readSpecificBooking("24012024JonasTestChristensen");
            })
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, b);
            });
    })
    it("TC6: Update or add booking", async () => {
        const b = new Booking(
            "waiting",
            null,
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            assert.throw(async () => {
                await addOrUpdateBooking(b);
            }, Error, "firstName cannot be null");
        } catch (error) {
            console.log(error);
        }
    })
    it("TC7: Update or add booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas Test",
            null,
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            assert.throw(async () => {
                await addOrUpdateBooking(b);
            }, Error, "lastName cannot be null");
        } catch (error) {
            console.log(error);
        }
    })
    it("TC8: Update or add booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas Test",
            "Christensen",
            23764209,
            null,
            "22S",
            "Datamatiker",
            10,
            new Date(2024, 0, 24, 17, 0, 0),
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        return addOrUpdateBooking(b)
            .then(() => {
                return readSpecificBooking("24012024JonasTestChristensen");
            })
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, b);
            });
    })
    it("TC9: Update or add booking", async () => {
        const b = new Booking(
            "waiting",
            "Jonas Test",
            "Christensen",
            23764209,
            "jonas@eaaa.test.dk",
            "22S",
            "Datamatiker",
            10,
            null,
            new Date(2024, 0, 24, 23, 59, 0),
            "Fødselsdagsfest med klassen",
            "Rundborde midt i lokalet og beerpong borde i kanten",
            "Jeg fylder 31"
        );
        try {
            assert.throw(async () => {
                await addOrUpdateBooking(b);
            }, Error, "startDato cannot be null");
        } catch (error) {
            console.log(error);
        }
    })
});

//Delete booking test
describe("Delete bookings test", () => {
    it("TC10: Delete booking", async () => {
        return deleteBooking("null")
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
    })
    it("TC11: Delete booking", async () => {
        return deleteBooking("24/01/2023 JonasTest Christensen")
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
    })
    it("TC12: Delete booking", async () => {
        return deleteBooking("24012023 JonasTestChristensen")
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
    })
    it("TC13: Delete booking", async () => {
        return deleteBooking("24012024JonasTestChristensen")
            .then((specificBooking) => {
                assert.deepEqual(specificBooking, undefined);
            });
    })
})