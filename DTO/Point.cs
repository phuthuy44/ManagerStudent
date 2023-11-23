using System;


namespace ManagerStudent.DTO
{
    public class Point : StudentResult
    {
        public string subjectID {  get; set; }
           public string typeofpointID { get; set; }
        public Point(string subjectID , string typeofpointID, string studentID, string classID, string academicyearID, string semesterID, float point, DateTime createDate, DateTime updateDate, DateTime updateTime)
        {
            this.subjectID = subjectID;
            this.typeofpointID = typeofpointID;
            this.studentID = studentID;
            this.classID = classID;
            this.academicyearID = academicyearID;
            this.semesterID = semesterID;
            Point = point;
            this.createDate = createDate;
            this.updateDate = updateDate;
            this.updateTime = updateTime;
        }
        public Point()
        {

        }

        public Point(string subjectID, string typeofsubjectID, string typeofpointID)
        {
            this.subjectID = subjectID;
            this.typeofpointID = typeofpointID;
        }
    }
}
