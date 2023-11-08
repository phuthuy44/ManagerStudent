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

     /*   public List<Grade> searchGrades(String searchTerm)
        {
            return gradeDAL.SearchGrades(searchTerm);
        }*/

        public string insertGrade(Grade grade)
        {
             gradeDAL.InsertGrade(grade);
            return "Thêm thành công";
        }

        public int GetLastGradeId()
        {
            return gradeDAL.getlastgradeid();
        }

        public string updateGrade(Grade grade)
        {
            gradeDAL.UpdateGrade(grade);
            return "Cập nhập thành công";
        }

        public void deleteGrade(Grade grade) {
            gradeDAL.DeleteGrade(grade);
        }
    }
}
