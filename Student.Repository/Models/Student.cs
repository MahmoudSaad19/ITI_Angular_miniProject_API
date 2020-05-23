using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Repository.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        
        public string Name { get; set; }
        
        public int Age { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        public string Image { get; set; }
    }
}
