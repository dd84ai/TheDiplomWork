using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace TheDiplomWork
{
    public partial class Form_ProjectileSettings : Form
    {
        public static bool Initializating = true;
        Wrapped_Box ProjectileSet = new Wrapped_Box(Projectile.settings);
        public Form_ProjectileSettings()
        {
            InitializeComponent();

            ProjectileSet.Add(textBox_mass, nameof(Projectile.settings.mass));
            ProjectileSet.Add(textBox_area, nameof(Projectile.settings.area));
            ProjectileSet.Add(textBox_cd, nameof(Projectile.settings.cd));
            ProjectileSet.Add(textBox_density, nameof(Projectile.settings.density));
            ProjectileSet.Add(textBox_windVx, nameof(Projectile.settings.windVx));
            ProjectileSet.Add(textBox_WindVz, nameof(Projectile.settings.windVy));
            ProjectileSet.Add(textBox_TimeSpeed, nameof(Projectile.settings.timespeed));


            checkBox_Advanced_Flying_Physics.Checked = Projectile.settings.AdvancedPhysics;
            ProjectileSet.RegulatingAdvancedSectorVisilibility();
            this.Show();
            Initializating = false;
        }

        
        private void button_Accept_Click(object sender, EventArgs e)
        {
            

            if (Projectile.settings.AdvancedPhysics)
                ProjectileSet.Wrapped_Transfer_or_Die();

            Projectile.jp.WP.Reignite_wind_from_static_info();
            Scene.SS.TrajectoryPath.Reloader();
            this.Close();
        }

        void Process_Text_Changed(TextBox Boxik)
        {
            try
            {
                ProjectileSet.Boxes[ProjectileSet.Boxes.FindIndex(x => x.Get_Name().Contains(Boxik.Name))].TrySetNewValue();
            }
            catch (Exception) { }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox_Advanced_Flying_Physics_CheckedChanged(object sender, EventArgs e)
        {
            Projectile.settings.AdvancedPhysics = checkBox_Advanced_Flying_Physics.Checked;
            ProjectileSet.RegulatingAdvancedSectorVisilibility();
        }       

        private void textBox_mass_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_area_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_cd_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_density_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_windVx_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_WindVz_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }

        private void textBox_TimeSpeed_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
        }
    }











    class Wrapped_Box
    {
        object obj;
        public Wrapped_Box(object inp)
        {
            obj = inp;
        }
        public List<Boxed> Boxes { get; } = new List<Boxed>();

        public void Wrapped_Transfer_or_Die()
        {
            foreach (var item in Boxes) item.Transfer_Or_Error(obj);
        }

        public void Add(TextBox Box, string name)
        {
            Boxes.Add(new Boxed(obj, Box, name));
        }

        public void RegulatingAdvancedSectorVisilibility()
        {
            foreach (var item in Boxes)
                if (item.Get_Name() != "timespeed")
                    item.SetVisibility(Projectile.settings.AdvancedPhysics);
        }

        public class Boxed
        {
            TextBox Box;
            string name;
            bool InitializedWithSomethingNew = false;
            public string Get_Name() { return Box.Name; }
            public static object GetPropValue(object src, string propName)
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            public Boxed(object obj, TextBox Box, string name)
            {
                this.Box = Box;
                this.name = name;

                Box.Text = ((double)GetPropValue(obj, name)).ToString("G6");
            }
            double NewValue = 0;
            bool changed = false;
            string error_message = "";
            public static string ReplaceDecimalSeparator(string inp)
            {
                inp = inp.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                inp = inp.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                return inp;
            }
            public void TrySetNewValue()
            {
                if (!Form_ProjectileSettings.Initializating)
                {
                    try
                    {
                        NewValue = Convert.ToDouble(ReplaceDecimalSeparator(Box.Text));

                        string[] NoNegativesFor = new string[] { "mass", "density", "cd", "area", "timespeed" };

                        foreach (var item in NoNegativesFor)
                            if (name == item)
                            {
                                if (NewValue < 0) throw new Exception("Отрицательное значение");
                                break;
                            }
                        changed = true;
                    }
                    catch (Exception exc)
                    {
                        changed = false;
                        error_message = exc.Message;
                    }
                    InitializedWithSomethingNew = true;
                }
            }

            public static void SetPropValue(object src, string propName, double value)
            {
                try
                {
                    src.GetType().GetProperty(propName).SetValue(src, value);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error!");
                }
            }
            public void Transfer_Or_Error(object obj)
            {
                if (InitializedWithSomethingNew)
                {
                    if (changed)
                    {
                        SetPropValue(obj, name, NewValue);
                        // = NewValue;
                    }
                    else MessageBox.Show(error_message, "Warning: " + Box.Name);
                }
            }
            public void SetVisibility(bool value) { Box.Visible = value; }

            
        }
    }

}













