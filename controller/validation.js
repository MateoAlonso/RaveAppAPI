const Joi = require('joi');

// Schema validacion de usuario
const userSchema = Joi.object({
    'nombre': Joi.string().min(3).max(10).required(),
    'pass': Joi.string().pattern(new RegExp('^[a-zA-Z0-9]{3,30}$')).required(),
    'tel': Joi.string(),
    'dni': Joi.string(),
    'isOrganizador': Joi.bool(),
    'cbu': Joi.string(),
});

// Schema validacion de evento
const eventoSchema = Joi.object({
    'nombre': Joi.string().min(3).max(30).required(),
    'genero': Joi.string().required(),
    'desc': Joi.string().max(250),
    'isLgbt': Joi.boolean(),
    'isAfter': Joi.bool(),
    'isValidado': Joi.bool(),
    'isCancelado': Joi.bool(),
    'isRecPaga': Joi.bool(),
    'fechaInicio': Joi.date().iso().min('now').required(),
    'fechaFin': Joi.date().iso().min(Joi.ref('fechaInicio')),
    'fechaFinVenta': Joi.date().iso().min('now').required(),
    'totalRec': Joi.string()
});

module.exports = {eventoSchema, userSchema};