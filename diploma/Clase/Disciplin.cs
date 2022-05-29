using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;


namespace diploma.Clase
{
    [Table(Name = "Discipline")]
    public class Disciplin
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
       public int IdDiscip { get; set; }
        [Column]
        public string Denumirea { get; set; }
    }
}
