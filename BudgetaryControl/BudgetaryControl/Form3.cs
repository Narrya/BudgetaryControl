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
    public partial class Form3 : Form
    {
        public string helper;
        public string helper2;
        public int datachanged;

        public Form3()
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
                textBox10.ReadOnly = true;
                button1.Hide();
                button4.Hide();
                button5.Hide();
                button6.Hide();
                button7.Hide();
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                combobox();
            }
            else
            {
                bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
                combobox();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker3.Value.ToShortDateString();
            string expenditure = textBox1.Text + "." + textBox2.Text;
            if (expenditure == "." || textBox2.Text =="" || textBox1.Text == "")
            {
                string inform = "Correct expenditure";
                string text = "Correct";
                MessageBox.Show(inform, text, MessageBoxButtons.OK);
            }
            else
            {
                Global.updatedata("INSERT INTO EXPENDITUREdatabase(EXPENDITURE,DATE,NOTE) VALUES ('" + expenditure + "','" + date + "','" + textBox5.Text + "')");
                dataGridView1.AutoGenerateColumns = true;
                bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                combobox();
            }
        }

        private void Form3_1_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            this.ClientSize = new Size(w * 3 / 4, h * 42 / 100);
            this.DesktopLocation = new Point(0, h * 465 / 1000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 1;
            daterange(i);
            combobox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            daterange(i);
            combobox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker4.Value.ToShortDateString();
            string expenditure = textBox4.Text + "." + textBox3.Text;
            if (expenditure != ".")
            {
                if (textBox4.Text == "" || textBox3.Text == "")
                {
                    string inform = "Correct expenditure";
                    string text = "Correct";
                    MessageBox.Show(inform, text, MessageBoxButtons.OK);
                }
                else
                {
                    Global.updatedata("UPDATE EXPENDITUREdatabase SET EXPENDITURE = '" + expenditure + "' WHERE NOTE = '" + helper + "'");
                }
            }
            if (datachanged == 1)
            {
                Global.updatedata("UPDATE EXPENDITUREdatabase SET DATE = '" + date + "' WHERE NOTE = '" + helper + "'");
            }
            if (textBox10.Text != "")
            {
                Global.updatedata("UPDATE EXPENDITUREdatabase SET NOTE = '" + textBox10.Text + "' WHERE NOTE = '" + helper + "'");
            }
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
            comboBox11.SelectedIndex = 0;
            textBox3.Clear();
            textBox4.Clear();
            textBox10.Clear();
            datachanged = 0;
            combobox();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Global.updatedata("DELETE FROM EXPENDITUREdatabase WHERE NOTE = '" + helper2 + "'");
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = Global.updatedata("SELECT * FROM EXPENDITUREdatabase");
            combobox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            delete();
            combobox();
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void delete()
        {
            string inform = "Remove selected?";
            string text = "Warning";
            if (MessageBox.Show(inform, text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.bindingSource1.RemoveCurrent();
            combobox();
        }

        private void combobox()
        {
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();
            SqlCeCommand scq = new SqlCeCommand("SELECT * FROM EXPENDITUREdatabase", Global.polaczenie);
            SqlCeDataReader reader = scq.ExecuteReader();
            while (reader.Read())
            {
                comboBox11.Items.Add(reader["NOTE"]);
                comboBox12.Items.Add(reader["NOTE"]);
            }
            reader.Close();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper = comboBox11.Text;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            datachanged = 1;
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            helper2 = comboBox12.Text;
        }
    }
}
