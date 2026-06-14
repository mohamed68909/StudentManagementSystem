using StudentAPIDataAccessLayer;

namespace StudentAPIBusinesslayer
{
    public class Student
    {
        public enum enumMode{
            Add,
            Update,
            Delete
    }
        public enumMode Mode = enumMode.Add;
        public StudentDTO SDTO
        {
            get { return (new StudentDTO(Id = this.Id, Name = this.Name, Age = this.Age, Grade = this.Grade));  }
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }
        public int Grade { get; set; }
        public Student(StudentDTO SDTO, enumMode mode=enumMode.Add)
        {
            this.Id = SDTO.Id;
            this.Name = SDTO.Name;
            this.Age = SDTO.Age;
            this.Grade = SDTO.Grade;
            mode = mode;
        }
        public static  List<StudentDTO> GetAllStudents()
        {

            return StudentData.GetAllStudents();
        }
        public static List<StudentDTO> GetPassesStudents()
        {
            return StudentData.GetPassesStudents();
        }
        public static float GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
       private bool _AddNewStudent()
        {

            this.Id =  StudentData.AddStudent(SDTO);
            return (this.Id!=-1);
        }
       public static Student Find(int id)
        {
            StudentDTO SDTO = StudentData.GetStudentByID(id);
            if (SDTO != null)
            {
                return new Student(SDTO, enumMode.Update);
            }
            else
            {
             return null;
            }
        }
        private bool _UpdateStudent()
        {
            return StudentData.UpdateStudent(SDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enumMode.Add:
                    {
                        if (_AddNewStudent())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enumMode.Update:

                  return  _UpdateStudent();

            }

            return false;
        }
        public bool DeleteStudent()
        {
            return StudentData.DeleteStudent(this.Id);
        }
    }
}
