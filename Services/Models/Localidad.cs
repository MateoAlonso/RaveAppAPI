﻿using RaveAppAPI.Services.Helpers;

namespace RaveAppAPI.Services.Models
{
    public class Localidad
    {
        [ColumnName("dslocalidad")]
        public string Nombre { get; }
        public Provincia Provincia { get; }

        public Localidad(string nombre, int provincia)
        {
            Nombre = nombre;
            Provincia = new Provincia(provincia, nombre);
        }

        //public static ErrorOr<Localidad> Create(string dsNombre, string dsProvincia)
        //{
        //    List<Error> errors = new(); añadir validaciones si fuera necesario
        //    Provincia provincia = new Provincia(dsProvincia);
        //    return new Localidad(dsNombre, provincia);
        //}
    }
}
