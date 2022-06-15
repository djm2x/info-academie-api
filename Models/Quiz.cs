using System;
using System.Collections.Generic;
namespace Models
{
public partial class Quiz 
{public int Id { get; set; }
public string Title { get; set; }
public string Description { get; set; }
public bool EnableTime { get; set; }
public DateTime Date { get; set; }
public bool IsActive { get; set; }
public int? IdContext { get; set; }
public virtual Cours Context { get; set; }
public virtual ICollection<Question> Questions { get; set; }
}
}
