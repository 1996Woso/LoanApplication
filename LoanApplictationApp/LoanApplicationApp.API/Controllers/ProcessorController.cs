using AutoMapper;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO.LoanProcessor;
using LoanApplicationApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILoanProcessorRepository loanProcessorRepository;

        public ProcessorController(IMapper mapper
            ,ILoanProcessorRepository loanProcessorRepository)
        {
            this.mapper = mapper;
            this.loanProcessorRepository = loanProcessorRepository;
        }
        //POST: https://localhost:portnumber/api/processor
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddLoanProcessorDTO addLoanProcessorDTO)
        {
            var loanProcessorDM = mapper.Map<LoanProcessor>(addLoanProcessorDTO);
            await loanProcessorRepository.CreatAsync(loanProcessorDM);
            var loanProcessorDTO = mapper.Map<LoanProcessorDTO>(loanProcessorDM);
            return CreatedAtAction(nameof(Create), new {loanProcessorDTO.LoanProcessorNo}, loanProcessorDTO);

        }
        //Update loanpreocesor
        //PUT: https://localhost:portnumber/api/processor/{userId}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateLoanProcessorRequestDTO updateLoanProcessorRequestDTO)
        {
            var loanProcessorDM = mapper.Map<LoanProcessor>(updateLoanProcessorRequestDTO);
            
            loanProcessorDM = await loanProcessorRepository.UpdateAsync(id,loanProcessorDM);
            if(loanProcessorDM == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<LoanProcessor>(loanProcessorDM));
        }
    }
}
