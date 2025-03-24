using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class GeneroEvento
    {
        [ColumnName("cdgenero")]
        public int CdGenero { get; set; }
        [ColumnName("dsgenero")]
        public string DsGenero { get; set; }
    }
}
