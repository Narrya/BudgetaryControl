using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BudgetaryControl
{
    public partial class Form1 : Form
    {
        public static int i = 0;
       
        public void NewWindow()
        {
            if (CheckWindow("Form2")) return;
            Form2 revenue = new Form2();
            if (CheckWindow("Form3")) return;
            Form3 expenditure = new Form3();
            if (CheckWindow("Form4")) return;
            Form4 balance = new Form4();
            if (CheckWindow("Form5")) return;
            Form5 date = new Form5();

            revenue.MdiParent = this;
            expenditure.MdiParent = this;
            balance.MdiParent = this;
            date.MdiParent = this;

            revenue.Show();
            expenditure.Show();
            balance.Show();
            date.Show();
        }

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string inform = "\t     Exit?";
            string text = "Warning";
            if (MessageBox.Show(inform, text, MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            i = 0;
            NewWindow();
        }
        
        private void updateBudgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            i = 1;
            NewWindow();
        }
        
        bool CheckWindow(string name)
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                if (Application.OpenForms[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
