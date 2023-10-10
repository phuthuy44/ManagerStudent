using ManagerStudent.GUI;
using ManagerStudent.DTO;
using ManagerStudent.DAL;
using System;
using System.Windows.Forms;

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
            Application.Run(new MainForm());*/
            Student student = new Student();
            student.Name = "123acb";
            Console.WriteLine(student.Name);
        }
    }
}
