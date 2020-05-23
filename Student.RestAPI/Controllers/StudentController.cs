using Microsoft.Ajax.Utilities;
using Student.Repository.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Student.RestAPI.Controllers
{
    public class StudentController : ApiController
    {
        public IHttpActionResult GetStudents()
        {
            var stds = StudentRepository.GetStudents();
            if (stds.Count>0)
            {
                return Ok(stds);
            }
            return NotFound();
        }

        public IHttpActionResult GetStudent(int id)
        {
            var std = StudentRepository.GetStudent(id);
            if (std is null)
            {
                return NotFound();
            }
            return Ok(std);
        }

        public IHttpActionResult POSTStudent()
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpPostedFile image = request.Files["image"];
            Repository.Models.Student student1 = new Repository.Models.Student()
            {
                Name = request["Name"],
                Age = int.Parse(request["Age"]),
                Email = request["Email"],
                Image = saveImageToFile(image)
            };

            StudentRepository.AddStudent(student1);
            return Created("Done", student1);

            //if (ModelState.IsValid)
            //{
            //    if (StudentRepository.AddStudent(student))
            //    {
            //        return Created("Added Successfully", student);
            //    }
            //}
            //return BadRequest("Input Data Is Not Valid ----> See Help");
        }

        private string saveImageToFile(HttpPostedFile imageFile)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension = Path.GetExtension(imageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtension;
            string imagePath = "/Content/Images/" + fileName;
            fileName = HttpContext.Current.Server.MapPath(imagePath);
            imageFile.SaveAs(fileName);
            return imagePath;
        }

        public IHttpActionResult PUTStudent([FromUri]int id)
        {
            HttpRequest request = HttpContext.Current.Request;
            string imageChanged = request["imageChanged"];
            string image = imageChanged.Contains("false") ? request["image"] : saveImageToFile(request.Files["image"]);            
            Repository.Models.Student student = new Repository.Models.Student()
            {
                ID = int.Parse( request["ID"]),
                Name = request["Name"],
                Age = int.Parse(request["Age"]),
                Email = request["Email"],
                Image = image
            };
            if (StudentRepository.EditStudent(id, student))
            {
                return Ok(student);
            }
            return BadRequest("Input Data Is Not Valid ----> See Help");
        }

        public IHttpActionResult DELETEStudent(int id)
        {
            if (StudentRepository.DeleteStudent(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

    }
}
