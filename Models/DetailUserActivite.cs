using System;
using System.Collections.Generic;
namespace Models
{
public partial class DetailUserActivite 
{public int Id { get; set; }
public DateTime Date { get; set; }
public int? IdUser { get; set; }
public int? IdActivite { get; set; }
public virtual User User { get; set; }
public virtual Activite Activite { get; set; }
}
}
