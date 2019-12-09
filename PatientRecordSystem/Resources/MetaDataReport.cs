using PatientRecordSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class MetaDataReport
    {
        public int AveragePerPatient { set; get; }
        public int MaxPerPatient { set; get; }

        public ICollection<TopMetaData> TopMetaData { get; set; }

        public MetaDataReport()
        {
            TopMetaData = new Collection<TopMetaData>();
        }
    }
}