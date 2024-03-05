using System;

namespace SMS_Student_Management_System_.Models
{
    public class Student
    {
        public long StudentId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string StudentNumber { get; set; } 
        public int Grade { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
