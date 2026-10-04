using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiceee
{
    public class Patient
    {
        public int patient_id { get; set; }
        public string fullname { get; set; } 
        public string contact_number { get; set; }
        public string status { get; set; } = "active";
    }
}
