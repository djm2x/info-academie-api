using System;
using System.Collections.Generic;
namespace Models
{
public partial class EventProf 
{public int Id { get; set; }
public string? Title { get; set; }
public DateTime Start { get; set; }
public DateTime End { get; set; }
public string? Color { get; set; }
public bool Draggable { get; set; }
public string? Resizable { get; set; }
public int? Month { get; set; }
public int? Year { get; set; }
public int? IdUser { get; set; }
public virtual User User { get; set; }
}
}
