const fs = require("fs");
const { get } = require("http");
const _ = require("lodash");
const userData = require("./jsonData/users.json");
const eventoData = require("./jsonData/eventos.json");
require("./User");
const { error } = require("console");

// GET requests
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


// POST requests
function postUsuario(nombre, tel, dni, cbu, isOrganizador, pass) {
    console.log("ENTER postUsuario");
    var id = userData.length;
    console.log("CREATE new User");
    User = new User(nombre, tel, dni, cbu, isOrganizador, pass, id);
    console.log(`INSIDE postUsuario() = id: ${id} - User: ${User}`);
    userData.push(User);
    console.log(`INSIDE postUsuario() = new lenght (should be > previous id): ${userData.length}`);
    if (id == userData.length) {
        throw error = "Failed to append data";
    }
    return User;
}







// Exports
module.exports = {getUsers, getEventos, getEventoByIsLgbt, getEventoByGenero, getEventoByIsCancelado, getEventoByIsAfter, getUserByNombre, getUsersByIsOrg, getEventoByNombre, getEventoByIsValidado, getEventoByIsRecPagada, postUsuario};