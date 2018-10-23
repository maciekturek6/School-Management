using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercise_1.Models;

namespace Exercise_1.Identity
{
    public static class DbInitializer
    {
        public static void Initialize(EFCContext context)
        {
            

            
            if (context.Students.Any())
            {
                return;   
            }

            var students = new Student[]
            {
                new Student { FirstMidName = "Maciej",   LastName = "Turek",
                    EnrollmentDate = DateTime.Parse("2018-10-01") },
                new Student { FirstMidName = "Marlen", LastName = "Zdun",
                    EnrollmentDate = DateTime.Parse("2018-10-05") },
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Roman",     LastName = "Kuba",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Kuba",    LastName = "Janas",
                    HireDate = DateTime.Parse("2015-07-06") },
            };

            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350,
                    StartDate = DateTime.Parse("2018-10-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kuba").ID },
                new Department { Name = "Mathematics", Budget = 100,
                    StartDate = DateTime.Parse("2018-11-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Janas").ID },
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
            };

            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Lewandowski").ID,
                    Location = "Piłsudzkiego 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Milik").ID,
                    Location = "Rokowań 27" },
            };

            foreach (OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Janas").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kuba").ID
                    },       
            };

            foreach (CourseAssignment ci in courseInstructors)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Wojtek").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                    new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Janas").ID,
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    Grade = Grade.C
                    },

            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.ID == e.StudentID &&
                            s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
