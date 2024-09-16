using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class PetSitter
    {
        public int PetSitterID { get; set; }
        public string UserID { get; set; }
        public string offeredServices { get; set; }
        public string availability { get; set; }
        public string location { get; set; }
        public int rating { get; set; }

        public ApplicationUser User { get; set; }

    }
}