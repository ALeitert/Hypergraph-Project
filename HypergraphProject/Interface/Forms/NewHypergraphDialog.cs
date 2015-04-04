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
    public partial class NewHypergraphDialog : Form
    {

        public NewHypergraphDialog()
        {
            InitializeComponent();

            cboType.Items.Add(HypergraphType.Arbitrary);
            cboType.Items.Add(HypergraphType.Acyclic);
            cboType.Items.Add(HypergraphType.Hypertree);

            cboType.SelectedItem = HypergraphType.Arbitrary;

        }

        public HypergraphType HypergraphType
        {
            get
            {
                return (HypergraphType)cboType.SelectedItem;
            }
        }

        public int Vertices
        {
            get
            {
                return (int)numVertices.Value;
            }
        }

        public int Edges
        {
            get
            {
                return (int)numEdges.Value;
            }
        }

        public int MaxCardinality
        {
            get
            {
                return (int)numCard.Value;
            }
        }

    }
}
