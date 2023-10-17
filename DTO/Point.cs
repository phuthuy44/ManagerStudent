using System;


namespace ManagerStudent.DTO
{
    public class Point : StudentResult
    {
        public string subjectID {  get; set; }
        public string typeofsubjectID {  get; set; }
        public string typeofpointID {  get; set; }
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

        public Point(string subjectID, string typeofsubjectID, string typeofpointID)
        {
            this.subjectID = subjectID;
            this.typeofsubjectID = typeofsubjectID;
            this.typeofpointID = typeofpointID;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
