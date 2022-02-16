using System;
using System.Windows.Forms;

namespace DropTop
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.Run(new DropTopContext());
        }
    }
}