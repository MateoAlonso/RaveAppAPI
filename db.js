const fs = require("fs");
const { get } = require("http");
const _ = require("lodash");

function getUsers() {
    return fs.readFileSync(`${__dirname}/jsonData/users.json`);
}

function getUsersByIsOrg(option) {
    return _.filter(getUsers(),{'is_organizador': option});
}

function getUserByNombre(nombre) {
    return _.filter(getUsers(), {'nombre': option});    
}

function getEventos() {
    return fs.readFileSync(`${__dirname}/json/eventos.json`);
}

function getEventoByNombre(nombre) {
    return _.filter(getEventos(), {'nombre': nombre});
}

function getEventoByGenero(genero) {
    return _.filter(getEventos(), {'genero': genero})
}

function getEventoByIsLgbt(option) {
    return _.filter(getEventos(), {'is_lgbt': option})
}

function getEventoByIsAfter(option) {
    return _.filter(getEventos(), {'is_after': option})
}

function getEventoByIsValidado(option) {
    return _.filter(getEventos(), {'is_validado': option})
}

function getEventoByIsCancelado(option) {
    return _.filter(getEventos(), {'is_cancelado': option})
}

function getEventoByRecPagada(option) {
    return _.filter(getEventos(), {'is_recaudacion_paga': option})
}