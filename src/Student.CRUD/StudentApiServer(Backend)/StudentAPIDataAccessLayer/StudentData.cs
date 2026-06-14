using Microsoft.Data.SqlClient;
using System.Data;

namespace StudentAPIDataAccessLayer
{
    public class StudentDTO
    {
        public StudentDTO(int id, string name, int age, int grade)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;

            this.Grade = grade;


        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

    }
    public class StudentData
    {
        static string _connectionString = "Server=localhost;Database=StudentsDB;User Id=sa12;Password=sa12345;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";

        public static List<StudentDTO> GetAllStudents()
        {
            var StudentList = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentList.Add(new StudentDTO
                             (
                                 reader.GetInt32(reader.GetOrdinal("Id")),
                                 reader.GetString(reader.GetOrdinal("Name")),
                                 reader.GetInt32(reader.GetOrdinal("Age")),
                                 reader.GetInt32(reader.GetOrdinal("Grade"))
                             ));

                        }
                    }
                }
                return StudentList;
            }
        }




        public static List<StudentDTO> GetPassesStudents()
        {
            var StudentList = new List<StudentAPIDataAccessLayer.StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students WHERE Grade>60";
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StudentList.Add(new StudentAPIDataAccessLayer.StudentDTO
                             (
                                 reader.GetInt32(reader.GetOrdinal("Id")),
                                 reader.GetString(reader.GetOrdinal("Name")),
                                 reader.GetInt32(reader.GetOrdinal("Age")),
                                 reader.GetInt32(reader.GetOrdinal("Grade"))
                             ));
                        }
                    }
                }
                return StudentList;
            }

        }

        public static int AddStudent(StudentDTO student)
        {
            int id = -1;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Name, Age, Grade) OUTPUT INSERTED.Id VALUES (@Name, @Age, @Grade)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);
                    id = (int)command.ExecuteScalar();
                }
            }
            return id;
        }


        public static float GetAverageGrade()
        {
            float average = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT AVG(Grade) FROM Students";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    average = (float)command.ExecuteScalar();
                }
            }
            return average;
        }


        public static StudentDTO GetStudentByID(int id)
        {
            StudentDTO student = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new StudentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                        }
                    }
                }
                return student;
            }
        }


        public static bool UpdateStudent(StudentDTO StudentDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SP_UpdateStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentId", StudentDTO.Id);
                command.Parameters.AddWithValue("@Name", StudentDTO.Name);
                command.Parameters.AddWithValue("@Age", StudentDTO.Age);
                command.Parameters.AddWithValue("@Grade", StudentDTO.Grade);

                connection.Open();
                command.ExecuteNonQuery();
                return true;

            }
        }

        public static bool DeleteStudent(int studentId)
        {

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SP_DeleteStudent", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StudentId", studentId);

                connection.Open();

                int rowsAffected = (int)command.ExecuteScalar();
                return (rowsAffected == 1);


            }



        }
    }
}



