import http from "node:http";

const port = 8000

const users = [
    {
        id: 1,
        image: "https://share.cederdorff.com/images/race.jpg",
        mail: "race@kea.dk",
        name: "Rasmus Cederdorff",
        title: "Senior Lecturer"
    },
    {
        id: 2,
        image: "https://share.cederdorff.com/images/petl.jpg",
        mail: "petl@kea.dk",
        name: "Peter Lind",
        title: "Senior Lecturer"
    },
    {
        id: 3,
        name: "Edith Terte",
        mail: "edan@kea.dk",
        title: "Lecturer",
        image: "https://media.licdn.com/dms/image/C4E03AQE6nx7oUPqo_g/profile-displayphoto-shrink_800_800/0/1643707886591?e=1697673600&v=beta&t=Qp4GcxVlJfsZi4t-if6YJ6O1u7bH2oLwWgVxB-X5Nt4"
    }
];

const app = http.createServer((req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.write("If you change these lines in app.js and hit save\n")
    res.write("The server will automatically update and restart\n")
    res.end(JSON.stringify(users))
});

app.listen(port, () => {
    console.log(`The server is running on port ${port}`)
})