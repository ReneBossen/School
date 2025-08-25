import express from "express"
import todos from "./data.js"

const server = express()
const port = 8000

server.get('/', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send ("Welcome to your first express server!")
});

server.get('/users', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send("You sent a get request!")
})

server.get('/todos', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send(todos)
})

server.post('/users', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send("You sent a post request!")
})

server.post('/todos/:id', (req, res) => {
    console.log(req.body)
    res.send(req.body)
})

server.put('/users', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send("You sent a put request!")
})

server.delete('/users', (req, res) => {
    res.statusCode = 200
    res.setHeader("Content-Type", "text/plain")

    res.send("You sent a delete request!")
})

server.listen(port, () => {
    console.log(`Server has started on port ${port}`)
    console.log(todos)
});