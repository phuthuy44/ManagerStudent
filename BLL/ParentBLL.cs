using ManagerStudent.DAL;
using ManagerStudent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class ParentBLL
    {
        public ParentDAL parent;
        public ParentBLL()
        {
            parent = new ParentDAL();
        }
        public List<Parent> getDataCha(int id)
        {
            return parent.getDataCha(id);
        }
        public List<Parent> getDataMe(int id)
        {
            return parent.getDataMe(id);
        }
        public bool insertParent(Parent p)
        {
            if (parent.checkExistCha(p.ID, p.Gender)){
                return parent.updateParentCha(p);
            }
            else
            {
                return parent.insertParent(p);
            }
        }
    }
}
