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
    class Music
    {
        public static string ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        public static WMP_player wmp_player;
        public static WAV_player wav_player;
        public static void Initialize()
        {
            wmp_player = new WMP_player();
            wav_player = new WAV_player();
        }

        public class WMP_player
        {
            public bool Inited = false;
            public WMP_player()
            {
                Player = new WMPLib.WindowsMediaPlayer();
                Inited = true;
            }
            public Random random = new Random();
            public WMPLib.WindowsMediaPlayer Player;
            public bool Player_is_online = false;
            public string PlayerTime()
            {
                return Player.controls.currentPositionString;
            }
            public string PlayerSongName()
            {
                return LastSong;
            }
            public string LastSong = "";
            public void SetLastSong(string song)
            {
                string[] splitted = song.Split('\\');
                LastSong = splitted[splitted.Count() - 1];
            }
            public void PlayTheMusic_NextSong()
            {
                StartTheMusic();
            }
            public void PlayTheMusic_Checker()
            {
                string PlayerStatus = Player.playState.ToString();
                if (PlayerStatus.Contains("Undefined") || PlayerStatus.Contains("Stopped"))
                {
                    StartTheMusic();
                }
            }
            public void PlayTheMusic()
            {
                string PlayerStatus = Player.playState.ToString();
                if (PlayerStatus.Contains("Undefined") || PlayerStatus.Contains("Stopped"))
                {
                    StartTheMusic();
                }
                else if (PlayerStatus.Contains("Playing"))
                {
                    Player.controls.pause();
                }
                else if (PlayerStatus.Contains("Paused"))
                {
                    Player.controls.play();
                }
            }
            public int LastChoice = -1;
            public int volumepower = 100;
            public void StartTheMusic()
            {
                string TargetPath = ProjectPath + "\\" + "Music" + "\\";

                if (Directory.Exists(@TargetPath))
                {
                    // Process the list of files found in the directory.
                    var fileEntries = Directory.GetFiles(TargetPath);

                    int Choice = random.Next(0, fileEntries.Count() - 1);

                    //Не ту что играла сейчас.
                    if (fileEntries.Count() > 1)
                        while ((Choice = random.Next(0, fileEntries.Count() - 1)) == LastChoice) { }

                    LastChoice = Choice;
                    if (fileEntries.Count() != 0)
                        if (true)
                        {
                            try
                            {
                                Player_is_online = true;
                                //Colorator("Activating music file: " + fileEntries[Choice], ConsoleColor.Yellow);
                                Player.URL = fileEntries[Choice];

                                SetLastSong(fileEntries[Choice]);
                                
                                Player.settings.volume = volumepower;
                                Player.settings.playCount = 1;
                                Player.controls.play();
                                //System.Media.SoundPlayer sp = new System.Media.SoundPlayer(fileEntries[Choice]);
                                //sp.Play();
                                string[] spliited = fileEntries[Choice].Split('\\');
                                Interface.Colorator("Music file " + spliited[spliited.Count() - 1] + " has been activated. ", ConsoleColor.Green);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine();
                                Interface.Colorator("Music file " + fileEntries[Choice] + " can't be activated!!! ", ConsoleColor.Red);
                            }

                        }
                }
                else
                {
                    Console.WriteLine();
                    Interface.Colorator("Folder does not exist!!! ", ConsoleColor.Red);
                }
            }
        }

        public class WAV_player
        {
            public System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
            public void SaySoundEffect(string Name)
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
                        Interface.Colorator("Music file " + TargetPath + Name + ".wav" + " has been activated. ", ConsoleColor.Green);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine();
                        Interface.Colorator("Music file " + TargetPath + Name + ".wav" + " can't be activated!!! ", ConsoleColor.Red);
                    }

                }
                else
                {
                    Console.WriteLine();
                    Interface.Colorator("Folder does not exist!!! ", ConsoleColor.Red);
                }
            }
        }
    }
}
