using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 缩略图生成程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void Create()
        {
            string img = "http://attach.cuuju.com/fm_file/club/20151223/2015122305053238449.jpg";

            ImageHelper.BuildSubImg(img, 200, 200, "sub1", "Cut");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Create();
        }
    }
}
