using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerStudent.GUI;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
namespace ManagerStudent.BLL
{
    public class GradeBLL
    {
        GradeDAL gradeDAL = new GradeDAL();
        
        public List<Grade> getAll()
        {
            return gradeDAL.GetAll();
        }

        public void insertGrade(Grade grade)
        {
             gradeDAL.InsertGrade(grade);
        }

        public int GetLastGradeId()
        {
            return gradeDAL.getlastgradeid();
        }

        public void updateGrade(Grade grade)
        {
            gradeDAL.UpdateGrade(grade);
        }

        public void deleteGrade(Grade grade) {
            gradeDAL.DeleteGrade(grade);
        }
    }
}
