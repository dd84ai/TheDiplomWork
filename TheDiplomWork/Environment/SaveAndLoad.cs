using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace TheDiplomWork
{
    class SaveAndLoad
    {
        public static string FileDirectory = "\\Save\\World_";
        public const string SystemSign = "SystemSign_";
        public const string Sign_New_Xcube = SystemSign + "Sign_New_Xcube";
        public const string Sign_New_XYcube = SystemSign + "Sign_New_XYcube";
        public const string Sign_New_XYZcube = SystemSign + "Sign_New_XYZcube";
        public static void Save(string foldername)
        {
            string FinalPath = Interface.ProjectPath + FileDirectory + foldername + "\\";
            bool exists = System.IO.Directory.Exists(FinalPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(FinalPath);

            foreach (var Xworld in Scene.SS.env.cub_mem.world.World_as_Whole)
                foreach(var XYworld in Xworld)
                {
                    using (StreamWriter outputFile = new StreamWriter(FinalPath
                + $"chunk_{XYworld.xz.x}_{XYworld.xz.z}.dat"))
                    {

                        foreach (var Xcube in XYworld.cubes)
                        {
                            foreach (var XYcube in Xcube)
                            {
                                foreach (var XYZcube in XYcube)
                                {
                                    if (XYZcube.Changed)
                                    {
                                        outputFile.WriteLine(XYZcube.xyz.x + ";" +
                                            XYZcube.xyz.y + ";" +
                                            XYZcube.xyz.z + ";" +
                                            XYZcube.IsFilled.ToString() + ";" +
                                            XYZcube.color.Name.ToString());
                                        //System.Drawing.Color.FromName();
                                    }
                                }
                            }
                        }
                    }
                }//Foreachworldend
            
        }//Function End
        public static void Load(string foldername)
        {
            try
            {
                string FinalPath = Interface.ProjectPath + FileDirectory + foldername + "\\";
                bool exists = System.IO.Directory.Exists(FinalPath);

                if (!exists) return;

                foreach (var Xworld in Scene.SS.env.cub_mem.world.World_as_Whole)
                    foreach (var XYworld in Xworld)
                    {
                        string str = "";
                        using (StreamReader InputFile = new StreamReader(FinalPath
                    + $"chunk_{XYworld.xz.x}_{XYworld.xz.z}.dat"))
                        {
                            string[] splitted; int i = 0, j = 0, k = 0;

                            while ((str = InputFile.ReadLine())!=null)
                            {
                                splitted = str.Split(';');
                                i = Convert.ToInt32(splitted[0]);
                                j = Convert.ToInt32(splitted[1]);
                                k = Convert.ToInt32(splitted[2]);
                                XYworld.cubes[i][j][k].IsFilled = Convert.ToBoolean(splitted[3]);
                                XYworld.cubes[i][j][k].color = System.Drawing.Color.FromName(splitted[4]);
                                XYworld.cubes[i][j][k].Changed = true;
                            }
                        }
                    }//Foreachworldend
            }
            catch (Exception message)
            {
                MessageBox.Show($"{message.Message}");
            }
        }//Function End
    }
}
