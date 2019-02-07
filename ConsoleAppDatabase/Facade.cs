using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleAppDatabase
{
    public static class Facade
    {
        private const string username = "";
        private const string password = "";
        private static readonly string connectionString = $"Server=tcp:progtest.database.windows.net,1433;Initial Catalog=student;Persist Security Info=False;User ID={username};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static List<Student> GetAllStudents()
        {
            string sqlQuery = "SELECT * FROM Student";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Student> students = new List<Student>();
                    while (reader.Read())
                    {
                        students.Add(new Student()
                        {
                            StudentID = reader.GetInt32(0),
                            Navn = reader.GetString(1),
                            Hold = reader.GetString(2),
                            MobilNr = reader.GetString(3)
                        });
                    }

                    return students;
                }
            }
        }

        public static Exam GetSpecifikExam(int examID)
        {
            string sqlQuery = "SELECT * FROM Exam WHERE Exam_Id = @examID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                command.Parameters.AddWithValue("@examID", examID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read())
                    {
                        return new Exam()
                        {
                            ExamID = reader.GetInt32(0),
                            Navn = reader.GetString(1),
                            Karakter = reader.GetString(2)
                        };
                    }
                    else
                        return null;
                }
            }
        }

        public static int DeleteExam(int examID)
        {
            string sqlQuery = "DELETE FROM Exam WHERE Exam_Id = @examID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                command.Parameters.AddWithValue("@examID", examID);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static int OpdaterStudent(Student modifiedStudent)
        {
            string sqlQuery = "UPDATE Student SET Navn = @navn, Hold = @hold, Mobil_Nr = @mobilnr WHERE Student_Id = @studentID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                command.Parameters.AddWithValue("@studentID", modifiedStudent.StudentID);
                command.Parameters.AddWithValue("@navn", modifiedStudent.Navn);
                command.Parameters.AddWithValue("@hold", modifiedStudent.Hold);
                command.Parameters.AddWithValue("@mobilnr", modifiedStudent.MobilNr);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static int AddStudent(Student newStudent)
        {
            string sqlQuery = "INSERT INTO Student (Navn, Hold, Mobil_Nr) VALUES (@navn, @hold, @mobilnr)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sqlQuery, connection)) {
                command.Parameters.AddWithValue("@studentID", newStudent.StudentID);
                command.Parameters.AddWithValue("@navn", newStudent.Navn);
                command.Parameters.AddWithValue("@hold", newStudent.Hold);
                command.Parameters.AddWithValue("@mobilnr", newStudent.MobilNr);

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static Student GetStudentStoredProcedure(int studentID)
        {
            string storedProcedureName = "SelectSpecifikStudent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Studentid", SqlDbType.Int).Value = studentID;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = reader.GetInt32(0),
                            Navn = reader.GetString(1),
                            Hold = reader.GetString(2),
                            MobilNr = reader.GetString(3)
                        };
                    }
                    else
                        return null;
                }
            }
        }

        public static int CreateStudentStoredProcedure(Student newStudent)
        {
            string storedProcedureName = "CreateStudent";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@navn", SqlDbType.NVarChar).Value = newStudent.Navn;
                command.Parameters.Add("@hold", SqlDbType.NVarChar).Value = newStudent.Hold;
                command.Parameters.Add("@mobilnr", SqlDbType.NVarChar).Value = newStudent.MobilNr;

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public static List<Student> GetAllStudentsAndExamsStoredProcedure()
        {
            string storedProcedureName = "SelectAllStudentAndExams";

            List<Student> students = new List<Student>();
            List<Exam> exams = new List<Exam>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = reader.GetInt32(0),
                            Navn = reader.GetString(1),
                            Hold = reader.GetString(2),
                            MobilNr = reader.GetString(3)
                        });
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        exams.Add(new Exam
                        {
                            ExamID = reader.GetInt32(0),
                            Navn = reader.GetString(1),
                            Karakter = reader.GetString(2),
                            StudentID = reader.GetInt32(3)
                        });
                    }
                    
                }
            }

            students.ForEach(s => s.Exams.AddRange(exams.Where(x => x.StudentID == s.StudentID)));
            return students;
        }
    }
}
