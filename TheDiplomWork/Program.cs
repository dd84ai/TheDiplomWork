using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TheDiplomWork
{
    public class StaticAccess
    {
        public static FormModernOpenGLSample FMOS;
    }
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StaticAccess.FMOS = new FormModernOpenGLSample();
            Application.Run(StaticAccess.FMOS);
        }
    }
}