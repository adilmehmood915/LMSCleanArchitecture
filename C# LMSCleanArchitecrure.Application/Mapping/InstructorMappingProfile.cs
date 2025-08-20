using AutoMapper;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using LMSCleanArchitecture.Core.Entities;

namespace LMSCleanArchitecrure.Application.Mapping
{
    public class InstructorMappingProfile : Profile
    {
        public InstructorMappingProfile()
        {
            CreateMap<CreateInstructorDTO, Instructor>();
            CreateMap<UpdateInstructorDTO, Instructor>();

            // FIX: map from entity to DTOs used by queries
            CreateMap<Instructor, GetByIdInstructorDTO>();
            CreateMap<Instructor, GetAllInstructorDTO>();

            CreateMap<Instructor, InstructorResponseDTO>();
        }
    }
}