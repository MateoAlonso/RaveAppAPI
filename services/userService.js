const validationRepo = require('../repository/validationRepo');
const dbRepo = require('../repository/dbRepo');

function registerUser(user) {
    
}

function validateUserData(user) {
    return validationRepo.userSchema.validate(user, {abortEarly: false});
}

module.exports = {registerUser, validateUserData};