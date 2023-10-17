using ManagerStudent.GUI;
using ManagerStudent.DTO;
using ManagerStudent.DAL;
using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace ManagerStudent
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HocSinhForm());*/
            ConnectGGSheet connect;
         
            Student student = new Student();
            student.Name = "123eeacb";
            log log = new log("..\\..\\file.log");
            log.writeLog(student.Name);
            Console.WriteLine(student.Name);

            GetAccountData getAccountData = new GetAccountData();
            getAccountData.GetAllAccount();
        }
    }
}
