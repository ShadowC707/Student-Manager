using System;

namespace Students.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Parent Mother { get; set; }
        public Parent Father { get; set; }

    }
}