using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;//files
using System.Windows.Forms;
using System.Drawing;

namespace TheDiplomWork
{
    class Interface
    {
        public static void Greetings()
        {
            Colorator("Yay!", ConsoleColor.Yellow);
            Colorator("Designation: 8sem_TheCourseWork", ConsoleColor.Magenta);
        }
        public static void Colorator(string str, ConsoleColor color)
        {
            //Red,Green,Blue,Magenta,Cyan; example: ConsoleColor.Magenta;
            Console.ForegroundColor = color; // set text color;
            Console.WriteLine(str);
            Console.ResetColor(); // reset to normal text color;
        }
        public static int Pause()
        {
            Colorator("Press Escape to exit.", ConsoleColor.Magenta);

            for (ConsoleKeyInfo awaiter = new ConsoleKeyInfo(); true; Console.Write("'q' to quit, 'r' to show. Key: "),awaiter = Console.ReadKey(), Console.Write(" -> "))
            {
                switch (awaiter.KeyChar)
                {
                    case 'r': return 42;
                    case 'q': return 46;
                    //default: PlayTheMusic(); break;
                }
            }

        }
        public static void Pause_One_Time()
        {
            Console.Write("key: "); Console.ReadKey(); Console.Write(" -> ");
        }
        public static string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        public static Random random = new Random();
        public static void Time_pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
            { }
        }

    }
}
