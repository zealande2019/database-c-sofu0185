using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppDatabase
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Navn { get; set; }
        public string Hold { get; set; }
        public string MobilNr { get; set; }
        public List<Exam> Exams { get; set; }

        public Student()
        {
            Exams = new List<Exam>();
        }

        public override string ToString()
        {
            return $"[StudentID: {StudentID}; Navn: {Navn}; Hold: {Hold}; MobilNr: {MobilNr};]";
        }
    }
}
