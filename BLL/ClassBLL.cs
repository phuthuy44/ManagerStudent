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
            if (clsdal.HasNameClass(cls.Name))
                return "Tên đã tồn tại";
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
        public bool checkUpdateClass(string name, int id)
        {
            return clsdal.checkUpdateClass(name, id);
        }
        public bool deleteClass(int ma)
        {
            return clsdal.DeleteClass(ma);
        }
    }
}
