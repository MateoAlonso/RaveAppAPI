using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Estado
    {
        [ColumnName("cdestado")]
        public int CdEstado { get; set; }
        [ColumnName("dsestado")]
        public string DsEstado { get; set; }
    }
}
