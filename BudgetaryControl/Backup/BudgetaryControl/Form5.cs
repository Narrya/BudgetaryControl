using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;


namespace BudgetaryControl
{
    public partial class Form5 : Form
    {
        public DateTime from;
        public DateTime to;
        public string file = "";

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            this.ClientSize = new Size(w / 2, h);
            this.DesktopLocation = new Point(w * 763/1000, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //richTextBox1.Text = Convert.ToString("from:\t" + dateTimePicker1.Value + "\n" + "to\t" + dateTimePicker2.Value);
        //    //DateTime from = dateTimePicker1.Value;
        //    //DateTime to = dateTimePicker2.Value;
        //    //Form2 filtr = new Form2();
        //    //filtr.rangerevenue(from,to);
        //}

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "(*.txt)|*.txt";

            open.ShowDialog();
            if (open.FileName != "")
            {
                file = open.FileName;
                StreamReader sr = new StreamReader(file);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Plik tekstowy (*.txt)|*.txt";
            save.ShowDialog();
            if (save.FileName != "")
            {
                file = save.FileName;
                StreamWriter sw = new StreamWriter(file);
                sw.Write(richTextBox1.Text);
                sw.Close();

            }
        }
    }
}
