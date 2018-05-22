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
        Wrapped_Box StaticSet = new Wrapped_Box(StaticSettings.S);
        public Form_ProjectileSettings()
        {
            InitializeComponent();

            ProjectileSet.Add(textBox_mass, nameof(Projectile.settings.mass), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_area, nameof(Projectile.settings.area), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_cd, nameof(Projectile.settings.cd), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_density, nameof(Projectile.settings.density), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_windVx, nameof(Projectile.settings.windVx), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_WindVz, nameof(Projectile.settings.windVy), Wrapped_Box.Types.doubler);
            ProjectileSet.Add(textBox_TimeSpeed, nameof(Projectile.settings.timespeed), Wrapped_Box.Types.doubler);

            StaticSet.Add(textBox_rangeofview, nameof(StaticSettings.S.RangeOfView), Wrapped_Box.Types.integer);
            StaticSet.Add(textBox_angleofview, nameof(StaticSettings.S.AngleOfView), Wrapped_Box.Types.floater);
            StaticSet.Add(textBox_angleblockcuter, nameof(StaticSettings.S.PointOfViewCoefOfDifference), Wrapped_Box.Types.floater);

            checkBox1_Trajectory.Checked = StaticSettings.S.TrajectoryIsVisilible;
            checkBox_HelpfulInfo.Checked = StaticSettings.S.HelpInfoForPlayer;
            checkBox_compass.Checked = StaticSettings.S.Compass;
            //

            checkBox_Advanced_Flying_Physics.Checked = Projectile.settings.AdvancedPhysics;
            ProjectileSet.RegulatingAdvancedSectorVisilibility();
            this.Show();
            Initializating = false;
        }

        
        private void button_Accept_Click(object sender, EventArgs e)
        {
            if (Projectile.settings.AdvancedPhysics)
                ProjectileSet.Wrapped_Transfer_or_Die();

            StaticSet.Wrapped_Transfer_or_Die();

            StaticSettings.S.TrajectoryIsVisilible = checkBox1_Trajectory.Checked;
            StaticSettings.S.HelpInfoForPlayer = checkBox_HelpfulInfo.Checked;
            StaticSettings.S.Compass = checkBox_compass.Checked;

            Projectile.jp.WP.Reignite_wind_from_static_info();
            Scene.SS.TrajectoryPath.Reloader();
            this.Close();
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

        private void textBox_ProjectileSet_TextChanged(object sender, EventArgs e)
        {
            ProjectileSet.Process_Text_Changed((TextBox)sender);
        }
        private void textBox_StaticSet_TextChanged(object sender, EventArgs e)
        {
            StaticSet.Process_Text_Changed((TextBox)sender);
        }
    }
}













