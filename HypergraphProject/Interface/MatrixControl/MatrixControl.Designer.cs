namespace HypergraphProject.Interface
{
    partial class MatrixControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuCorner = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCornerAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCornerAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCornerAddColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCornerResize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCorner.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuCorner
            // 
            this.mnuCorner.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCornerAdd,
            this.mnuCornerResize});
            this.mnuCorner.Name = "mnuCorner";
            this.mnuCorner.Size = new System.Drawing.Size(153, 70);
            // 
            // mnuCornerAdd
            // 
            this.mnuCornerAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCornerAddRow,
            this.mnuCornerAddColumn});
            this.mnuCornerAdd.Name = "mnuCornerAdd";
            this.mnuCornerAdd.Size = new System.Drawing.Size(152, 22);
            this.mnuCornerAdd.Text = "Add";
            // 
            // mnuCornerAddRow
            // 
            this.mnuCornerAddRow.Name = "mnuCornerAddRow";
            this.mnuCornerAddRow.Size = new System.Drawing.Size(152, 22);
            this.mnuCornerAddRow.Text = "Row";
            this.mnuCornerAddRow.Click += new System.EventHandler(this.mnuCornerAddRow_Click);
            // 
            // mnuCornerAddColumn
            // 
            this.mnuCornerAddColumn.Name = "mnuCornerAddColumn";
            this.mnuCornerAddColumn.Size = new System.Drawing.Size(152, 22);
            this.mnuCornerAddColumn.Text = "Column";
            this.mnuCornerAddColumn.Click += new System.EventHandler(this.mnuCornerAddColumn_Click);
            // 
            // mnuCornerResize
            // 
            this.mnuCornerResize.Name = "mnuCornerResize";
            this.mnuCornerResize.Size = new System.Drawing.Size(152, 22);
            this.mnuCornerResize.Text = "Resize...";
            // 
            // MatrixControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "MatrixControl";
            this.mnuCorner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnuCorner;
        private System.Windows.Forms.ToolStripMenuItem mnuCornerAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuCornerAddRow;
        private System.Windows.Forms.ToolStripMenuItem mnuCornerAddColumn;
        private System.Windows.Forms.ToolStripMenuItem mnuCornerResize;
    }
}
