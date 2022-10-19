const express = require("express");
const router = express.Router();
var db = require("../repository/db");
const validation = require("../repository/validation");

// middleware
router.use('/post', (req, res, next) => {
    const result = validation.eventoSchema.validate(req.body, {abortEarly: false});
    if (!result.error) {
        next();
    } else {
    res.status(400).send(result.error.message);
    }
});

// get eventos
router.get('/',(req, res) => {
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

// get eventos por genero
router.get("/genero", (req, res) => {
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

// get eventos por isLgbt
router.get("/lgbt", (req, res) => {
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

// get eventos por isAfter
router.get("/after", (req, res) => {
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

// get eventos por isValidado
router.get("/validado", (req, res) => {
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

// get eventos por isCancelado
router.get("/cancelado", (req, res) => {
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

// get eventos por isRecPaga
router.get("/recPagada", (req, res) => {
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

// post evento
router.post("/post", (req, res)=>{
    var{nombre} = req.body;
    var{genero} = req.body;
    var{desc} = req.body;
    var{isLgbt} = req.body;
    var{isAfter} = req.body;
    var{isValidado} = req.body;
    var{isCancelado} = req.body;
    var{isRecPaga} = req.body;
    var{fechaInicio} = req.body;
    var{fechaFin} = req.body;
    var{fechaFinVenta} = req.body;
    var{totalRec} = req.body;
    try {
        var newEvento = db.postEvento(nombre, genero, desc, isLgbt, isAfter, isValidado, isCancelado, isRecPaga, fechaInicio, fechaFin, fechaFinVenta, totalRec);
        res.send({
            newEvento
        });
    } catch (error) {
        res.status(418).send({
            message: `${error}`
        });
    }
});

module.exports = router;