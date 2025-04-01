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
    }
}
