using AutoMapper;
using e2e_DAL.Entities;
using e2e_services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e2e_services.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
            .ForMember(dest => dest.LeaveRecords, opt => opt.MapFrom(src => src.LeaveRecords));
            CreateMap<ContractDto, Contract>();
            CreateMap<LeaveRecordsDto, LeaveRecords>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateContractDto, Contract>();
            CreateMap<Contract, ContractDto>();
            CreateMap<LeaveRecords, LeaveRecordsDto>();
        }
    }
}
