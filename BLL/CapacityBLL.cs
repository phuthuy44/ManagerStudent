using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class CapacityBLL
    {
        public Response GetCapacityData()
        {
            GetCapacityData getCapacityData = new DAL.GetCapacityData();
            if (getCapacityData.GetAllCapacity().Count == 0)
            {
                return new Response(false, "Lấy dữ liệu thất bại.", null);
            }
            return new Response(true, "Lấy dữ liệu thành công.", getCapacityData.GetAllCapacity());
        }
    }
}
