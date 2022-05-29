using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name ="Activit")]
    public class Activ
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int idActivit { get; set; }
        [Column]
        public string ActivitDidact { get; set; }
        [Column]
        public int IdPerson { get; set; }
    }
}
