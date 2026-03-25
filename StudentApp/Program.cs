using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

class Student
{
    public int Id;
    public string name;
    public int Age;
}

class Program
{
    static List<Student> students = new List<Student>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("1: Add Student");
            Console.WriteLine("2: View Student");
            Console.WriteLine("3: Update Student");
            Console.WriteLine("4: Delete Student");
            Console.WriteLine("5: Exit");

            Console.WriteLine("Choose Option: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice)) ;
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
                    ViewStudent();
                    break;
                case 3:
                    UpdateStudent();
                    break;
                case 4:
                    DeleteStudent();
                    break;

                default: Console.WriteLine("Invalid choice");
                    break;
            }
        


        }
    }

    static void AddStudent()
    {
        Student s = new Student();

        Console.Write("Enter ID");
        s.Id = int.Parse(Console.ReadLine());

        Console.Write("Enter Name");
        s.name = string.Parse(Console.ReadLine());

        Console.Write("Enter Age: ");
        s.age = int.Parse(Console.ReadLine());

        students.Add(s);


    }

    static void ViewStudents()
    {
        Console.WriteLine("Student List");
        if(students.Count == 0)
        {
            Console.WriteLine("No Students Found");
            return;
        }
        foreach(var s in students)
        {
            Console.WriteLine($"ID: {s,id} , Name: {s.name} , Age: {s.age}");
        }
        
    }

    static void UpdateStudent()
    {
        Console.WriteLine("Update Student");

        Console.WriteLine("Enter Student ID: ");
        findID = int.Parse(Console.ReadLine());

        for(Student studentList: student){
            if(findID == studentList.Id) 
            {
                Console.WriteLine("ID Found");

                Console.WriteLine(studentList.ToString);
            }
        }

    }


}