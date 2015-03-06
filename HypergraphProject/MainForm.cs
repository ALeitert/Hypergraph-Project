using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HypergraphProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            matrixControl.Dimension = new Size(35, 25);

            Random rng = new Random();

            for (int i = 0; i < 64; i++)
            {
                matrixControl[rng.Next(35), rng.Next(25)] = true;
            }

        }

        private void btnStartEdit_Click(object sender, EventArgs e)
        {
            matrixControl.StartEditing();

            toolMain.SuspendLayout();
            btnStartEdit.Visible = false;
            btnStopEdit.Visible = true;
            toolMain.ResumeLayout();

        }

        private void mnuStopEditExecute_Click(object sender, EventArgs e)
        {
            matrixControl.ExecuteEditing();

            toolMain.SuspendLayout();
            btnStartEdit.Visible = true;
            btnStopEdit.Visible = false;
            toolMain.ResumeLayout();

        }

        private void mnuStopEditCancel_Click(object sender, EventArgs e)
        {
            matrixControl.CancelEditing();

            toolMain.SuspendLayout();
            btnStartEdit.Visible = true;
            btnStopEdit.Visible = false;
            toolMain.ResumeLayout();

        }

    }
}
