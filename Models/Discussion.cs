using System;
using System.Collections.Generic;
namespace Models
{
public partial class Discussion 
{public int Id { get; set; }
public int? UnReaded { get; set; }
public DateTime Date { get; set; }
public int? IdMe { get; set; }
public int? IdOtherUser { get; set; }
public virtual User Me { get; set; }
public virtual User OtherUser { get; set; }
public virtual ICollection<Message> Messages { get; set; }
}
}
