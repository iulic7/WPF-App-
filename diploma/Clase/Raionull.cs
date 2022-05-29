using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Raionul")]
    public class Raionull
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdRaion { get; set; }
        [Column]
        public string RaionDenum { get; set; }
        [Column]
        public int idLocal { get; set; }
    }
}
