using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "PersFunctie")]
    class PersoanaFunc
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdPersF { get; set; }
        [Column]
        public int IdPerson { get; set; }
        [Column]
        public int IdFunctie { get; set; }
        [Column]
        public Double Unitati { get; set; }
        [Column]
        public int ClasaSalar { get; set; }
    }
}
