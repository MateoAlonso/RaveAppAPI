const Joi = require('joi');

const userSchema = Joi.object({
    'nombre': Joi.string().min(3).max(10).required(),
    'pass': Joi.string().pattern(new RegExp('^[a-zA-Z0-9]{3,30}$')).required(),
    'tel': Joi.allow(),
    'dni': Joi.allow(),
    'isOrganizador': Joi.allow(),
    'cbu': Joi.allow(),
});

const eventoSchema = Joi.object({

});

module.exports = {eventoSchema, userSchema};