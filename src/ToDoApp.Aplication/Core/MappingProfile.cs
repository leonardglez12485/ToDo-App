using AutoMapper;
using ToDoApp.Aplication.DTOs.Person;
using ToDoApp.Aplication.DTOs.Task;
using ToDoApp.Aplication.Persons.PersonCreate;
using ToDoApp.Aplication.Persons.PersonUpdate;
using ToDoApp.Domain;

namespace ToDoApp.Aplication.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add your mapping configurations here
        CreateMap<Taske, TaskResponse>()
            .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Person));
        CreateMap<PersonCreateRequest, Person>();
        CreateMap<PersonUpdateRequest, Person>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id to prevent overwriting during update
        CreateMap<Person, PersonResponse>();
    }
}