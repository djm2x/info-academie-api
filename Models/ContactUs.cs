using System;
using System.Collections.Generic;
namespace Models
{
public partial class ContactUs 
{public int Id { get; set; }
public string Object { get; set; }
public string Msg { get; set; }
public DateTime Date { get; set; }
public int? IdUser { get; set; }
public virtual User User { get; set; }
}
}
