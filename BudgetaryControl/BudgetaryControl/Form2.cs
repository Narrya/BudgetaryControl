using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace BudgetaryControl
{
    public partial class Form2 : Form
    {     
        public string helper;
        public int datachanged;

        public Form2()
        {
            InitializeComponent();
            if (!Global.Polacz())
            {
                MessageBox.Show("Nie udało się nawiązać połączenia");
                this.Close();
                return;
            }
            dataGridView1.AutoGenerateColumns = true;
            if (Form1.i == 0)
            {
                bindingSource1.DataSource = Global.viewdata("SELECT * FROM REVENUEdatabase");
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button6.Hide();
                button7.Hide();
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                combobox();
            }
            else
            {
                bindingSource1.DataSource = Global.updatedata("SELECT * FROM REVENUEdatabase");
                combobox();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            this.ClientSize = new Size(w * 3 / 4, h * 42 / 100);
            this.DesktopLocation = new Point(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 1;
            daterange(i);
            combobox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            daterange(i);
            combobox();
        }

        private void daterange(int i)
        {
            SqlCeResultSet result;
            string from;
            string to;
            if (i == 1)
            {
                from = dateTimePicker1.Value.ToShortDateString();
                to = dateTimePicker2.Value.ToShortDateString();
            }
            else
            {
                from = "1900-01-01";
                to = "2100-01-01";
            }
            result = Global.viewdata("SELECT * FROM REVENUEdatabase WHERE ([DATE] BETWEEN '" + from + "' AND '" + to + "')");
            dataGridView1.DataSource = result;
            combobox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker3.Value.ToShortDateString();
            string revenue = textBox8.Text + "." + textBox7.Text;
            if (revenue == "." || textBox8.Text == "" || textBox7.Text == "")
            {
                string inform = "Correct revenue";
                string text = "Correct";
                MessageBox.Show(inform, text, MessageBoxButtons.OK);
            }
            else
            {
                Global.updatedata("INSERT INTO REVENUEdatabase(REVENUE,DATE,NOTE) VALUES ('" + revenue + "','" + date + "','" + textBox6.Text + "')");
                dataGridView1.AutoGenerateColumns = true;
                bindingSource1.DataSource = Global.updatedata("SELECT * FROM REVENUEdatabase");
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                combobox();
            }
        }

        private void combobox()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlCeCommand scq = new SqlCeCommand("SELECT * FROM REVENUEdatabase", Global.polaczenie);
            SqlCeDataReader reader = scq.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["NOTE"]);
                comboBox2.Items.Add(reader["NOTE"]);
            }
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper = comboBox1.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker4.Value.ToShortDateString();
            string revenue = textBox2.Text + "." + textBox1.Text;
            if (revenue != ".")
            {
                if (textBox2.Text == "" || textBox1.Text == "")
                {
                    string inform = "Correct revenue";
                    string text = "Correct";
                    MessageBox.Show(inform, text, MessageBoxButtons.OK);
                }
                else
                {
                    Global.updatedata("UPDATE REVENUEdatabase SET REVENUE = '" + revenue + "' WHERE NOTE = '" + helper + "'");
                }
            }
            if(datachanged == 1)
            {
                Global.updatedata("UPDATE REVENUEdatabase SET DATE = '" + date + "' WHERE NOTE = '" + helper + "'");
            }
            if (textBox5.Text != "")
            {
                Global.updatedata("UPDATE REVENUEdatabase SET NOTE = '" + textBox5.Text + "' WHERE NOTE = '" + helper + "'");
            }            
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM REVENUEdatabase");
            comboBox1.SelectedIndex = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            datachanged = 0;
            combobox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Global.updatedata("DELETE FROM REVENUEdatabase WHERE NOTE = '" + comboBox2.Text + "'");
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM REVENUEdatabase");
            combobox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete();
            combobox();
        }
                    
        private void delete()
        {
            string inform = "Remove selected?";
            string text = "Warning";
            if (MessageBox.Show(inform, text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.bindingSource1.RemoveCurrent();
            combobox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string inform = "Remove all?";
            string inform2 = "Remove all definitely?";
            string text = "Warning";
            if (MessageBox.Show(inform, text, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MessageBox.Show(inform2, text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    while (bindingSource1.Current != null)
                    {
                        this.bindingSource1.RemoveCurrent();
                    }
                }
            }
            combobox();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            datachanged = 1;
        }
    }
}
