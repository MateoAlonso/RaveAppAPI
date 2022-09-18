const fs = require("fs");
const { get } = require("http");
const _ = require("lodash");

function getUsers() {
    var data = fs.readFileSync(`${__dirname}/jsonData/users.json`);
    return JSON.parse(data);
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
    var data = fs.readFileSync(`${__dirname}/jsonData/eventos.json`);
    return JSON.parse(data);
}

function getEventoByNombre(nombre) {
    var data = _.filter(getEventos(), {'nombre': nombre});
    return data
}

function getEventoByGenero(genero) {
    var data = _.filter(getEventos(), {'genero': genero})
    return data
}

function getEventoByIsLgbt(option) {
    var data = _.filter(getEventos(), {'is_lgbt': option})
    return data
}

function getEventoByIsAfter(option) {
    var data = _.filter(getEventos(), {'is_after': option})
    return data
}

function getEventoByIsValidado(option) {
    var data = _.filter(getEventos(), {'is_validado': option})
    return data
}

function getEventoByIsCancelado(option) {
    var data = _.filter(getEventos(), {'is_cancelado': option})
    return data
}

function getEventoByIsRecPagada(option) {
    var data = _.filter(getEventos(), {'is_recaudacion_paga': option})
    return data
}

module.exports = {getUsers, getEventos, getEventoByIsLgbt, getEventoByGenero, getEventoByIsCancelado, getEventoByIsAfter, getUserByNombre, getUsersByIsOrg, getEventoByNombre, getEventoByIsValidado, getEventoByIsRecPagada}