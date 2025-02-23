using webApiClass.DTO;
using webApiClass.model;

namespace webApiClass.Iservice
{
    public interface IStudent 
    {
        // Return type and the functionName add parameters if required 

        void CreateStudent(Student student);

        Student GetStudentById(int id);

        Student GetStudentByName(string name);

        string DeleteStudentById(int id);

        void UpdateStudentId(int id, Student student);

        ICollection<Student> GetAllStudents();


    }
}
