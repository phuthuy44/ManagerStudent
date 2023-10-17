using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class log
    {
        private string logFilePath;

        public log()
        {
        }

        public log(string filePath)
        {
            logFilePath = filePath;
        }

        public void writeLog(string message)
        {
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}");
            }
        }

        internal void writeLog(SqlString sqlString)
        {
            throw new NotImplementedException();
        }
    }
}
