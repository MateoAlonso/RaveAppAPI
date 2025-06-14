using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Tipo
    {
        [ColumnName("cdtipo")]
        public int CdTipo { get; set; }
        [ColumnName("dstipo")]
        public string DsTipo { get; set; }
    }
}
