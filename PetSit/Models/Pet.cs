using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class Pet
    {
        public int PetID { get; set; }
        public string OwnerID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int age { get; set; }
        public string medicalInfo { get; set; }
        public string bio { get; set; }
        public string ProfilePicturePath { get; set; } 

        public virtual ApplicationUser Owner { get; set; }
    }
}
