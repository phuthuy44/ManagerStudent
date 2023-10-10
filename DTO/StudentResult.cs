using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class StudentResult
    {
        /* public required string ID {  get; set; }
         public required string studentID {  get; set; }
         public required string classID {  get; set; }
         public required string academicyearID {  get; set; }
         public required string semesterID {  get; set; }
         public required string gradeID {  get; set; }*/
        public  string ID { get; set; }
        public  string studentID { get; set; }
        public  string classID { get; set; }
        public  string academicyearID { get; set; }
        public  string semesterID { get; set; }
        public  string gradeID { get; set; }
        public string Name { get; set; }
        public float Point { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime updateTime { get; set; }
        public StudentResult(string iD, string studentID, string classID, string academicyearID, string semesterID, string gradeID, string name, float point, DateTime createDate, DateTime updateDate, DateTime updateTime)
        {
            ID = iD;
            this.studentID = studentID;
            this.classID = classID;
            this.academicyearID = academicyearID;
            this.semesterID = semesterID;
            this.gradeID = gradeID;
            Name = name;
            Point = point;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.updateTime = updateTime;
        }
        public StudentResult() { }
    }
}
