using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Linq;



namespace WindowsFormsApplication2
{



    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public string request(string url, string postData)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(url);
            //WebProxy proxy = new WebProxy();
            //proxy.Address=new Uri("http://127.0.0.1/:8080");
            //request.Proxy = proxy;
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string path = "C://ug.xml";
                if (File.Exists(path))
                {
                    string xmlString = File.ReadAllText(path);
                    XmlReader reader = XmlReader.Create(new StringReader(xmlString));
                    // reader = XmlReader.Create(new StringReader(xmlString));
                    int i = 0;
                    while (!reader.EOF)
                    {
                        i++;
                        reader.ReadToFollowing("User");

                    }
                    reader = XmlReader.Create(new StringReader(xmlString));
                    reader.ReadToFollowing("User");
                    if (dataGridView1.RowCount < 2) dataGridView1.Rows.Add(i - 1);
                    i = 0;
                    while (!reader.EOF)
                    {
                        reader.ReadToFollowing("id");
                        dataGridView1.Rows[i].Cells[0].Value = reader.ReadElementContentAsString();
                        reader.ReadToFollowing("auth");
                        dataGridView1.Rows[i].Cells[1].Value = reader.ReadElementContentAsString();
                        reader.ReadToFollowing("active");
                        dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value = (reader.ReadElementContentAsString());
                        reader.ReadToFollowing("User");
                        i++;

                    }
                    reader = XmlReader.Create(new StringReader(xmlString));
                    reader.ReadToFollowing("steal");
                    numericUpDown1.Value = reader.ReadElementContentAsInt();
                    reader.ReadToFollowing("time");
                    numericUpDown2.Value = reader.ReadElementContentAsInt();
                    reader.ReadToFollowing("server");
                    comboBox1.Text = reader.ReadElementContentAsString();
                }
                else
                {
                    dataGridView1.Rows[0].Cells[0].Value = "89654978";
                    dataGridView1.Rows[0].Cells[1].Value = "74835e10955d6eff1039fa942c7f1ac4";
                }
                richTextBox1.Text = request("http://transend.esy.es/2.txt", "");

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        List<Racer> r = new List<Racer>();


