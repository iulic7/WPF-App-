using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace diploma.Clase
{
    [Table(Name = "Persona")]
    public class Persoane
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IdPerson { get; set; }
        [Column]
        public string IDNP { get; set; }
        [Column]
        public int idRaion { get; set; }
        [Column]
        public string Nume { get; set; }
        [Column]
        public string Prenume { get; set; }
        [Column]
        public int idInst { get; set; }
        [Column]
        public DateTime DataNast { get; set; }
        [Column]
        public bool Gen { get; set; }
        [Column]
        public int idNational { get; set; }
        [Column]
        public Double VechimeInMunc { get; set; }
        [Column]
        public bool VirstaPens { get; set; }
        [Column]
        public int idStudii { get; set; }
        [Column]
        public bool StudiiManager { get; set; }
        [Column]
        public string GradManager { get; set; }
        [Column]
        public bool ArStudiiPed { get; set; }
        [Column]
        public bool CazatCam { get; set; }
        [Column]
        public string Camin { get; set; }
        [Column]
        public string TelefonMob { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public bool ModAngaj { get; set; }
        [Column]
        public string Statutul { get; set; }
        [Column]
        public int idgradstiint { get; set; }
        [Column]
        public bool ConcediuTermenL { get; set; }
        [Column]
        public int IdCatedra { get; set; }
        [Column]
        public int IdGradDidactic { get; set; }
        [Column]
        public DateTime DataPrimiriiGrad { get; set; }
        [Column]
        public Double StagiuPedagogic { get; set; }
        [Column]
        public DateTime DataAngajarii { get; set; }
        [Column]
        public DateTime DataEliberarii { get; set; }
        [Column]
        public bool EsteProfesor { get; set; }
    }
}

