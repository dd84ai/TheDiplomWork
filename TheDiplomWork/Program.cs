
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Windows;
namespace TheDiplomWork
{

    public class StaticAccess
    {
        public static FormModernOpenGLSample FMOS;
        public static AboutBox1 AB = null;
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