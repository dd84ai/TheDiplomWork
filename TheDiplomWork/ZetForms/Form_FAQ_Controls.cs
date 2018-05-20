using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace TheDiplomWork
{
    public partial class Form_FAQ_Controls : Form
    {
        public static string FilePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
            + "\\ZetForms\\FAQ_Controls.txt";
        public Form_FAQ_Controls()
        {
            InitializeComponent();

            using (StreamReader SR = new StreamReader(FilePath))
            {
                textBox1.Text = SR.ReadToEnd();
            }
            this.Show();
            this.Focus();
        }

        private void button_Return_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
