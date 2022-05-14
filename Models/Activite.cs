using System;
using System.Collections.Generic;
namespace Models
{
public partial class Activite 
{public int Id { get; set; }
public string? Nom { get; set; }
public string? NomAr { get; set; }
public string? ImageUrl { get; set; }
public int? IdTypeActivite { get; set; }
public virtual TypeActivite TypeActivite { get; set; }
public virtual ICollection<DetailUserActivite> DetailUserActivites { get; set; }
public virtual ICollection<Student> Students { get; set; }
}
}
