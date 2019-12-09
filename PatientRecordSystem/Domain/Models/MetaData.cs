using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Models
{
    public class MetaData
    {
        public int PatientId { set; get; }

        public Patient Patient { get; set; }

        public string Key { set; get; }

        public string Value { set; get; }
    }
}