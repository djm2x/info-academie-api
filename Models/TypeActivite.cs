using System;
using System.Collections.Generic;
namespace Models
{
public partial class TypeActivite 
{public int Id { get; set; }
public string Nom { get; set; }
public string NomAr { get; set; }
public string ImageUrl { get; set; }
public bool Active { get; set; }
public virtual ICollection<Activite> Activites { get; set; }
}
}
