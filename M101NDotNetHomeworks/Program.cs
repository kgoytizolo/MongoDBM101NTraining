using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M101NDotNetHomeworks.DataAccess;
using M101NDotNetHomeworks.Models;

namespace M101NDotNetHomeworks
{
    class Program
    {
        static void Main(string[] args)
        {
            //Warning: Before executing, make sure you imported students.json file from MongoDB exercises / homework 3.1
            //Command: > mongoimport --drop -d school -c students [Your path]/students.json
            //After that, it will work perfectly
            MainAsync(args).Wait();
            Console.ReadKey();
        }

        static async Task MainAsync(string[] args) {
            DataAccessGeneric checkHomeworks = new DataAccessGeneric(1);
            List <Student> listOfStudents = await checkHomeworks.getListOfStudentsTask31();
            //In case there are no results, print another message
            if (listOfStudents.Count() == 0 || listOfStudents == null)
            {
                Console.WriteLine("There are no elements returned by the exercise");
                Console.ReadLine();
            }
            else {
                //All documents will be displayed
                foreach (var student in listOfStudents) {
                    Console.WriteLine("Student ID: " + student.Id + ", Name: " + student.Name + ", Score Type: " + student.Grades[2].Type.ToString() + " / Score: " + student.Grades[2].Score);
                }
                //Displays answer

            }
        }
    }
}
