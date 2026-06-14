using StudentAPIServer.Model;

namespace StudentAPIServer.DataSimlation
{
    public class StudentDataSimulation
    {

        public static readonly List<Student> StudentsList = new List<Student>
        {
            new Student { Id = 1, Name = "John Doe", Age = 20, Grade = 90 },
            new Student { Id = 2, Name = "Jane Smith", Age = 22, Grade = 85 },
            new Student { Id = 3, Name = "Sam Brown", Age = 19, Grade = 88 },
            new Student { Id = 4, Name = "Lisa White", Age = 21, Grade = 92 },
            new Student { Id = 5, Name = "Tom Green", Age = 23, Grade = 80 }
        };
    }
}
