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

            public List<StudentCourse> StudentCourses { get; set; } = null!;
            public List<Course> Courses { get; set; }

            public int? SupervisorProfessorID { get; set; }
            public Professor? SupervisorProfessor { get; set; }

        }

        public class Course
        {
            public int CourseID { get; set; }
            public string Name { get; set; }

            public List<StudentCourse> StudentCourses { get; set; } = null!;
            public List<Student> Students { get; set; }

            public int? ProfessorID { get; set; }
            public Professor? Professor { get; set; }
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
            public Course? Course { get; set; }
            public List<Student> SupervisedStudents { get; set; } = null!;
        }

        public class DatabaseContext : DbContext
        {

            public DbSet<Student> Students { get; set; } = null!;
            public DbSet<Course> Courses { get; set; } = null!;
            public DbSet<Professor> Professors { get; set; } = null!;
            public DbSet<StudentCourse> StudentCourse { get; set; } = null!;

            public DatabaseContext()
            {
                // Database.EnsureDeleted();
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source = database.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Student>().HasKey(x => x.StudentID);
                modelBuilder.Entity<Course>().HasKey(x => x.CourseID);
                modelBuilder.Entity<Professor>().HasKey(x => x.ProfessorID);

                // One to one or zero Professor-Course
                modelBuilder.Entity<Professor>()
                    .HasOne<Course>(c => c.Course)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Course>(c=>c.ProfessorID)
                    .OnDelete(DeleteBehavior.SetNull);


                // One to Many Professor-Student
                modelBuilder.Entity<Student>()
                    .HasOne<Professor>(s=>s.SupervisorProfessor)
                    .WithMany(p=>p.SupervisedStudents)
                    .HasForeignKey(s=>s.SupervisorProfessorID)
                    .OnDelete(DeleteBehavior.SetNull);

                // Many to Many Student-StudentCourse-Course
                modelBuilder
                    .Entity<Course>()
                    .HasMany(c => c.Students)
                    .WithMany(s => s.Courses)
                    .UsingEntity<StudentCourse>(
                        j => j
                            .HasOne(pt => pt.Student)
                            .WithMany(t => t.StudentCourses)
                            .HasForeignKey(pt => pt.StudentID)
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne(pt => pt.Course)
                            .WithMany(p => p.StudentCourses)
                            .HasForeignKey(pt => pt.CourseID)
                            .OnDelete(DeleteBehavior.Cascade),
                        j => {
                            j.HasKey(t => new { t.CourseID, t.StudentID });
                            j.ToTable("StudentCourses");
                        }
                    );

                modelBuilder.Entity<Course>()
                    .Property(c => c.Name)
                    .HasMaxLength(50);  // Column constraints

            }
        }
    }
}
