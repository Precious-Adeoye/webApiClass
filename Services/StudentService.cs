using System.Linq;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using webApiClass.Data;
using webApiClass.DTO;
using webApiClass.Iservice;
using webApiClass.model;

namespace webApiClass.Services
{
    public class StudentService : IStudent  
    {
        private readonly StudentDBcontext studentDBContext;
        private readonly IMapper mapper;

        public StudentService(StudentDBcontext studentDBContext, IMapper mapper)
        {
            this.studentDBContext = studentDBContext;
            this.mapper = mapper;
        }

        public void CreateStudent(StudentDTO studentDto)
        {
            var student = mapper.Map<Student>(studentDto);
            
            if (studentDto ==null)
            {
                throw new Exception("Student input cannot be empty");
            }
            var students = studentDBContext.Students
                .FirstOrDefault(s => s.FirstName == studentDto.FirstName && s.LastName == studentDto.LastName && s.Stack == studentDto.Stack);
            if (students != null)
            {
                throw new Exception("Student already exist");

            }

            studentDBContext.Students.Add(student);
             studentDBContext.SaveChanges();

        }

        public StudentDTO GetStudentDTO (Student student)
        {
            return mapper.Map<StudentDTO>(student);
        }

        public ICollection<Student> GetAllStudents()
        {
            return studentDBContext.Students.ToList();
        }

        public Student GetStudentByName(string name)
        {
           foreach (var student in studentDBContext.Students)
            {
                if (student.FirstName.Equals (name, StringComparison.OrdinalIgnoreCase) || student.LastName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return student;
                }
            }
            return null;
        }

        //public Student GetStudentByName(string name)
        //{
        //    return studentDBContext.Students
        //        .Where(s => s.FirstName == name || s.LastName == name).FirstOrDefault();
        //}


        public string DeleteStudentById(int id)
        {
            var student = studentDBContext.Students.Find(id);
            if (student == null)
            {
                return "Id not found";
            }
            else if (student != null)
            {
                studentDBContext.Students.Remove(student);
                studentDBContext.SaveChanges();
                return "student successfully deleted";
            }

            return "";
        }



        public Student GetStudentById(int id)
        {
            return studentDBContext.Students.Find(id);
        }

        public void UpdateStudentId(int id, Student update)
        {
           
            var student = studentDBContext.Students.Find(id);
            if (student != null)
            {
                studentDBContext.Entry(student).CurrentValues.SetValues(update);
                studentDBContext.SaveChanges();
            }
           
        }
       
    }
}
