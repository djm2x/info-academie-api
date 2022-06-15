using System;
using System.Collections.Generic;
namespace Models
{
public partial class Video 
{public int Id { get; set; }
public string Title { get; set; }
public int? Order { get; set; }
public string Description { get; set; }
public DateTime Date { get; set; }
public string UrlVideo { get; set; }
}
}
