using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "GrStiint")]
   public class GrStiinte
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Idgrstiint { get; set; }
        [Column]
        public string Garad { get; set; }
    }
}
