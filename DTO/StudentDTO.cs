using System.ComponentModel.DataAnnotations;
using AutoMapper;
using webApiClass.model;

namespace webApiClass.DTO
{
    public class StudentDTO
    {
        [Required]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Stack { get; set; }

     } 

    public class MappingProfile : Profile
    {
        public MappingProfile ()
        {
            CreateMap<Student, StudentDTO>();
        }
    }
}
