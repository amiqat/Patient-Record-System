using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Models
{
    public class MetaDataStat
    {
        public int AveragePerPatient { set; get; }
        public int MaxPerPatient { set; get; }
    }

    public class TopMetaData
    {
        public string Key { set; get; }
        public int count { set; get; }
    }
}