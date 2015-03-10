using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HypergraphProject.Interface
{
    public partial class ResizeMatrixDialog : Form
    {
        public ResizeMatrixDialog()
        {
            InitializeComponent();
        }

        public ResizeMatrixDialog(int vertices, int edges)
        {
            InitializeComponent();

            numVertices.Value = vertices;
            numVertices.Minimum = vertices;

            numEdges.Value = edges;
            numEdges.Minimum = edges;
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

    }
}
