using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class PatientReportResource
    {
        public int Id { set; get; }
        public string PatientName { set; get; }
        public int Age { set; get; }
        public Double? Avg { set; get; }
        public Double? AvgW { set; get; }
        public string MostVisitMounth { set; get; }

        public RecordResource record { set; get; }
    }
}