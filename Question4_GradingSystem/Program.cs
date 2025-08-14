using System;
using System.Collections.Generic;
using System.IO;

namespace Question4_GradingSystem
{
    public class Student
    {
        public int Id; public string FullName; public int Score;
        public string GetGrade()
        {
            if (Score >= 80) return "A";
            if (Score >= 70) return "B";
            if (Score >= 60) return "C";
            if (Score >= 50) return "D";
            return "F";
        }
    }

    public class InvalidScoreFormatException : Exception { public InvalidScoreFormatException(string m): base(m){} }
    public class MissingFieldException : Exception { public MissingFieldException(string m): base(m){} }

    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string path)
        {
            var list = new List<Student>();
            using var r = new StreamReader(path);
            string line; int ln = 0;
            while ((line = r.ReadLine()) != null)
            {
                ln++;
                var parts = line.Split(',');
                if (parts.Length != 3) throw new MissingFieldException("Line " + ln);
                if (!int.TryParse(parts[2], out int score)) throw new InvalidScoreFormatException("Line " + ln);
                list.Add(new Student{ Id = int.Parse(parts[0]), FullName = parts[1], Score = score });
            }
            return list;
        }

        public void WriteReportToFile(List<Student> students, string path)
        {
            using var w = new StreamWriter(path);
            foreach (var s in students) w.WriteLine($"{s.FullName} (ID: {s.Id}): Score={s.Score}, Grade={s.GetGrade()}");
        }
    }

    public class Program
    {
        public static void Main()
        {
            var p = new StudentResultProcessor();
            var list = p.ReadStudentsFromFile("students_input.txt");
            p.WriteReportToFile(list, "students_report.txt");
            Console.WriteLine("Done");
        }
    }
}
