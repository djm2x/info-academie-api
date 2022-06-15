using System;
using System.Collections.Generic;
namespace Models
{
    public partial class TypeCours
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string NomAr { get; set; }
        public virtual ICollection<OffreProf> OffreProfes { get; set; }

    }
}
