using System;
using System.Collections.Generic;
namespace Models
{
public partial class User 
{public int Id { get; set; }
public string? Nom { get; set; }
public string? Prenom { get; set; }
public string? Tel1 { get; set; }
public string? Tel2 { get; set; }
public string? Email { get; set; }
public string? Password { get; set; }
public bool IsActive { get; set; }
public DateTime Date { get; set; }
public string? Adresse { get; set; }
public string? ImageUrl { get; set; }
public string? Cin { get; set; }
public string? Role { get; set; }
public int? IdVille { get; set; }
public virtual Ville Ville { get; set; }
public virtual ICollection<DetailUserActivite> DetailUserActivites { get; set; }
public virtual ICollection<ContactUs> ContactUs { get; set; }
public virtual ICollection<Discussion> Discussions { get; set; }
public virtual ICollection<Discussion> OtherUserDiscussions { get; set; }
public virtual ICollection<Message> Messages { get; set; }
public virtual ICollection<Message> OtherUserMessages { get; set; }
public virtual ICollection<EventProf> EventProfs { get; set; }
public virtual ICollection<Prof> Profs { get; set; }
public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

}
}
