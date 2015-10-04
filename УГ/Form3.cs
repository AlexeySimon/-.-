using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {


                XElement contacts = new XElement("Users");


          
                try
                { 
                    contacts.Add(new XElement("User",
                    new XElement("id", textBox1.Text),
                    new XElement("auth", textBox2.Text),
                    new XElement("active")));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                contacts.Add(new XElement("steal", new Random().Next(1, 256549787)));
                contacts.Add(new XElement("time", 60));
                contacts.Save("C:\\ug.xml");
                //f.show;
                Close();
            

        }
    }
}