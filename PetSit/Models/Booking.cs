using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int PetSitterID { get; set; }
        public int PetID { get; set; }
        public string UserID { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public Service SelectedService { get; set; }
        public string comment { get; set; }
        public string status { get; set; }

        public virtual PetSitter PetSitter { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Pet Pet { get; set; }

    }
}