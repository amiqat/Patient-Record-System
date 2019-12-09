using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class PatientResource
    {
        public int Id { set; get; }
        public string PatientName { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public DateTime? LastEntry { set; get; }
        public int MetaDataCount { set; get; }
    }
}