using AutoMapper;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO.Application;
using LoanApplicationApp.API.Repositories;
using LoanApplicationApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;

namespace LoanApplicationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ApplicationController(IApplicationRepository applicationRepository
            ,IMapper mapper
            ,IConfiguration configuration)
        {
            this.applicationRepository = applicationRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        //Update application
        //PUT: https:localhost:portnumber/api/application/{id}
        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateApplicationRequestDTO updateApplicationRequestDTO)
        {
            //Map DTO to DM
            var applicationDM = mapper.Map<Application>(updateApplicationRequestDTO);
            //Check if applicaaation exists
            applicationDM = await applicationRepository.UpdateAsync(id, applicationDM);
            if(applicationDM == null)
            {
                return NotFound();
            }
            //Map DM to DTO
            var applicationDTO = mapper.Map<ApplicationDTO>(applicationDM);
            //Return applicationDTO to client
            return Ok(applicationDTO);
        }
        //Get application by id
        //Get: https://localgost:portnumber/api/application/{id}
        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            //Application DM
            var applicationDM = await applicationRepository.GetByIdAsync(id);
            if(applicationDM == null)
            {
                return NotFound();
            }
            //Map Application DM to DTO
            var applicationDTO = mapper.Map<ApplicationDTO>(applicationDM);
            //Return DTO back to client
            return Ok(applicationDTO);
        }
        //Get all applications
        //Get: https://loacalhost:portnumber/api/application
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? ApplicantId, [FromQuery] long? LoanProcesorNo)
        {
            var applicationDM = await applicationRepository.GetAllAsync(ApplicantId, LoanProcesorNo);
            if (!applicationDM.Any()) return NotFound();
            var applicationDTO = mapper.Map<IEnumerable<ApplicationDTO>>(applicationDM);
            return Ok(applicationDTO);
        }
        //POST method to add new application
        //POST: https://localhost:portnumber/api/application
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddApplicationDTO addApplicationDTO,[FromQuery] string applicantId)
        {
            //Map or convert DTO to DM
            var applicationDM = mapper.Map<Application>(addApplicationDTO);
            await applicationRepository.CreatAsync(applicationDM,applicantId);
            //Map DM back to DTO
            var applicationDTO = mapper.Map<ApplicationDTO>(applicationDM);
            return CreatedAtAction(nameof(Create),new { applicationDTO.ApplicantId}, applicationDTO);
        }
    }
}
