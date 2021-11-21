using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SIF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private static List<Course> courses { get; }
        private static int nextId = 4;

        private readonly ILogger<CourseController> _logger;

        static CourseController()
        {
            courses = new List<Course> {
                new Course {Id=1,Name="English" },
                new Course {Id=2,Name="Maths" },
                new Course {Id=3,Name="Computer Science" },
            };
        }

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Course Get(int id)
        {
            return courses.Where(c => c.Id == id).FirstOrDefault();
        }

        [HttpGet("All")]
        public ActionResult<List<Course>> GetAll()
        {
            return Ok(courses);
        }

        [HttpPut]
        public ActionResult<Course> AddCourse(Course course)
        {
            if (courses.Exists(c => c.Name == course.Name))
            {
                return BadRequest("Course already exists");
            }

            course.Id = nextId++;

            courses.Add(course);

            return Ok(course);
        }

        [HttpPost]
        public ActionResult<Course> SaveCourse(Course course)
        {
            if (!courses.Exists(c => c.Id == course.Id))
            {
                return BadRequest($"Course with id = {course.Id} not exists");
            }

            courses.First(c => c.Id == course.Id).Name = course.Name;

            return Ok(course);
        }

        [HttpDelete]
        public ActionResult<Course> DeleteCourse(int id)
        {
            //if (!courses.Exists(c => c.Id == id))
            //{
            //    return BadRequest($"Course with id = {id} not exists");
            //}

            courses.Remove(courses.First(c => c.Id == id));

            return Ok();
        }


    }
}
