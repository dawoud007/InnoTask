using System.ComponentModel.DataAnnotations;

namespace ThirdApp;
public class Student
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public int TotalGrads { get; set; }
    public int Level { get; set; }
    public int Age { get; set; }

}
