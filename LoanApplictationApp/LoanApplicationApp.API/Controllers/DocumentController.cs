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
            , IMapper mapper)
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
                var documentDM = await documentRepository.UploadAsync(documentUploadRequestDTO);
                return Ok(documentDM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //GEt: https:localhost:portnumber/api/document
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? userId)
        {
            var documentDM = await documentRepository.GetAllAsync(userId);
            if (!documentDM.Any())
            {
                return NotFound("No document found.");
            }
            var documentDTO = mapper.Map<IEnumerable<DocumentDTO>>(documentDM);
            return Ok(documentDTO);
        }
        //Delete: https://localhost:portnumber/api/documents/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var documentDM = await documentRepository.DeleteAsync(id);
            if (documentDM == null)
            {
                return NotFound("Document not found.");
            }
            return Ok(mapper.Map<DocumentDTO>(documentDM));
        }
        //PUT: https://localhost:portnumber/api/documents/{id}
        [HttpPut]
        [Route("Replace/{id:Guid}")]
        public async Task<IActionResult> Replace([FromRoute] Guid id, [FromForm] DocumentUploadRequestDTO documentUploadRequestDTO)
        {
            try
            {
                var documentDM = await documentRepository.ReplaceAsync(documentUploadRequestDTO, id);
                return Ok(documentDM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
