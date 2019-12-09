using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Resources
{
    public class SaveRecordResource
    {
        public int Id { set; get; }

        [Required]
        public int PatientId { set; get; }

        public float Amount { get; set; } = 0.0f;

        [Required]
        public string DiseaseName { set; get; }

        public DateTime TimeOfEntry { set; get; }
        public string Discription { set; get; }
    }
}