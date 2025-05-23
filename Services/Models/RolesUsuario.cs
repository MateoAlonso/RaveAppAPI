using RaveAppAPI.Services.Helpers;

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
