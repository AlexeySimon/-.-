using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            f.Form1_Load(sender, e);
           // Update;
            f.checkBox1.Checked = checkBox1.Checked;
            f.checkBox1_CheckStateChanged(sender, e);

        }
        Form1 f = new Form1();
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            f.Show();  
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
           string path = "C://ug.xml";
           Form3 k = new Form3();
           if (!File.Exists(path))
           {
               //f.Visible = false;
               k.Show();
           }
           else k.Dispose();
        }
    }
}
