using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheDiplomWork
{
    public partial class Form_ProjectileSettings : Form
    {
        public static bool Initializing = true;
        public Form_ProjectileSettings()
        {
            InitializeComponent();

            this.Show();
        }
        List<Boxed> Boxes = new List<Boxed>();
        //void Process_All()
        //{
        //    Boxes.Add(new Boxed(textBox_mass, ref Projectile.Settings.mass));
        //    Boxes.Add(new Boxed(textBox_area, ref Projectile.Settings.area));
        //    Boxes.Add(new Boxed(textBox_cd, ref Projectile.Settings.cd));
        //    Boxes.Add(new Boxed(textBox_density, ref Projectile.Settings.density));
        //    Boxes.Add(new Boxed(textBox_windVx, ref Projectile.Settings.windVx));
        //    Boxes.Add(new Boxed(textBox_WindVz, ref Projectile.Settings.windVy));
        //}
        public class Boxed
        {
            TextBox Box;
            double value;
            public string Get_Name() { return Box.Name; }
            public Boxed(TextBox Box, ref double value)
            {
                this.Box = Box;
                this.value = value;

                Box.Text = value.ToString("G6");
            }
            //void Process_Text_Box()
            //{
            //        try
            //        {
            //            double test = Convert.ToDouble(Box.Text);
            //            value = test;
            //        }
            //        catch (Exception exc)
            //        {
            //            //MessageBox.Show(exc.Message, "Warning:" + Box.Name);
            //        }

            //}
            double NewValue = 0;
            bool changed = false;
            string error_message = "";
            public void TrySetNewValue()
            {
                try
                {
                    NewValue = Convert.ToDouble(Box.Text);
                    changed = true;
                }
                catch (Exception exc)
                {
                    changed = false;
                    error_message = exc.Message;
                }
            }

            public void Transfer_Or_Error()
            {
                if (changed) value = NewValue;
                else MessageBox.Show(error_message, "Warning: " + Box.Name);
            }

        }
        private void button_Accept_Click(object sender, EventArgs e)
        {
            foreach (var item in Boxes) item.Transfer_Or_Error();
        }

        private void textBox_mass_TextChanged(object sender, EventArgs e)
        {
            Process_Text_Changed((TextBox)sender);
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
    }
}
