using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Resources;

namespace PatientRecordSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMetaDataRepository _metaDataRepository;

        public MetaDataController(IMapper mapper, IMetaDataRepository metaDataRepository)
        {
            _metaDataRepository = metaDataRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<MetaDataReport>> Get()
        {
            return Ok(await _metaDataRepository.getMetaStat());
        }
    }
}