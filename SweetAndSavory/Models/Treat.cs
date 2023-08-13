using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SweetAndSavory.Models
{
    public class Treat
    {
        public int TreatId { get; set; }
     

        [Required(ErrorMessage = "This field cannot be empty. Please try again.")]
        public string TreatName { get; set; }
        

        public List<FlavorTreat> JoinEntities { get; set; }
    }
}