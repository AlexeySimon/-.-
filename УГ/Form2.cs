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
            //f.checkBox1_CheckStateChanged(sender, e);

        }
        Form1 f = new Form1();
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            f.Show();  
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
           string path = "C://ug.xml";
           if (!File.Exists(path))
           {
               //f.Visible = false;
               k.Show();
           }
           else k.Dispose();
        }

        private void linkLabel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://money.yandex.ru/embed/shop.xml?account=410012276643403&quickpay=shop&payment-type-choice=on&writer=seller&targets=%D0%9F%D0%BE%D0%B6%D0%B5%D1%80%D1%82%D0%B2%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5&default-sum=100&button-text=03&comment=on&hint=&successURL=");
        }

        private void Form2_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
