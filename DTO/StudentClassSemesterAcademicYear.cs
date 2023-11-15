using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    internal class StudentClassSemesterAcademicYear
    {
        public StudentClassSemesterAcademicYear()
        {
        }

        public int studentID { get; set; }
        public int classID { get; set; }
        public int semesterID { get; set; }
        public int academicyearID { get; set; }
        
        public int gradeID { get; set; }
        public int stateID { get; set; }

        public StudentClassSemesterAcademicYear(int studentID, int classID, int semesterID, int academicyearID, int gradeID, int stateID)
        {
            this.studentID = studentID;
            this.classID = classID;
            this.semesterID = semesterID;
            this.academicyearID = academicyearID;
            this.gradeID = gradeID;
            this.stateID = stateID;
        }
    }
}
