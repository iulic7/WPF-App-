using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Localitatea")]
   public class Localit
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdLocal { get; set; }
        [Column]
        public string Localitate { get; set; }
    }
}
