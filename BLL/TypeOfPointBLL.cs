using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class TypeOfPointBLL
    {

        private TypeOfPointDAL pointDAL;

        public TypeOfPointBLL()
        {
            pointDAL = new TypeOfPointDAL();
        }

        public DataTable TypeOfPointData()
        {
            Console.WriteLine("kkkkkk");
            return pointDAL.TypeOfPointData();
        }

    }
}
