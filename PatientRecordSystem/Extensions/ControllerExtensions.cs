using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatientRecordSystem.Helpers;

namespace PatientRecordSystem.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Adds pagination metadata to the response headers
        /// </summary>
        /// <typeparam name="T">The type of items in the paged list</typeparam>
        /// <param name="controller">The controller instance</param>
        /// <param name="pagedList">The paged list containing pagination metadata</param>
        public static void AddPaginationHeader<T>(this ControllerBase controller, PagedList<T> pagedList)
        {
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };
            controller.Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);
        }
    }
}