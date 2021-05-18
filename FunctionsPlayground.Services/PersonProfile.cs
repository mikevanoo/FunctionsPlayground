using System;
using AutoMapper;
using FunctionsPlayground.Models;

namespace FunctionsPlayground.Services
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonRequest, Person>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
        }
    }
}