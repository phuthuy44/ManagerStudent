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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
/*            Application.Run(new LoginForm());*/
            Application.Run(new MainForm());
        }
    }
}
