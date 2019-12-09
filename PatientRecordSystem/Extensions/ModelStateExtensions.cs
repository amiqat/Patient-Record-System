using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRecordSystem.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary) => dictionary.SelectMany(m => m.Value.Errors)
                          .Select(m => m.ErrorMessage)
                          .ToList();
    }
}