console.log("Hello world")

const persons = [
    {
        Name: "Ren�",
        Age: 25

    },
    {
        Name: "Sven",
        Age: 52
    }
];

function printPersons() {
    for (let i = 0; i < persons.length; i++) {
        console.log("People in this room: " + persons[i].Name)
    }
}

printPersons();