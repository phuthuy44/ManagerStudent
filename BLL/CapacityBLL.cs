using ManagerStudent.DAL;
using System.Data;

namespace ManagerStudent.BLL
{
    public class CapacityManager
    {
        private GetCapacityData capacityData;

        public CapacityManager()
        {
            capacityData = new GetCapacityData();
        }

        public DataTable GetAllCapacity()
        {
            return capacityData.GetAllCapacity();
        }

        public DataTable FindCapacity(string sql) {
            return capacityData.FindCapacity(sql);
        }
    }
}