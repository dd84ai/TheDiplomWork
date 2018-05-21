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
        List<Boxed> Boxes = new List<Boxed>();
        public Form_ProjectileSettings()
        {
            InitializeComponent();

            Boxes.Add(new Boxed(textBox_mass, nameof(Projectile.settings.mass)));
            Boxes.Add(new Boxed(textBox_area, nameof(Projectile.settings.area)));
            Boxes.Add(new Boxed(textBox_cd, nameof(Projectile.settings.cd)));
            Boxes.Add(new Boxed(textBox_density, nameof(Projectile.settings.density)));
            Boxes.Add(new Boxed(textBox_windVx, nameof(Projectile.settings.windVx)));
            Boxes.Add(new Boxed(textBox_WindVz, nameof(Projectile.settings.windVy)));

            checkBox_Advanced_Flying_Physics.Checked = Projectile.settings.AdvancedPhysics;
            RegulatingAdvancedSectorVisilibility();
            this.Show();
            Initializating = false;
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
            public Boxed(TextBox Box, string name)
            {
                this.Box = Box;
                this.name = name;

                Box.Text = ((double)GetPropValue(Projectile.settings, name)).ToString("G6");
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
                if (!Initializating)
                {
                    try
                    {
                        NewValue = Convert.ToDouble(ReplaceDecimalSeparator(Box.Text));
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
            public void Transfer_Or_Error()
            {
                if (InitializedWithSomethingNew)
                {
                    if (changed)
                    {
                        SetPropValue(Projectile.settings, name, NewValue);
                        // = NewValue;
                    }
                    else MessageBox.Show(error_message, "Warning: " + Box.Name);
                }
            }
            public void SetVisibility(bool value) { Box.Visible = value; }
        }
        private void button_Accept_Click(object sender, EventArgs e)
        {
            Projectile.settings.AdvancedPhysics = checkBox_Advanced_Flying_Physics.Checked;

            if (Projectile.settings.AdvancedPhysics)
                foreach (var item in Boxes) item.Transfer_Or_Error();

            Projectile.jp.WP.Reignite_wind_from_static_info();
            Scene.SS.TrajectoryPath.Reloader();
            this.Close();
        }

        void Process_Text_Changed(TextBox Boxik)
        {
            try
            {
                Boxes[Boxes.FindIndex(x => x.Get_Name().Contains(Boxik.Name))].TrySetNewValue();
            }
            catch (Exception) { }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox_Advanced_Flying_Physics_CheckedChanged(object sender, EventArgs e)
        {
            RegulatingAdvancedSectorVisilibility();
        }
        void RegulatingAdvancedSectorVisilibility() { foreach (var item in Boxes) item.SetVisibility(checkBox_Advanced_Flying_Physics.Checked); }

        

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
    }
}
