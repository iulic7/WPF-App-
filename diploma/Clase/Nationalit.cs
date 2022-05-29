using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "National")]
    public class Nationalit
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdNational { get; set; }
        [Column]
        public string Nationalitatea { get; set; }
    }
}
