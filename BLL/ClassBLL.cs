using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerStudent.DTO;
using ManagerStudent.DAL;

namespace ManagerStudent.BLL
{
    public class ClassBLL
    {
        ClassDAL clsdal = new ClassDAL();
        public List<Class> getAll()
        {
            return clsdal.getAll();
        }
        public string insertClass(Class cls)
        {
            clsdal.InsertClass(cls);
            return "Them thanh cong";
        }
        public int getlastclassid()
        {
            return clsdal.getlastclassid();
        }
        public string updateClass(Class cls)
        {
            clsdal.UpdateClass(cls);
            return "Cập nhập thành công";
        }
    }
}
