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
using System.Runtime.CompilerServices;

namespace Gear
{
    public partial class Gear : Form
    {
        public Gear()
        {
            InitializeComponent();
        }
        public static List<string[]> csvDataList;
        public static int atm1;
        public static int atm2;
        public static double chamfer1;
        public static double chamfer2;

        public static bool retesz1;
        public static double retesz1_Szelesseg;
        public static double retesz1_Hossz;
        public static double retesz1_Magassag;

        public static bool retesz2;
        public static double retesz2_Szelesseg;
        public static double retesz2_Hossz;
        public static double retesz2_Magassag;

        public static double alapkor;
        public static double fejkor;
        public static double fogmagassag;
        public static double fogszam;
        public static double kihuzas;


        class dimensionsOfInserts
        {
            public int StartValue { get; set; }
            public int EndValue { get; set; }
            public double StepDistance { get; set; }
            public double width { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Start();
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
            global::Gear.Gear.csvDataList = csvDataList;
            comboBox1.SelectedIndex = 20;
            comboBox2.SelectedIndex = 20;
      comboBox2.Visible = true;

            comboBox3.Items.Add(0.75);
            comboBox3.Items.Add(1.0);
            comboBox3.Items.Add(1.25);
            comboBox3.Items.Add(1.5);
            comboBox3.Items.Add(1.75);
            comboBox3.Items.Add(2.0);

            comboBox4.Items.Add(0.75);
            comboBox4.Items.Add(1.0);
            comboBox4.Items.Add(1.25);
            comboBox4.Items.Add(1.5);
            comboBox4.Items.Add(1.75);
            comboBox4.Items.Add(2.0);

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            //Start();
           // this.Close();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void Start()
        {
            double d1 = double.Parse(comboBox1.SelectedItem.ToString());
            double d2 = double.Parse(comboBox1.SelectedItem.ToString())+5;
            double d3 = double.Parse(comboBox1.SelectedItem.ToString());

            double vall1 = double.Parse(textBox6.Text);
            double vall2 = double.Parse(textBox5.Text);
            double vall3 = double.Parse(textBox4.Text);

            global::Gear.Gear.atm1 = comboBox1.SelectedIndex;
            global::Gear.Gear.atm2 = comboBox2.SelectedIndex;


            global::Gear.Gear.chamfer1 = double.Parse(comboBox3.SelectedItem.ToString());
            global::Gear.Gear.chamfer2 = double.Parse(comboBox4.SelectedItem.ToString());

            global::Gear.Gear.retesz1_Hossz = double.Parse(textBox1.Text.ToString());

            global::Gear.Gear.retesz2_Hossz = double.Parse(textBox8.Text.ToString());

            global::Gear.Gear.alapkor = double.Parse(textBox13.Text.ToString());
            global::Gear.Gear.fejkor = double.Parse(textBox12.Text.ToString());
            global::Gear.Gear.fogmagassag = double.Parse(textBox11.Text.ToString());
            global::Gear.Gear.fogszam = double.Parse(textBox14.Text.ToString());
            global::Gear.Gear.kihuzas = double.Parse(textBox15.Text.ToString());

            List<dimensionsOfInserts> eloszlasok = new List<dimensionsOfInserts>
        {
            new dimensionsOfInserts { StartValue = 6, EndValue = 8, StepDistance = 1.2, width = 2.0},
            new dimensionsOfInserts { StartValue = 8, EndValue = 10, StepDistance = 1.8, width = 3.0 },
            new dimensionsOfInserts { StartValue = 10, EndValue = 12, StepDistance = 2.5, width = 4.0 },
            new dimensionsOfInserts { StartValue = 12, EndValue = 17, StepDistance = 3.0 , width = 5.0},
            new dimensionsOfInserts { StartValue = 17, EndValue = 22, StepDistance = 3.5 , width = 6.0},
            new dimensionsOfInserts { StartValue = 22, EndValue = 30, StepDistance =  4.0 , width = 8.0},
            new dimensionsOfInserts { StartValue = 30, EndValue = 38, StepDistance = 5.0 , width = 10.0},
            new dimensionsOfInserts { StartValue = 38, EndValue = 44, StepDistance = 5.0 , width = 12.0},
            new dimensionsOfInserts { StartValue = 44, EndValue = 50, StepDistance = 5.5 , width = 14.0},
            new dimensionsOfInserts { StartValue = 50, EndValue = 58, StepDistance = 6.0 , width = 16.0},
            new dimensionsOfInserts { StartValue = 58, EndValue = 65, StepDistance = 7.0 , width = 18.0},
            new dimensionsOfInserts { StartValue = 65, EndValue = 75, StepDistance = 7.5 , width = 20.0},
            new dimensionsOfInserts { StartValue = 75, EndValue = 85, StepDistance = 9.0 , width = 22.0},
            new dimensionsOfInserts { StartValue = 85, EndValue = 95, StepDistance = 9.0 , width = 25.0},
            new dimensionsOfInserts { StartValue = 95, EndValue = 110, StepDistance = 10.0 , width = 28.0},
            // Ide add hozzá a további eloszlásokat
        };

            foreach (var eloszlas in eloszlasok)
            {
                for (double ertek = eloszlas.StartValue; ertek <= eloszlas.EndValue; ertek += eloszlas.StepDistance)
                {
                    if (d1 >= eloszlas.StartValue && d1 < eloszlas.EndValue)
                    {
                        global::Gear.Gear.retesz1_Magassag = eloszlas.StepDistance;
                        global::Gear.Gear.retesz1_Szelesseg = eloszlas.width;
                    }
                    if (d3 >= eloszlas.StartValue && d3 < eloszlas.EndValue)
                    {
                        global::Gear.Gear.retesz2_Magassag = eloszlas.StepDistance;
                        global::Gear.Gear.retesz2_Szelesseg = eloszlas.width;
                    }
                }
            }
            global::Gear.Gear.retesz1 = checkBox1.Checked;
            global::Gear.Gear.retesz2 = checkBox2.Checked;


            SE_DT DT = new SE_DT();
            DT.application = (SolidEdgeFramework.Application)
                Marshal.GetActiveObject("SolidEdge.Application");
            // Get a reference to the documents collection
            DT.documents = DT.application.Documents;
            // Create a new part document
            DT.part = (SolidEdgePart.PartDocument)
            DT.documents.Add("SolidEdge.PartDocument", Missing.Value);
            DT.Shaft(d1, vall1, d2, vall2, d3, vall3);
            DT.Fogaskerek(global::Gear.Gear.kihuzas, vall1 + vall2 / 2 - global::Gear.Gear.kihuzas / 2);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