        private void close(object sender, FormClosingEventArgs e)
        {
            XElement contacts = new XElement("Users");

            try
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {

                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "") break;

                    contacts.Add(new XElement("User",
                    new XElement("id", dataGridView1.Rows[i].Cells[0].Value),
                    new XElement("auth", dataGridView1.Rows[i].Cells[1].Value),
                    new XElement("active", dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - 1].Value)));

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            contacts.Add(new XElement("steal", numericUpDown1.Value.ToString()));
            contacts.Add(new XElement("time", numericUpDown2.Value.ToString()));
            contacts.Add(new XElement("server", comboBox1.Text));
            contacts.Save("C:\\ug.xml");
            e.Cancel = true;
            Visible = false;

        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Active"].Value = true;
        }

        public void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {


                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    try
                    {

                        if (dataGridView1.Rows[i].Cells["Active"].Value.ToString() != "false")
                        {
                            r.Add(new Racer(Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString()),
                            dataGridView1.Rows[i].Cells[1].Value.ToString(), comboBox1.Text));
                            r[r.Count - 1].Unlim = checkBox2.Checked;
                            r[r.Count - 1].Start((int)numericUpDown2.Value, (int)numericUpDown1.Value);
                            dataGridView1.Rows[i].Cells["Silver"].Value = r[r.Count - 1].silver;
                            dataGridView1.Rows[i].Cells["Gold"].Value = r[r.Count - 1].gold;
                            dataGridView1.Rows[i].Cells["Fuel"].Value = r[r.Count - 1].fuel;
                            numericUpDown1.Value = r[r.Count - 1].startId;
                        }
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 0; i > r.Count - 1; i++)
                {
                    r[i].Stop();
                }
                r.Clear();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
    public class Racer
    {
        public int id;
        public string auth;
        public int Car;
        public string Host;
        public int stCount = 1;
        public string gold;
        public string silver;
        public string fuel;
        public bool Unlim=false;
        public void toLabel(string respSrv)
        {
            XmlReader reader = XmlReader.Create(new StringReader(respSrv));
            //XmlAttribute
            reader.ReadToFollowing("user");
            reader.MoveToAttribute("gold");
            gold = reader.Value;
            reader.MoveToAttribute("money");
            silver = reader.Value;
            reader.MoveToAttribute("fuel");
            fuel = reader.Value;

        }
        public int startId;
        public List<int> Opponent = new List<int>();
        private void SetOpp() //противники
        {
            string s = "";
            for (int i = startId; i < startId + 7000; i++)
                s += i.ToString() + ",";
            startId += 7000;
            s += "9";
            string post = string.Format("ids={0}&auth={1}&random=0.3843563939444721", s, auth);
            List<int> z = new List<int>();
            z = parse(
            request(Host + "init?user_id=" + id, post));
            if (z.Count > 1) Opponent.AddRange(z);
            if (Opponent.Count < 30 * stCount) SetOpp();
        }

        public void steal()
        {

            string lab = "";
            Opponent.Clear();
            SetOpp();
            for (int i = 1; i < Opponent.Count - 1; i++)
            {
                try
                {
                    //Thread t = new Thread(()=>
                    lab = request(Host + "steal?user_id=" + id,
                    string.Format("auth={0}&friend_id={1}&random=0.36565781058743596", auth, Opponent[i])); //);
                    //t.Start();
                    toLabel(lab);
                }
                catch { }
                if (lab.Contains("steal fuel='0' money='0' resource='1' exp='0' gold='0'")) break;
            }
            if (!lab.Contains("steal fuel='0' money='0' resource='1' exp='0' gold='0'"))
            {
                SetOpp();
                steal();
            }
            toLabel(lab);
            date = DateTime.Now;
        }



        public void Race()
        {
            if (Opponent.Count < 10) SetOpp();
            string lab = "";
            int k = 0;
            while (true)
            {
                k++;
                try
                {
                    lab = request(Host + "race?user_id=" + id,
                    string.Format("auth={0}&friend_id={1}&random=0.36565781058743596", auth, Opponent[k]));
                }
                catch
                {
                    break;
                }

            }
            toLabel(lab);

        }

        public Racer(int Id, string Auth, string ServURI)
        {
            if (Id <= 0 || Auth == "") throw new Exception("Неверные параметры");
            id = Id;
            auth = Auth;
            Host = ServURI; //"http://188.93.19.132/";
        }
        private List<int> parse(string xmlString)
        {
            XmlReader reader = XmlReader.Create(new StringReader(xmlString));
            List<int> V = new List<int>();
            reader = XmlReader.Create(new StringReader(xmlString));
            reader.ReadToFollowing("user");
            while (!reader.EOF)
            {
                reader.MoveToAttribute(7); //id
                string genre = reader.Value;
                reader.MoveToAttribute(14); //robbed
                if (reader.Value == "False")
                {
                    reader.MoveToAttribute("visit_day");
                    if (Unlim)
                    {
                        if (Convert.ToInt32(reader.Value) <= 735610)
                            if (Convert.ToInt32(reader.Value)!=0)
                            V.Add(Convert.ToInt32(genre));
                    }
                    else V.Add(Convert.ToInt32(genre));
                }
                reader.ReadToFollowing("user");
            }
            return V;
        }
        public void Start(int r, int z) //r минуты
		{
			t.Tick += (object p, EventArgs i) => tic();
			t.Interval = r * 60000;
			startId = z;
			t.Start();
			tic();

		}
        public void Stop() //r часы
        {
            t.Stop();
        }

        private DateTime date;
        private void tic()
        {

            if (date != DateTime.Now) steal();
            Race();

        }
        private System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public string request(string url, string postData)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(url);
            //WebProxy proxy = new WebProxy();
            //proxy.Address=new Uri("http://127.0.0.1/:8080");
            //request.Proxy = proxy;
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }

}