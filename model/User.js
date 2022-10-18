class User{
    constructor(nombre, tel, dni, cbu, isOrganizador, pass, id){
        this.nombre = nombre;
        this.tel = tel;
        this.dni = dni;
        this.cbu = cbu;
        this.isOrganizador = isOrganizador;
        this.pass = pass;
        this.id = id;
    }

    get Data(){
        return [{
            'id': this.id,
            'nombre': this.nombre,
            'tel': this.tel,
            'dni': this.dni,
            'cbu': this.cbu,
            'isOrganizador': this.isOrganizador,
            'pass': this.pass
        }];
    }

}

module.exports = User;