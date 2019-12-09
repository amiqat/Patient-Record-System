using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Domain.Services;
using PatientRecordSystem.Extensions;
using PatientRecordSystem.Persistence.Contexts;
using PatientRecordSystem.Resources;

namespace PatientRecordSystem.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecordService _recordService;

        public RecordsController(IMapper mapper, IRecordService recordService)
        {
            _mapper = mapper;
            _recordService = recordService;
        }

        //GET: api/Records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordResource>>> GetRecords([FromQuery] QueryStringParameters queryParameters)
        {
            var columnsMap = new Dictionary<string, Expression<Func<Record, object>>>()
            {
                ["patientName"] = v => v.Patient.PatientName,
                ["id"] = v => v.Id,
                ["timeOfEntry"] = v => v.TimeOfEntry,
                ["diseaseName"] = v => v.DiseaseName
            };
            var list = await _recordService.ListAsync(queryParameters, columnsMap);
            var recordResource = _mapper.Map<IEnumerable<Record>, IEnumerable<RecordResource>>(list);
            var metadata = new
            {
                list.TotalCount,
                list.PageSize,
                list.CurrentPage,
                list.TotalPages,
                list.HasNext,
                list.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(recordResource);
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordResource>> GetRecord(int id)
        {
            var result = await _recordService.GetById(id);
            if (!result.Success)
            {
                return NotFound(new ErrorResource(result.Message));
            }
            var recordResource = _mapper.Map<Record, SaveRecordResource>(result.Record);
            return Ok(recordResource);
        }

        // POST: api/Records
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<IActionResult> PostRecord(SaveRecordResource saveRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var record = _mapper.Map<SaveRecordResource, Record>(saveRecord);

            var result = await _recordService.SaveAsync(record);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok();
        }

        // PUT: api/Records/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecord(int id, SaveRecordResource saveRecord)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var record = _mapper.Map<SaveRecordResource, Record>(saveRecord);
            var result = await _recordService.UpdateAsync(id, record);
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            return Ok();
        }
    }
}