using System;
using System.Collections.Generic;
namespace Models
{
    public partial class NiveauScolaire
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? NomAr { get; set; }
        public int? IdCycle { get; set; }
        public double CoursLigneGroupe { get; set; }
        public double CoursLigneIndividuel { get; set; }
        public double CoursDomicileGroupe { get; set; }
        public double CoursDomicileIndividuel { get; set; }
        public virtual ICollection<Branche> Branches { get; set; }
        public virtual ICollection<Cours> Courses { get; set; }

    }
}
