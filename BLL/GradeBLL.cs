using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerStudent.GUI;
using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System.Data;
using System.Data.SqlClient;

namespace ManagerStudent.BLL
{

    public class GradeBLL
    {
        GradeDAL gradeDAL = new GradeDAL();

        public List<Grade> getAll()
        {
            return gradeDAL.GetAll();
        }

        public List<Grade> searchGrades(String searchTerm)
        {
            return gradeDAL.SearchGrades(searchTerm);
        }
        public string insertGrade(Grade grade)
        {
            if (gradeDAL.HasNameGrade(grade.Name))
                return "Tên đã tồn tại";
            gradeDAL.InsertGrade(grade);
            return "Thêm thành công";
        }

        public bool hasNameGrade(string name)
        {
            return gradeDAL.HasNameGrade(name);
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

        public bool checkUpdateGrade(string name, int id)
        {
            return gradeDAL.checkUpdateGrade(name, id);
        }

        public bool deleteGrade(int ma)
        {
            return gradeDAL.DeleteGrade(ma);
        }

    }
}
