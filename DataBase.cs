using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntershipTasks
{
    internal class DataBase
    {
        public class Student
        {
            
            public int StudentID { get; set; }
            public string Name { get; set; } = null!;

            public List<StudentCourse> Courses { get; set; } = null!;


        }

        public class Course
        {
            public int CourseID { get; set; }
            public string Name { get; set; } = null!;

            public List<StudentCourse> Students { get; set; } = null!;

        }

        public class StudentCourse
        {
            public int StudentID { get; set; }
            public Student Student { get; set; } = null!;

            public int CourseID { get; set; }
            public Course Course { get; set; } = null!;
        }

        public class Professor
        {
            public int ProfessorID { get; set; }
            public string Name { get; set; } = null!;



        }

        public class DatabaseContext : DbContext
        {

            public DbSet<Student> Students { get; set; } = null!;
            public DbSet<Course> Courses { get; set; } = null!;
            public DbSet<Professor> Professors { get; set; } = null!;

            public DatabaseContext()
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source = database.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>().HasKey(x => x.StudentID);
                modelBuilder.Entity<Student>().HasOne;
                base.OnModelCreating(modelBuilder);

            }
        }
    }
}
