using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Stagiu")]
    public class Stagiu
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public DateTime inceput { get; set; }
        [Column]
        public DateTime sfirsit { get; set; }
        [Column]
        public int idPersoana { get; set; }

    }
}
