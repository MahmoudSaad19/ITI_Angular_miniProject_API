namespace Student.Repository.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StudentContext : DbContext
    {
        public StudentContext()
            : base("name=StudentContext.cs")
        {
        }
        public virtual DbSet<Student> Students { get; set; }
    }
}