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
        public static bool Inited = false;
        public Interface()
        {
            
            
        }
        public static bool IsReadyToPlay()
        {
            Interface.Player = new WMPLib.WindowsMediaPlayer();
            Interface.Inited = true;
            return true;
        }
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
        public static WMPLib.WindowsMediaPlayer Player;
        public static bool Player_is_online = false;
        public static void PlayTheMusic()
        {
            
            string TargetPath = ProjectPath + "\\" + "Music" + "\\";

            if (Directory.Exists(@TargetPath))
            {

                // Process the list of files found in the directory.
                var fileEntries = Directory.GetFiles(TargetPath);

                int Choice = random.Next(0, fileEntries.Count()-1);
                if (fileEntries.Count() != 0)
                    if (true)
                    {
                        try
                        {
                            Player_is_online = true;
                            //Colorator("Activating music file: " + fileEntries[Choice], ConsoleColor.Yellow);
                            Player.URL = fileEntries[Choice];
                            Player.settings.volume = 50;
                            Player.controls.play();
                            //System.Media.SoundPlayer sp = new System.Media.SoundPlayer(fileEntries[Choice]);
                            //sp.Play();
                            string[] spliited = fileEntries[Choice].Split('\\');
                            Colorator("Music file " + spliited[spliited.Count() - 1] + " has been activated. ", ConsoleColor.Green);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine();
                            Colorator("Music file " + fileEntries[Choice] + " can't be activated!!! ", ConsoleColor.Red);
                        }

                    }
            }
            else
            {
                Console.WriteLine();
                Colorator("Folder does not exist!!! ", ConsoleColor.Red);
            }
        }
        public static System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
        public static void SaySoundEffect(string Name)
        {
            string TargetPath = ProjectPath + "\\" + "Sounds" + "\\";

            if (Directory.Exists(@TargetPath))
            {

                // Process the list of files found in the directory.
                        try
                        {
                            sp.SoundLocation = TargetPath + Name + ".wav";
                    //if (sp.)
                    sp.Play();
                    Colorator("Music file " + TargetPath + Name + ".wav" + " has been activated. ", ConsoleColor.Green);
                        }
                        catch (Exception Er)
                        {
                            Console.WriteLine();
                            Colorator("Music file " + TargetPath + Name + ".wav" + " can't be activated!!! ", ConsoleColor.Red);
                        }
                
            }
            else
            {
                Console.WriteLine();
                Colorator("Folder does not exist!!! ", ConsoleColor.Red);
            }
        }
        public static void Time_pause(int value)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
            { }
        }

    }
}
