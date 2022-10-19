const fs = require("fs");
const _ = require("lodash");
const userData = require("../jsonData/users.json");
const eventoData = require("../jsonData/eventos.json");
const User = require('../model/User');
const Evento = require('../model/Evento');

// get requests
function getUsers() {
    return userData;
}

function getUsersByIsOrg(option) {
    var data = _.filter(getUsers(),{'is_organizador': option});
    return data;
}

function getUserByNombre(nombre) {
    var data = _.filter(getUsers(), {'nombre': nombre});
    return data;
}

function getEventos() {
    return eventoData;
}

function getEventoByNombre(nombre) {
    var data = _.filter(getEventos(), {'nombre': nombre});
    return data;
}

function getEventoByGenero(genero) {
    var data = _.filter(getEventos(), {'genero': genero});
    return data;
}

function getEventoByIsLgbt(option) {
    var data = _.filter(getEventos(), {'is_lgbt': option});
    return data;
}

function getEventoByIsAfter(option) {
    var data = _.filter(getEventos(), {'is_after': option});
    return data;
}

function getEventoByIsValidado(option) {
    var data = _.filter(getEventos(), {'is_validado': option});
    return data;
}

function getEventoByIsCancelado(option) {
    var data = _.filter(getEventos(), {'is_cancelado': option});
    return data;
}

function getEventoByIsRecPagada(option) {
    var data = _.filter(getEventos(), {'is_recaudacion_paga': option});
    return data;
}


// post requests
function postUsuario(user) {
    var id = userData.length;
    let newUser = new User(user.nombre, user.tel, user.dni, user.cbu, user.isOrganizador, user.pass, id);
    userData.push(newUser);
    fs.writeFileSync("./jsonData/users.json",JSON.stringify(userData));
    if (id == userData.length) {
        throw error = "Failed to append data";
    }
    return newUser;
}

function postEvento(nombre, genero, desc, isLgbt, isAfter, isValidado, isCancelado, isRecPaga, fechaInicio, fechaFin, fechaFinVenta, totalRec) {
    var id = eventoData.length;
    let evento = new Evento(id, nombre, genero, desc, isLgbt, isAfter, isValidado, isCancelado, isRecPaga, fechaInicio, fechaFin, fechaFinVenta, totalRec);
    eventoData.push(evento);
    fs.writeFileSync("./jsonData/eventos.json",JSON.stringify(eventoData));
    if (id == eventoData.length) {
        throw error = "Failed to append data";
    }
    return evento;
}





// Exports
module.exports = {getUsers, getEventos, getEventoByIsLgbt, getEventoByGenero, getEventoByIsCancelado, getEventoByIsAfter, getUserByNombre, getUsersByIsOrg, getEventoByNombre, getEventoByIsValidado, getEventoByIsRecPagada, postUsuario, postEvento};