using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HypergraphProject
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkBitbucket_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://bitbucket.org/Seneferu/hyergraph-project");
        }
    }
}
