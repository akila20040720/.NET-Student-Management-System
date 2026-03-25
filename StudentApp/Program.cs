using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Data.SQLite;

class Student
{
    public int Id;
    public string name;
    public int age;

  public override string ToString()
    {
        return $"ID: {Id} , Name: {name} , Age: {age}";
    }
}

class Program
{
    static List<Student> students = new List<Student>();

    static void Main()
    {
        string connectionString = "Data Source=students.db;version=3;";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string createTable = @"CREATE TABLE IF NOT EXISTS Students (
               Id INTEGER PRIMARY KEY,
                Name Text,
                Age INTEGER 
            )";
            var cmd = new SQLiteCommand(createTable, connection);
            cmd.ExecuteNonQuery();


        }

        while (true)
        {
            Console.WriteLine("1: Add Student");
            Console.WriteLine("2: View Student");
            Console.WriteLine("3: Update Student");
            Console.WriteLine("4: Delete Student");
            Console.WriteLine("5: Exit");

            Console.WriteLine("Choose Option: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice)) 
            {
                Console.WriteLine("Invalid Input");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;
                case 2:
                    ViewStudents();
                    break;
                case 3:
                    UpdateStudent();
                    break;
                case 4:
                    DeleteStudent();
                    break;
                 case 5:
                    return;




                default: Console.WriteLine("Invalid choice");
                    break;
            }
        


        }
    }

    static void AddStudent()
    {
        Student s = new Student();

        string cs = "Data Source=students.db;version=3;";
        var conn = new SQLiteConnection(cs);
        conn.Open();

        Console.Write("Enter ID");
        int Id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());

        students.Add(s);

        string query = "INSERT INTO Students (Id, name , Age) VALUES (@Id, @name, @age)";
        var cmd = new SQLiteCommand(query, conn);

        cmd.Parameters.AddWithValue("@Id", Id);
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@age", age);

        cmd.ExecuteNonQuery();

    }

    static void ViewStudents()
    {
        string cs = "Data Source=students.db;version=3;";
        var conn = new SQLiteConnection(cs);
        conn.Open();

        string query = "SELECT * FROM Students";
        var cmd = new SQLiteCommand(query, conn);

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Age: {reader["Age"]}");
        }

        Console.WriteLine("Student List");
        if(students.Count == 0)
        {
            Console.WriteLine("No Students Found");
            return;
        }
        foreach(var s in students)
        {
            //Console.WriteLine($"ID: {s.Id} , Name: {s.name} , Age: {s.age}");
            Console.WriteLine(s.ToString());
        }
        
    }

    static void UpdateStudent()
    {
        string cs = "Data Source=students.db;version=3;";
        var conn = new SQLiteConnection(cs);
        conn.Open();

        string query = "SELECT name=@name, age=@age WHERE Id=@Id";

        var cmd = new SQLiteCommand(query,conn);

       


        Console.WriteLine("Update Student");

        Console.WriteLine("Enter Student ID: ");
        int findID = int.Parse(Console.ReadLine());

        foreach(Student studentList in students){
            if(findID == studentList.Id) 
            {
                Console.WriteLine("ID Found");

                Console.WriteLine(studentList.ToString());

                Console.WriteLine("Enter New Name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter New Age: ");
                int age = int.Parse(Console.ReadLine());

                Console.WriteLine("Student Updated");

                cmd.Parameters.AddWithValue("@id", findID);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);

                int rows = cmd.ExecuteNonQuery();

            }
        }

    }

    static void DeleteStudent()
    {
        string cs = "Data Source=students.db;version=3";
        var conn = new SQLiteConnection(cs);
        conn.Open();
        
        Console.WriteLine("Delete Student");

        Console.Write("Enter Student ID: ");
        int findID = int.Parse(Console.ReadLine());
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].Id == findID)
            {
                Console.WriteLine("Student Found:");
                Console.WriteLine(students[i]);

                students.RemoveAt(i);

                Console.WriteLine("Student Deleted");
                return;
            }
        }

        string query = "DELETE FROM Students WHERE Id=@findID";

        var cmd = new SQLiteCommand(query, conn);

        cmd.Parameters.AddWithValue("@findID", findID);
        int rows = cmd.ExecuteNonQuery();


        Console.WriteLine("Student Not Found");
    }


}