using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class RecordResource
    {
        public int Id { set; get; }
        public string PatientName { get; set; }
        public string DiseaseName { set; get; }
        public DateTime TimeOfEntry { set; get; }
    }
}