﻿using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class RolesUsuario
    {
        [ColumnName("cdrol")]
        public int CdRol { get; set; }
        [ColumnName("dsrol")]
        public string DsRol { get; set; }
    }
}
