using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class DashboardViewModel
    {
        public ApplicationUser user;



        public List<Pet> pets;

        public List<Booking> bookings;
    }
}