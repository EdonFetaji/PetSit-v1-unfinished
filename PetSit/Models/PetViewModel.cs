using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSit.Models
{
    public class PetViewModel
    {
        public int PetID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public string MedicalInfo { get; set; }
        public string Bio { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }  // For file upload
        public string ProfilePicturePath { get; set; }
    }

}