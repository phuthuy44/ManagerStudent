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

        private TypeOfPointDAL typeofpointDAL;

        public TypeOfPointBLL()
        {
            typeofpointDAL = new TypeOfPointDAL();
        }

        public DataTable TypeOfPointData()
        {
            return typeofpointDAL.TypeOfPointData();
        }
        public bool insertTypeofPointBLL(string pointName, int coefficient)
        {
            TypeOfPointDAL insertTypeofPointData = new TypeOfPointDAL();
            return insertTypeofPointData.insertTypeofPoint(pointName, coefficient);
        }

        public bool updateTypeofPointBLL(int ID, string pointName, int coefficient)
        {
            TypeOfPointDAL updateTypeofPointData = new TypeOfPointDAL();
            return updateTypeofPointData.updateTypeofPoint(ID, pointName, coefficient);
        }

        public bool deleteTypeofPointBLL(string pointName)
        {
            TypeOfPointDAL deleteTypeofPointData = new TypeOfPointDAL();
            return deleteTypeofPointData.deleteTypeofPoint(pointName);
        }

        public bool checkUpdateTypeofPointName(string pointName, int ID)
        {
            TypeOfPointDAL checkUpdateTypeofPointData = new TypeOfPointDAL();
            return checkUpdateTypeofPointData.checkUpdateTypeofPointName(pointName, ID);
        }

        public bool checkInsertTypeofPointName(string pointName)
        {
            TypeOfPointDAL checkInsertTypeofPointData = new TypeOfPointDAL();
            return checkInsertTypeofPointData.checkInsertTypeofPointName(pointName);
        }

        public DataTable FindTypeOfPoint(string str)
        {
            return typeofpointDAL.FindTypeOfPoint(str);
        }

    }
}
