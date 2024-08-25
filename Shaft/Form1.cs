using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidEdgeConstants;
using System.Runtime.InteropServices;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Part_Creator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<string[]> csvDataList;
        public static int atm1;
        public static int atm2;
        public static double chamfer1;
        public static double chamfer2;

        private void button1_Click(object sender, EventArgs e)
        {
            double d1 = double.Parse(comboBox1.SelectedItem.ToString());
            double d2 = double.Parse(textBox2.Text);
            double d3 = double.Parse(comboBox2.SelectedItem.ToString());

            double vall1 = double.Parse(textBox6.Text);
            double vall2 = double.Parse(textBox5.Text);
            double vall3 = double.Parse(textBox4.Text);

            Part_Creator.Form1.atm1 = comboBox1.SelectedIndex;
            Part_Creator.Form1.atm2 = comboBox2.SelectedIndex;

            //string[] values = textBox1.Text.Split('.'); // Válassza szét a sorokat vesszők mentén
            //string[] values2 = textBox3.Text.Split('.'); // Válassza szét a sorokat vesszők mentén

            Part_Creator.Form1.chamfer1 = double.Parse(comboBox3.SelectedItem.ToString());
            Part_Creator.Form1.chamfer2 = double.Parse(comboBox4.SelectedItem.ToString());
            SE_DT DT = new SE_DT();
            DT.application = (SolidEdgeFramework.Application)
                Marshal.GetActiveObject("SolidEdge.Application");
            // Get a reference to the documents collection
            DT.documents = DT.application.Documents;
            // Create a new part document
            DT.part = (SolidEdgePart.PartDocument)
            DT.documents.Add("SolidEdge.PartDocument", Missing.Value);
            DT.Shaft(d1, vall1, d2, vall2, d3, vall3);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string csvFilePath = "seger horony.csv"; // Az olvasni kívánt CSV fájl elérési útja

            List<string[]> csvDataList = new List<string[]>(); // Listába olvassuk be a CSV adatokat
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';'); // Válassza szét a sorokat vesszők mentén
                    comboBox1.Items.Add(values[0]);
                    comboBox2.Items.Add(values[0]);
                    csvDataList.Add(values); // Adjuk hozzá az értékeket a listához
                }                
            }
            Part_Creator.Form1.csvDataList = csvDataList;
            comboBox1.SelectedIndex = 17;
            comboBox2.SelectedIndex = 18;


            comboBox3.Items.Add(1.0);
            comboBox3.Items.Add(1.25);
            comboBox3.Items.Add(1.5);
            comboBox3.Items.Add(1.75);
            comboBox3.Items.Add(2.0);

            comboBox4.Items.Add(1.0);
            comboBox4.Items.Add(1.25);
            comboBox4.Items.Add(1.5);
            comboBox4.Items.Add(1.75);
            comboBox4.Items.Add(2.0);

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }
    }
}
