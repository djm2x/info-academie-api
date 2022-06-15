using System;
using System.Collections.Generic;
namespace Models
{
public partial class Message 
{public int Id { get; set; }
public string Object { get; set; }
public string Content { get; set; }
public bool Vu { get; set; }
public DateTime Date { get; set; }
public int? IdCours { get; set; }
public string OtherUserName { get; set; }
public string OtherUserImage { get; set; }
public int? IdMe { get; set; }
public int? IdOtherUser { get; set; }
public int? IdDiscussion { get; set; }
public virtual User Me { get; set; }
public virtual User OtherUser { get; set; }
public virtual Discussion Discussion { get; set; }
}
}
