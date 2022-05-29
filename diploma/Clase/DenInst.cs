using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "DenumInst")]
    public class DenInst
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdInst { get; set; }

        [Column]
        public string DenumInst { get; set; }
    }
}
