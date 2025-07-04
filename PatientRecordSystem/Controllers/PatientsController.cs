using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Domain.Services;
using PatientRecordSystem.Extensions;
using PatientRecordSystem.Resources;

namespace PatientRecordSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatientService _patientService;

        public PatientsController(IMapper mapper, IPatientService patienService)
        {
            _patientService = patienService;
            _mapper = mapper;
        }

        [HttpGet("GetReport/{id}")]
        public async Task<ActionResult<PatientReportResource>> GetReport(int id)
        {
            return Ok(await _patientService.GetPatientReport(id));
        }

        [HttpGet("Search/{id}")]
        public async Task<ActionResult<IEnumerable<KeyValuePairResource>>> Search(string prfix)
        {
            var list = await _patientService.Search(prfix, 50);
            var data = _mapper.Map<IEnumerable<Patient>, IEnumerable<KeyValuePairResource>>(list);
            return Ok(data);
        }

        //GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientResource>>> GetPatients([FromQuery] QueryStringParameters queryParameters)
        {
            var columnsMap = new Dictionary<string, Expression<Func<PatientResource, object>>>()
            {
                ["patientName"] = v => v.PatientName,
                ["id"] = v => v.Id,
                ["dateOfBirth"] = v => v.DateOfBirth,
                ["metaDataCount"] = v => v.MetaDataCount,
                ["lastEntry"] = v => v.LastEntry
            };
            var list = await _patientService.ListAsync(queryParameters, columnsMap);
            var metadata = new
            {
                list.TotalCount,
                list.PageSize,
                list.CurrentPage,
                list.TotalPages,
                list.HasNext,
                list.HasPrevious
            };
            Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);
            return Ok(list);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavePatientResource>> GetPatient(int id)
        {
            var result = await _patientService.GetById(id);
            if (!result.Success)
            {
                return NotFound(new ErrorResource(result.Message));
            }
            var patientResource = _mapper.Map<Patient, SavePatientResource>(result.Patient);
            return Ok(patientResource);
        }

        [HttpPost]
        public async Task<ActionResult> PostPatient(SavePatientResource savePatient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = _mapper.Map<SavePatientResource, Patient>(savePatient);
            if (await _patientService.IsExist(patient.OffcialId, null))
            {
                ModelState.AddModelError("OffcialId", "OffcialId already exists");
                return BadRequest(ModelState);
            }
            var result = await _patientService.SaveAsync(patient);

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
        public async Task<IActionResult> PutPatient(int id, SavePatientResource savePatient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _patientService.IsExist(savePatient.OffcialId.GetValueOrDefault(), id))
            {
                ModelState.AddModelError("OffcialId", "OffcialId already exists");
                return BadRequest(ModelState);
            }
            var result = await _patientService.UpdateAsync(id, savePatient);
            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }
            return Ok();
        }
    }
}