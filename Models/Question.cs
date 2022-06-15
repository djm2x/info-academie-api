using System;
using System.Collections.Generic;
namespace Models
{
public partial class Question 
{public int Id { get; set; }
public string Value { get; set; }
public string ResponsesString { get; set; }
public string Choices { get; set; }
public bool IsMultiChoises { get; set; }
public int? Time { get; set; }
public int? IdQuiz { get; set; }
public virtual Quiz Quiz { get; set; }
        public virtual ICollection<Response> Responses { get; set; }

}
}
