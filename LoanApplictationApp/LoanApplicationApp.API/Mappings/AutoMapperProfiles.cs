using AutoMapper;
using LoanApplicationApp.API.Models.Domain;
using LoanApplicationApp.API.Models.DTO.Application;
using LoanApplicationApp.API.Models.DTO.Document;
using LoanApplicationApp.API.Models.DTO.LoanProcessor;

namespace LoanApplicationApp.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Mappings for Application
            //CreateMap<AddApplicationDTO, Application>()
            //    .ForMember(dest => dest.IdCopy, opt => opt.Ignore())
            //    .ForMember(dest => dest.BankStatement, opt => opt.Ignore())
            //    .ForMember(dest => dest.PaySlip, opt => opt.Ignore())
            //    .ReverseMap();
            CreateMap<AddApplicationDTO, Application>().ReverseMap();
            CreateMap<Application, ApplicationDTO>().ReverseMap();
            CreateMap<UpdateApplicationRequestDTO, Application>().ReverseMap();

            //Mapping for LoanProcessor
     
            CreateMap<AddLoanProcessorDTO, LoanProcessor>().ReverseMap();
            CreateMap<LoanProcessor, LoanProcessorDTO>().ReverseMap();
            CreateMap<UpdateLoanProcessorRequestDTO, LoanProcessor>().ReverseMap();

            //Mappings for Document
            CreateMap<DocumentUploadRequestDTO, Document>();
            CreateMap<Document, DocumentDTO>().ReverseMap();

        }
    }
}
