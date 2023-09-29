using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProject.DTO
{
    public class Point:StudentResult
    {
        public required string subjectID {  get; set; }
        public required string typeofsubjectID {  get; set; }
        public required string typeofpointID {  get; set; }
        public Point(string subjectID, string typeofsubjectID, string typeofpointID, string studentID, string classID, string academicyearID, string semesterID, string gradeID, string name, float point, DateTime createDate, DateTime updateDate, DateTime updateTime)
        {
            this.typeofsubjectID = typeofsubjectID;
            this.subjectID = subjectID;
            this.typeofpointID = typeofpointID;
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
        public Point()
        {

        }
    }
}
