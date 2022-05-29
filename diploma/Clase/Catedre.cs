using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Catedra")]
    class Catedre
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdCatedra { get; set; }
        [Column]
        public string DenumireCatedra { get; set; }
    }
}
