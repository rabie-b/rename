using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rename.Properties;

namespace rename
{
    public partial class AskToReplace : Form
    {
        public AskToReplace()
        {
            InitializeComponent();
        }

        public string AskTexts
        {
            //get { return label1.Text; }
            //set { label1.Text = value; }
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public bool AskResultS { get { return AskResult; } }
        public bool AskResult = false;

        private void AskToReplace_Load(object sender, EventArgs e)
        {
            Image image = Resources.info;
            pictureBox1.Image = image;
            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;
        }

        private void Rtn_Yes_Click(object sender, EventArgs e)
        {
            AskResult = true;
            Close();
        }

        private void Btn_No_Click(object sender, EventArgs e)
        {
            AskResult = false;
            Close();
        }
    }
}
