using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class Service
    {
        public string name { get; set; }
        public string description { get; set; }

        public int price { get; set; }


        public void setPrice(int p)
        {
            this.price = p;
        }


        public Service(string name = "", string desc = "", int pr = -1)
        {
            this.name = name;
            this.description = desc;
            this.price = pr;
        }

        public Service()
        {
            this.name = "";
            this.description = "";
            this.price = 0;
        }
    }
}