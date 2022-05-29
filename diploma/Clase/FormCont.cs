using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "FomareCont")]
    public class FormCont
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdForm { get; set; }
        [Column]
        public string Denumirea { get; set; }
        [Column]
        public DateTime Anul { get; set; }
        [Column]
        public int IdInst { get; set; }
        [Column]
        public int IdPerson { get; set; }
    }
}
