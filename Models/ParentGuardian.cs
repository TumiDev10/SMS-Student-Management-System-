using System;

namespace SMS_Student_Management_System_.Models
{
    public class ParentGuardian
    {
        public long ParentId { get; set; } 
        public long StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RelationshipToStudent { get; set; }
        public string ContactDetails { get; set; }
        public string? Address { get; set; }
    }
}