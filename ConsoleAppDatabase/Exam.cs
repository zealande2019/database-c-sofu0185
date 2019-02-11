using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppDatabase
{
    public class Exam
    {
        public int ExamID { get; set; }
        public string Navn { get; set; }
        public string Karakter { get; set; }
        public int StudentID { get; set; }

        public override string ToString()
        {
            return $"[ExamID: {ExamID}; Navn: {Navn}; Karakter: {Karakter}; StudentID: {StudentID};]";
        }
    }
}
