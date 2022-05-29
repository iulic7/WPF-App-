using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Studii")]
    public class Stud
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdStudii { get; set; }
        [Column]
        public string Studiile { get; set; }
    }
}
