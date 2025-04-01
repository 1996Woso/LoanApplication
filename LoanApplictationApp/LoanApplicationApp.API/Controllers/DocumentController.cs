using AutoMapper;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO.Document;
using LoanApplicationApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplicationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IMapper mapper;

        public DocumentController(IDocumentRepository documentRepository
            ,IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
        }
        //Post: https:localhost:portnumber/api/document/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadRequestDTO documentUploadRequestDTO)
        {
            try
            {
                var documentDM = await documentRepository.Upload(documentUploadRequestDTO);
                return Ok(documentDM);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
           
        }
    }
}
