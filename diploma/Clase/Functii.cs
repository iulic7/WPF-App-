using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Functia")]
    public class Functii
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdFunctie { get; set; }
        [Column]
        public string DenFunctie { get; set; }
    }
}
