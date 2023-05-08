﻿namespace MVCCRUD_tutorial.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
    }
}
