using System;

namespace ConsoleAppDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Opgave 2: All students in db");
            Facade.GetAllStudents().ForEach(s => Console.WriteLine("\t" + s));
            Console.WriteLine();

            Console.WriteLine("Opgave 3: Specifik eksamen");
            Console.WriteLine($"\t{Facade.GetSpecifikExam(1)}\n");

            Console.WriteLine("Opgave 4: Delete eksamen");
            Console.WriteLine($"\tRows affected: {Facade.DeleteExam(8)}\n");

            Console.WriteLine("Opgave 5: Opdatere student");
            Console.WriteLine($"\tRows affected: {Facade.OpdaterStudent(new Student{StudentID = 5, Navn = "Thomas", Hold = "Sem1", MobilNr = "87722321"})}\n");

            Console.WriteLine("Opgave 6: Add Student");
            Console.WriteLine($"\tRows affected: {Facade.AddStudent(new Student { Navn = "Frederik", Hold = "Sem3", MobilNr = "12345678" })}\n");

            Console.WriteLine("Opgave 7: Get student stored procedure");
            Console.WriteLine($"\t{Facade.GetStudentStoredProcedure(1)}\n");

            Console.WriteLine("Opgave 8: Create student stored procedure");
            Console.WriteLine($"\tRows affected: {Facade.CreateStudentStoredProcedure(new Student { Navn = "Nikoline", Hold = "Sem3", MobilNr = "12345678" })}\n");

            Console.WriteLine("Opgave 9: Get all students and exams stored procedure");
            foreach (Student s in Facade.GetAllStudentsAndExamsStoredProcedure())
            {
                Console.WriteLine("\t" + s);
                s.Exams.ForEach(x => Console.WriteLine("\t\t" + x));
            }
            
            Console.ReadKey();
        }
    }
}
