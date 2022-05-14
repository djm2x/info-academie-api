using System;
using System.Collections.Generic;
namespace Models
{
public partial class Response 
{public int Id { get; set; }
public string? TrueResponse { get; set; }
public string? UserResponse { get; set; }
public DateTime Date { get; set; }
public int? Note { get; set; }
public int? IdQuestion { get; set; }
public virtual Question Question { get; set; }
public int? IdUser { get; set; }
public virtual User User { get; set; }
}
}
