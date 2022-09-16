const fs = require("fs");
const { get } = require("http");
const _ = require("lodash");

function getUsers() {
    return fs.readFileSync(`${__dirname}/jsonData/users.json`);
}

function getUsersByIsOrg(option) {
    return _.filter(getUsers(),{'nombre': option});
}

function getEventos() {
    
}

function getUserByNombre(nombre) {
    
}

function getEventoByNombre(nombre) {
    
}

function getEventoByGenero(genero) {
    
}

function getEventoByIsLgbt(option) {
    
}

function getEventoByIsAfter(option) {
    
}

function getEventoByIsValidado(option) {
    
}

function getEventoByIsCancelado(option) {
    
}

function getEventoByRecPagada(option) {
    
}