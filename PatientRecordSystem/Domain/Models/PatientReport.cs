using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Models
{
    public class PatientReport
    {
        public int Id { set; get; }
        public string PatientName { set; get; }
        public int Age { set; get; }
        public Double? Avg { set; get; }
        public Double? AvgW { set; get; }
        public string MostVisitMounth { set; get; }
    }
}