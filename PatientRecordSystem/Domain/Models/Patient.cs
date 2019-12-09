using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Models
{
    public class Patient
    {
        public Patient()
        {
            MetaData = new Collection<MetaData>();
        }

        public int Id { set; get; }
        public int OffcialId { set; get; }
        public string PatientName { set; get; }

        public DateTime? DateOfBirth { set; get; }

        public string Email { set; get; }
        public virtual ICollection<MetaData> MetaData { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}