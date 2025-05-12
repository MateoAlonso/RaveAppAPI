using RaveAppAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Models
{
    public class Socials
    {
        [ColumnName("idsocial")]
        public string IdSocial { get; set; }
        [ColumnName("mdinstagram")]
        public string MdInstagram { get; set; }
        [ColumnName("mdspotify")]
        public string MdSpotify { get; set; }
        [ColumnName("mdsoundcloud")]
        public string MdSoundcloud { get; set; }
    }
}
