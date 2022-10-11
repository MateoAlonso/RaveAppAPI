const express = require("express");
var app = express();
const PORT = 8080;


// routes
const usersRoute = require('./routes/users');
const eventosRoute = require('./routes/eventos');


// middleware
app.use(express.json());
app.use('/users', usersRoute);
app.use('/eventos', eventosRoute);

// Puerto host
app.listen(
    PORT,
    () => console.log(`Running on http://localhost:${PORT}`)
);

// API server status
app.get('/ping', (req, res) => {
    res.status(200).send({ message: 'OK' });
});