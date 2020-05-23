using Student.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Student.Repository.Repository
{
    public static class StudentRepository
    {
        static StudentContext Db;
        static StudentRepository()
        {
            Db = new StudentContext();
        }

        public static List<Models.Student> GetStudents()
        {
            var stds = Db.Students.ToList();
            if (stds.Count == 0)
            {
                return new List<Models.Student>();
            }
            return stds;
        }

        public static Models.Student GetStudent(int id)
        {
            return Db.Students.Find(id);
        }

        public static bool AddStudent(Models.Student student)
        {
            if (student is null)
            {
                return false;
            }

            Db.Students.Add(student);
            return Db.SaveChanges() > 0;
        }

        public static bool EditStudent(int id, Models.Student student)
        {
            if (id != student.ID)
            {
                return false;
            }
            var std = Db.Students.AsNoTracking().FirstOrDefault(i => i.ID == id);
            //var std = Db.Students.Find(id);
            if (std != null)
            {
                Db.Entry(std).State = EntityState.Detached;
                Db.Entry(student).State = EntityState.Modified;
                //std.Age = student.Age;
                //std.Email = student.Email;
                //std.Image = student.Image;
                //std.Name = student.Name;
                return Db.SaveChanges() > 0;
            }
            return false;
        }

        public static bool DeleteStudent(int id)
        {
            var std = Db.Students.Find(id);
            if (!Db.Students.Any(i => i.ID == id))
            {
                return false;
            }
            Db.Students.Remove(std);
            return Db.SaveChanges() >0;
        }
    }
}
