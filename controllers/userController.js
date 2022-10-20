const express = require("express");
const router = express.Router();
var db = require("../repository/dbRepo");
const userService = require("../services/userService");

// middleware
router.use('/post', (req, res, next) => {
    const result = userService.validateUserData(req.body);
    if (!result.error) {
        next();
    } else {
    res.status(400).send(result.error.message);
    }
});

// get users
router.get("/", (req, res) => {
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

// get users por nombre
router.get("/nombre", (req, res) => {
    var {nombre} = req.body;
    console.log(nombre);
    var data = db.getUserByNombre(nombre);
    res.send({
        data
    });
});

// get users por isOrganizador
router.get("/org", (req, res) => {
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

// register user
router.post("/post", (req, res)=>{

    var user = req.body;
    try {
        var newUser = userService.registerUser(user);
        res.send({
            newUser
        })
    } catch (error) {
        res.status(418).send({
            message: `${error}`
        });
    }
    
    
    /*var{nombre} = req.body;
    var{tel} = req.body;
    var{dni} = req.body;
    var{isOrganizador} = req.body;
    var{cbu} = req.body;
    var{pass} = req.body;
    try {
        var newUser = db.postUsuario(nombre, tel, dni, cbu, isOrganizador, pass);
        res.send({
            newUser
        });
    } catch (error) {
        res.status(418).send({
            message: `${error}`
        });
    } */
});

module.exports = router;