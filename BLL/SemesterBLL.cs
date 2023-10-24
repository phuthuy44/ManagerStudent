using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class SemesterBLL
    {
        
        public Response GetDataSemester()
        {
            GetSemesterData getSemesterData = new GetSemesterData();
            IList<Semester> Semestes = getSemesterData.GetAllSemester();
            if (Semestes.Count == 0)
            {
                return new Response(false, "Lấy dữ liệu thất bại!", null);
            }
            return new Response(true, "Lấy dữ liệu thành công!", Semestes);
        }
    }
}
