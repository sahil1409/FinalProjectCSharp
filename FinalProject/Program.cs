using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Project
{
    class Program
    {

        public static void Main(string[] args)
        {
            string filePath = @"C:\Users\sahil.shaikh\Documents\C#\Data.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<Teacher> people = new List<Teacher>();

            FetchRecords fetch = new FetchRecords();
            NewEntry enter = new NewEntry();
            UpdateRecords edit = new UpdateRecords();
            int opt = 0;

            Teacher check = new Teacher();
            foreach (var line in lines)
            {
                string[] entries = line.Split(',');

                Teacher newTeacher = new Teacher();

                newTeacher.ID = int.Parse(entries[0]);
                newTeacher.Name = entries[1];
                newTeacher.ClassSection = entries[2];

                people.Add(newTeacher);
            }

            while (opt != 4)
            {
                Console.WriteLine("\n1. Fetch Records \n2. Add New Entry \n3. Update Entry \n4. Exit\n");
                Console.Write("Enter Choice: ");
                opt = Convert.ToInt32(Console.ReadLine());
                switch (opt)
                {
                    case 1:
                        fetch.fetchEntries(filePath, people);
                        break;

                    case 2:
                        enter.createEntries(filePath, people);
                        break;

                    case 3:
                        edit.updateEntries(filePath, people);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("\nEnter Valid Option\n");
                        break;
                }
            }
        }
    }

    class FetchRecords
    {
        public void fetchEntries(string filePath, List<Teacher> people)
        {
            Console.WriteLine("\nID \tName \tClass And Section\n");
            foreach (var teacher in people)
            {
                Console.WriteLine($"{teacher.ID} \t{teacher.Name} \t{teacher.ClassSection}");
            }
        }
    }

    class NewEntry
    {
        public void createEntries(string filePath, List<Teacher> people)
        {
            string choice = "Y";
            while (choice == "Y")
            {
                Console.Write("\nEnter ID: ");
                int newID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Name: ");
                string newName = Console.ReadLine();
                Console.Write("Enter Class and Section: ");
                string newClassSection = Console.ReadLine();

                people.Add(new Teacher { ID = newID, Name = newName, ClassSection = newClassSection });

                List<string> output = new List<string>();

                foreach (var teacher in people)
                {
                    output.Add($"{teacher.ID},{teacher.Name},{teacher.ClassSection}");
                }
                File.WriteAllLines(filePath, output);
                Console.WriteLine();
                Console.Write("Do you want to add more entries? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
            }
        }
    }

    public class UpdateRecords
    {
        public void updateEntries(string filePath, List<Teacher> people)
        {
            Console.Write("\nEnter ID to be Updated: ");
            int editID = Convert.ToInt32(Console.ReadLine());

            Teacher editTeacher = people.Where(i => i.ID == editID).FirstOrDefault();
            
            if (editTeacher == null)
            {
                Console.WriteLine("\nPlease input the correct ID");
            }

            else
            {
                Console.Write("\nEnter Updated Name: ");
                string editName = Console.ReadLine();

                Console.Write("\nEnter Updated Class and Section: ");
                string editClassSection = Console.ReadLine();

                editTeacher.Name = editName;
                editTeacher.ClassSection = editClassSection;

                File.Delete(filePath);

                List<string> output = new List<string>();
                foreach (var teacher in people)
                {
                    output.Add($"{teacher.ID},{teacher.Name},{teacher.ClassSection}");
                }

                File.AppendAllLines(filePath, output);
            }
        }
    }
}
