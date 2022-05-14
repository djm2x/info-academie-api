using System;
using System.Collections.Generic;
namespace Models
{
public partial class OffreProf 
{public int Id { get; set; }
public string? Interval { get; set; }
public int? Value { get; set; }
public int? IdTypeCours { get; set; }
public virtual TypeCours TypeCours { get; set; }
}
}
