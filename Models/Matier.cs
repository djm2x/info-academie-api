using System;
using System.Collections.Generic;
namespace Models
{
public partial class Matier 
{public int Id { get; set; }
public string? Name { get; set; }
public string? NameAr { get; set; }
public virtual ICollection<Cours> Courses { get; set; }
}
}
