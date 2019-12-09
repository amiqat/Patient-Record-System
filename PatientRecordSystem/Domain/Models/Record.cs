using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Models
{
    public class Record
    {
        public int Id { set; get; }
        public int PatientId { set; get; }
        public Patient Patient { get; set; }

        public float Amount { get; set; }
        public string DiseaseName { set; get; }
        public DateTime TimeOfEntry { set; get; }
        public string Discription { set; get; }
    }
}