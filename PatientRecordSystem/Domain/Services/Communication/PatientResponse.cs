using PatientRecordSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Services.Communication
{
    public class PatientResponse : BaseResponse
    {
        public Patient Patient { get; private set; }

        private PatientResponse(bool success, string message, Patient patient) : base(success, message)
        {
            Patient = patient;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public PatientResponse(Patient patient) : this(true, string.Empty, patient)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public PatientResponse(string message) : this(false, message, null)
        { }
    }
}