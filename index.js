const { Console } = require("console");
const { application } = require("express");
const express = require("express");
const fs = require("fs");
var app = express();
const PORT = 8080;
var userData = null;
//const eventoData = fs.readFile(`${__dirname}/jsonData/eventos.json`);

// middlewares
application.use(
    
);
app.use(express.json());
// end middlewares


app.listen(
    PORT,
    () => console.log(`Running on http://localhost:${PORT}`)
);

app.get("/users", (req, res) => {
    // implement
    res.send(userData());
});

/* app.get("/users/:nombre", (req, res) => {
    
}); */

app.post("/user/:id", (req, res) => {
    const {id} = req.params;
    const {usuario} = req.body;
    const {pass} = req.body;

    if (!pass | !usuario) {
        res.status(418).send({
            message: "need user and pass"
        });
    }

    res.send({
        usuario: `usuario ${usuario} con id ${id} y pass ${pass}`
    });
});