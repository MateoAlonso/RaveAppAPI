const express = require("express");
const fs = require("fs");
var app = express();
const PORT = 8080;
var db = require("./db");
require("./User");

// middleware
app.use(express.json());
// end middlewares

// Puerto host
app.listen(
    PORT,
    () => console.log(`Running on http://localhost:${PORT}`)
);

// GET request users
app.get("/users", (req, res) => {
    var data = db.getUsers();
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/users/nombre", (req, res) => {
    var {nombre} = req.body;
    console.log(nombre);
    var data = db.getUserByNombre(nombre);
    res.send({
        data
    });
});

app.get("/users/org", (req, res) => {
    var {org} = req.body;
    var data = db.getUsersByIsOrg(org);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

//GET requests eventos
app.get('/eventos',(req, res) => {
    //implement
    var data = db.getEventos();
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/genero", (req, res) => {
    var {genero} = req.body;
    var data = db.getEventoByGenero(genero);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/lgbt", (req, res) => {
    var {lgbt} = req.body;
    var data = db.getEventoByIsLgbt(lgbt);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/after", (req, res) => {
    var {after} = req.body;
    var data = db.getEventoByIsAfter(after);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/validado", (req, res) => {
    var {validado} = req.body;
    var data = db.getEventoByIsValidado(validado);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/cancelado", (req, res) => {
    var {cancelado} = req.body;
    var data = db.getEventoByIsCancelado(cancelado);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

app.get("/eventos/recPagada", (req, res) => {
    var {recPagada} = req.body;
    var data = db.getEventoByIsRecPagada(recPagada);
    if (!data) {
        res.status(418).send({
            message: "ERROR FETCHING DATA"
        });
    }
    res.send({
        data
    });
});

//POST request Users
app.post("/users/postUser", (req, res)=>{
    var{nombre} = req.body;
    var{tel} = req.body;
    var{dni} = req.body;
    var{isOrganizador} = req.body;
    var{cbu} = req.body;
    var{pass} = req.body;
    console.log(`BEFORE TRY INDEX = nombre: ${nombre} - tel: ${tel} - dni: ${dni} - isOrg: ${isOrganizador} - cbu: ${cbu} - pass: ${pass}`)
    console.log("enter try");
    var newUser = db.postUsuario(nombre, tel, dni, cbu, isOrganizador, pass);
    console.log(`INSIDE TRY INDEX = newUser: ${newUser}`);
    res.send({
        newUser
    });
    console.log("enter catch");
    res.status(418).send({
        message: `Failed to append data ${error}`
    });
});

/* app.post("/user/:id", (req, res) => {
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
}); */