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
    }
}
