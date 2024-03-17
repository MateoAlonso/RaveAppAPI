﻿using ErrorOr;
using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Localidad
    {
        [ColumnName("dslocalidad")]
        public string Nombre { get;}
        public Provincia Provincia { get;}

        public Localidad(string nombre, string provincia)
        {
            Nombre = nombre;
            Provincia = new Provincia(provincia);
        }

        //public static ErrorOr<Localidad> Create(string dsNombre, string dsProvincia)
        //{
        //    List<Error> errors = new(); añadir validaciones si fuera necesario
        //    Provincia provincia = new Provincia(dsProvincia);
        //    return new Localidad(dsNombre, provincia);
        //}
    }
}