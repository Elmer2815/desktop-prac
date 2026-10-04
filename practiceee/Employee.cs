using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiceee
{
    public class Employee
    {
        public int patient_id { get; set; }
        public string fullname { get; set; }
        public string contact_number { get; set; }
        public Status status { get; set; }
        public Image picture { get; set; }
    }

    public enum Status
    {
        Pending,
        InProgress,
        Complete
    }
}
