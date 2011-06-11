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
    public partial class Form3_11 : Form
    {
        public string helper;

        public Form3_11()
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
                bindingSource1.DataSource = Global.viewdata("SELECT * FROM EXPENDITUREdatabase");
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                button2.Hide();
                button3.Hide();
                button4.Hide();
                button6.Hide();
                button7.Hide();
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                combobox();
            }
            else
            {
                bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
                combobox();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            this.ClientSize = new Size(w * 3 / 4, h * 42 / 100);
            this.DesktopLocation = new Point(0, h * 465 / 1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 1;
            daterange(i);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            daterange(i);
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
            result = Global.viewdata("SELECT * FROM EXPENDITUREdatabase WHERE ([DATE] BETWEEN '" + from + "' AND '" + to + "')");
            dataGridView1.DataSource = result;
            combobox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string date = (textBox9.Text + "-" + textBox8.Text + "-" + textBox7.Text);
            string revenue = textBox10.Text;
            revenue.Replace(".", ",");
            Global.updatedata("INSERT INTO EXPENDITUREdatabase(EXPENDITURE,DATE,NOTE) VALUES ('" + revenue + "','" + date + "','" + textBox6.Text + "')");
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            combobox();
        }

        private void combobox()
        {
            comboBox3.Items.Clear();
            SqlCeCommand scq = new SqlCeCommand("SELECT * FROM EXPENDITUREdatabase", Global.polaczenie);
            SqlCeDataReader reader = scq.ExecuteReader();
            while (reader.Read())
            {
                comboBox3.Items.Add(reader["NOTE"]);
                comboBox4.Items.Add(reader["NOTE"]);
            }
            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            delete();
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

        private void button7_Click(object sender, EventArgs e)
        {
            string date = (textBox2.Text + "-" + textBox3.Text + "-" + textBox4.Text);
            string revenue = textBox1.Text;
            revenue.Replace(".", ",");
            Global.updatedata("UPDATE EXPENDITUREdatabase SET EXPENDITURE = '" + revenue + "' WHERE NOTE = '" + helper + "'");
            Global.updatedata("UPDATE EXPENDITUREdatabase SET DATE = '" + date + "' WHERE NOTE = '" + helper + "'");
            Global.updatedata("UPDATE EXPENDITUREdatabase SET NOTE = '" + textBox5.Text + "' WHERE NOTE = '" + helper + "'");
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
            comboBox3.SelectedIndex = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            combobox();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            delete();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper = comboBox3.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper = comboBox4.Text;
        }
    }
}
