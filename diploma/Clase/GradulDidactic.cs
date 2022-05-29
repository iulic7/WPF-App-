using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "GradDidactic")]
    class GradulDidactic
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdGradDidactic { get; set; }
        [Column]
        public string Gradul { get; set; }
    }
}
