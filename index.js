const express = require("express");
const app = express();
const PORT = 8080;

// routes
const usersRoute = require('./controllers/userController');
const eventosRoute = require('./controllers/eventoController');

// middleware
app.use(express.json());
app.use('/users', usersRoute);
app.use('/eventos', eventosRoute);

// puerto host
app.listen(
    PORT,
    () => console.log(`Running on http://localhost:${PORT}`)
);

// API server status
app.get('/ping', (req, res) => {
    res.status(200).send({ message: 'OK' });
});