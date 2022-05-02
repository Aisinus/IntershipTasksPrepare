using IntershipTasks;
using static IntershipTasks.DataBase;

using (var db = new DatabaseContext())
{
    /*
    Student student = new Student
    {
        Name = "Michael"
    };
    db.Students.Add(student);
    db.SaveChanges();
    */
    /*
    var prof = new Professor
    {
        Name = "Ivan Ivanov"
    };

    db.Professors.Add(prof);
    var students = db.Students.ToList();
    var student = db.Students.ToList().First();
    student.SupervisorProfessor = prof;

    var course = new Course
    {
        Name = "Test1",
        Professor = prof
    };

    db.Courses.Add(course);

    course.Students.Add(student);
    course.Students.Add(student);

    db.SaveChanges();

    var check = db.Students.ToList();
    foreach(var c in check)
    {
        Console.WriteLine(c.Name+" "+c.SupervisorProfessor.Name);
    }
    */
    var course = db.Courses.First();
    var studentcheck=new Student { Name = "Michail" };
    db.Students.Add(studentcheck);
    db.SaveChanges();


    db.Entry(course).Collection(s => s.Students).Load();
    course.Students.Add(studentcheck);
    db.SaveChanges();


    //db.Entry(course).Collection(s => s.Students).Load();
    foreach (var student in course.Students)
    {
        System.Console.WriteLine(student.Name);
        foreach (var StudentCourse in student.Courses)
        {
            System.Console.WriteLine(StudentCourse.Name);
        }
    }
}