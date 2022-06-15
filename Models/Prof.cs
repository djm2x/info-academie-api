using System;
using System.Collections.Generic;
namespace Models
{
public partial class Prof 
{public int Id { get; set; }
public string Lien { get; set; }
public string Description { get; set; }
public string Experience { get; set; }
public string Approche { get; set; }
public string Intro { get; set; }
public string VideoUrl { get; set; }
public string CvUrl { get; set; }
public int? Note { get; set; }
public int? PrixHrWeb { get; set; }
public int? PrixHrHome { get; set; }
public int? PrixHrWebGroupe { get; set; }
public int? PrixHrHomeGroupe { get; set; }
public string IdsTypeActivites { get; set; }
public string IdsActivites { get; set; }
public string IdsTypeCours { get; set; }
public string IdsLieuCours { get; set; }
public string IdsNiveauScolaires { get; set; }
public int? IdUser { get; set; }
public virtual User User { get; set; }
}
}
