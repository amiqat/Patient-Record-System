using PatientRecordSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Domain.Services.Communication
{
    public class RecordResponse : BaseResponse
    {
        public Record Record { get; private set; }

        private RecordResponse(bool success, string message, Record record) : base(success, message)
        {
            Record = record;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public RecordResponse(Record record) : this(true, string.Empty, record)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public RecordResponse(string message) : this(false, message, null)
        { }
    }
}