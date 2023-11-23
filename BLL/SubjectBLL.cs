using ManagerStudent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.BLL
{
    internal class SubjectBLL
    {
        private GetSubjectData subjectData;
        public SubjectBLL()
        {
            subjectData = new GetSubjectData();
        }

        public DataTable GetSubjectData()
        {
            return subjectData.GetAllSubject();
        }

        public DataTable FindSubjects(string str)
        {
            return subjectData.FindSubject(str);
        }

        public bool insertSubjects(string subjectName)
        {
            GetSubjectData insertSubjectData = new GetSubjectData();
            return insertSubjectData.insertSubject(subjectName);
        }

        public bool deleteSubjects(string subjectName)
        {
            GetSubjectData deleteSubjectData = new GetSubjectData();
            return deleteSubjectData.deleteSubject(subjectName);
        }

        public bool updateSubjects(int ID, string subjectName)
        {
            GetSubjectData updateSubjectData = new GetSubjectData();
            return updateSubjectData.updateSubject(ID, subjectName);
        }

        public bool checkInsertSubjectName(string subjectName)
        {
            GetSubjectData checkInsertSubjectData = new GetSubjectData();
            return checkInsertSubjectData.checkInsertSubject(subjectName);
        }

        public bool checkUpdateSubjectName(int ID, string subjectName)
        {
            GetSubjectData checkUpdateSubjectData = new GetSubjectData();
            return checkUpdateSubjectData.checkUpdateSubject(ID, subjectName);
        }
    }
}
