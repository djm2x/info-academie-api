using System;
using System.Collections.Generic;
namespace Models
{
public partial class Student 
{public int Id { get; set; }
public string? Ecole { get; set; }
public int? Niveau { get; set; }
public int? Branche { get; set; }
public string? NomParent { get; set; }
public string? PrenomParent { get; set; }
public string? Tel1Parent { get; set; }
public string? Tel2Parent { get; set; }
public int? IdUser { get; set; }
public int? IdActivite { get; set; }
public virtual User User { get; set; }
public virtual Activite Activite { get; set; }
}
}
