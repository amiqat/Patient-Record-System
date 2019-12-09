using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class SavePatientResource
    {
        public int Id { set; get; }

        [Required]
        public int? OffcialId { set; get; }

        [Required]
        public string PatientName { set; get; }

        public DateTime? DateOfBirth { set; get; }

        [EmailAddress]
        public string Email { set; get; }

        public ICollection<MetaDataResource> MetaData { get; set; }

        public SavePatientResource()
        {
            MetaData = new Collection<MetaDataResource>();
        }
    }
}