class Evento{
    constructor(id, nombre, genero, desc, isLgbt, isAfter, isValidado, isCancelado, isRecPaga, fechaInicio, fechaFin, fechaFinVenta, totalRec){
        this.id = id;
        this.nombre = nombre;
        this.genero = genero;
        this.desc = desc;
        this.isLgbt = isLgbt;
        this.isAfter = isAfter;
        this.isValidado = isValidado;
        this.isCancelado = isCancelado
        this.isRecPaga = isRecPaga;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
        this.fechaFinVenta = fechaFinVenta;
        this.totalRec = totalRec;
    };

    get Data(){
        return [{
            'id': this.id,
            'nombre': this.nombre,
            'genero': this.genero,
            'desc': this.desc,
            'isLgbt': this.isLgbt,
            'isAfter': this.isAfter,
            'isValidado': this.isValidado,
            'isCancelado': this.isCancelado,
            'isRecPaga': this.isRecPaga,
            'fechaInicio': this.fechaInicio,
            'fechaFin': this.fechaFin,
            'fechaFinVenta': this.fechaFinVenta,
            'totalRec': this.totalRec
        }];
    }
}
module.exports = Evento;