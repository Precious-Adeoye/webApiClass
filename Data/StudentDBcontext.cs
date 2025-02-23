using Microsoft.EntityFrameworkCore;
using webApiClass.DTO;
using webApiClass.model;

namespace webApiClass.Data
{
    public class StudentDBcontext : DbContext
    {
        public StudentDBcontext(DbContextOptions<StudentDBcontext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
