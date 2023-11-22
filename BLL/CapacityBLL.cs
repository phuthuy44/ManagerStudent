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
        public bool checkInsertCapacityName(string capacityName)
        {
            GetCapacityData checkInsertCapacityData = new GetCapacityData();
            return checkInsertCapacityData.checkInsertCapacityName(capacityName);
        }

        public bool checkUpdateCapacityName(int ID, string capacityName)
        {
            GetCapacityData checkUpdateCapacityData = new GetCapacityData();
            return checkUpdateCapacityData.checkUpdateCapacityName(ID, capacityName);
        }

        public bool insertCapacities(string capacityName, float upperLimit, float lowerLimit, float paraPoint)
        {
            GetCapacityData insertCapacityData = new GetCapacityData();
            return insertCapacityData.insertCapacity(capacityName, upperLimit, lowerLimit, paraPoint);
        }

        public bool updateCapacities(int ID, string capacityName, float upperLimit, float lowerLimit, float paraPoint)
        {
            GetCapacityData updateCapacityData = new GetCapacityData();
            return updateCapacityData.updateCapacity(ID, capacityName, upperLimit, lowerLimit, paraPoint);
        }

        public bool deleteCapacities(string capacityName)
        {
            GetCapacityData deleteCapacityData = new GetCapacityData();
            return deleteCapacityData.deleteCapacity(capacityName);
        }
    }
}
