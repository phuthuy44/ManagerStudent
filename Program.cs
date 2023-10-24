using ManagerStudent.GUI;
using ManagerStudent.DTO;
using ManagerStudent.DAL;
using System;
using System.Windows.Forms;
using System.Diagnostics;
using ManagerStudent.BLL;

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
            /*SemesterBLL semesterBLL = new SemesterBLL();
            Console.WriteLine(semesterBLL.GetDataSemester().Message);*/
            GetTypeOfPointData getTypeOfPointData = new GetTypeOfPointData();
            getTypeOfPointData.GetAllTypeOfPoint();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //Application.Run(new MainForm());
        }
    }
}
