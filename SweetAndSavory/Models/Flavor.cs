using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetAndSavory.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    [Required(ErrorMessage = "This field cannot be empty. Please try again.")]
    public string FlavorName { get; set; }
    public int TreatId { get; set; }
  
    public List <FlavorTreat> JoinEntities { get; set; }
    public Account User { get; set; }   
    
  }
}