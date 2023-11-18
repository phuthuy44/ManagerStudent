using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class ConductBLL
    {
        private GetConductData ConductData;
        public ConductBLL()
        {
            ConductData = new GetConductData();
        }
        public DataTable GetConductData()
        {
            return ConductData.GetallConduct();
        }

        public DataTable FindConduct(string str) {
            return ConductData.FindConduct(str);
        }
        public bool insertConducts(string conductName, int upperLimit, int lowerLimit)
        {
            GetConductData insertConductData = new GetConductData();
            return insertConductData.insertConduct(conductName, upperLimit, lowerLimit);
        }

        public bool deleteConducts(string conductName)
        {
            GetConductData deleteConductData = new GetConductData();
            return deleteConductData.deleteConduct(conductName);
        }

        public bool updateConducts(string conductName, int upperLimit, int lowerLimit, int ID)
        {
            GetConductData updateConductData = new GetConductData();
            return updateConductData.updateConduct(conductName, upperLimit, lowerLimit, ID);
        }

        public bool checkUpdateConductName(string conductName, int ID)
        {
            GetConductData checkUpdateConductData = new GetConductData();
            return checkUpdateConductData.checkUpdateConductName(conductName, ID);
        }

        public bool checkInsertConductName(string conductName)
        {
            GetConductData checkInsertConductData = new GetConductData();
            return checkInsertConductData.checkInsertConductName(conductName);
        }
    }
}
