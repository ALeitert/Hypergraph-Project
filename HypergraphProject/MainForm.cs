﻿using System;
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

        private void btnDual_Click(object sender, EventArgs e)
        {
            matrixControl.Dual();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSaveMatrix.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                matrixControl.WriteToFile(dlgSaveMatrix.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (
                        "Unable to save matrix.\n\n" + e.GetType().Name + "\n" + ex.Message,
                        "Unable to save matrix.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dlgOpenMatrix.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                matrixControl.ReadFromFile(dlgOpenMatrix.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show
                    (
                        "Unable to load matrix.\n\n" + e.GetType().Name + "\n" + ex.Message,
                        "Unable to load matrix.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
            }
        }

    }
}
