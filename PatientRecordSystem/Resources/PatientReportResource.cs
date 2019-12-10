using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICollection<PatientDiseasesResource> PatientDiseases { get; set; }
        public RecordResource record { set; get; }

        public PatientReportResource()
        {
            PatientDiseases = new Collection<PatientDiseasesResource>();
        }
    }

    public class PatientDiseasesResource
    {
        public string PatientName { set; get; }

        public ICollection<string> DiseaseList { get; set; }

        public PatientDiseasesResource()
        {
            DiseaseList = new Collection<string>();
        }
    }
}