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
    public partial class Form4 : Form
    {
        private void sumrev()
        {          
            Global.Polacz();
            double helper1 = 0;
            double helper2 = 0;
            string from;
            string to;
            from = dateTimePicker1.Value.ToShortDateString();
            to = dateTimePicker2.Value.ToShortDateString();
            SqlCeDataReader reader1 = Global.viewdata("SELECT (REVENUE) FROM REVENUEdatabase WHERE ([DATE] BETWEEN '" + from + "' AND '" + to + "')");
            SqlCeDataReader reader2 = Global.viewdata("SELECT (EXPENDITURE) FROM EXPENDITUREdatabase WHERE ([DATE] BETWEEN '" + from + "' AND '" + to + "')");
            DataTable tabela = new DataTable();
            
            for (int i = 0; i < reader1.FieldCount; i++)
            {
                DataColumn column = new DataColumn(reader1.GetName(i));
                tabela.Columns.Add(column);
            }

            while (reader1.Read())
            {
                DataRow row = tabela.NewRow();
                for (int i = 0; i < reader1.FieldCount; i++)
                {
                    row[i] = reader1.GetValue(i);
                    helper1 = Convert.ToDouble(row[i]) + helper1;
                }
                tabela.Rows.Add(row);
                this.label1.Text = (Convert.ToString(helper1));
            }
            
            for (int i = 0; i < reader2.FieldCount; i++)
            {
                DataColumn column = new DataColumn(reader2.GetName(i));
                tabela.Columns.Add(column);
            }

            while (reader2.Read())
            {
                DataRow row = tabela.NewRow();
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row[i] = reader2.GetValue(i);
                    helper2 = Convert.ToDouble(row[i]) + helper2;
                }
                tabela.Rows.Add(row);
                this.label2.Text = (Convert.ToString(helper2));
            }

            this.label3.Text = Convert.ToString((Convert.ToDouble(helper1)) - (Convert.ToDouble(helper2)));
        }
        
        public Form4()
        {
            InitializeComponent();   
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            this.ClientSize = new Size(w/3, h/3);
            this.DesktopLocation = new Point(w * 763 / 1000, h* 587/1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sumrev();
        }
    }
}
