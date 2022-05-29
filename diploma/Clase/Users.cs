using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "User")]
    public class Users
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdUser { get; set; }
        [Column]
        public string numeUtilizator { get; set; }
        [Column]
        public string Parola { get; set; }
        [Column]
        public DateTime dataInreg { get; set; }
        [Column]
        public DateTime dataMod { get; set; }

    }
}
