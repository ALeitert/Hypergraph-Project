namespace HypergraphProject
{
    partial class NewHypergraphDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblEdges = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblCardinality = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numVertices = new System.Windows.Forms.NumericUpDown();
            this.numEdges = new System.Windows.Forms.NumericUpDown();
            this.numCard = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numVertices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCard)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Vertices";
            // 
            // lblEdges
            // 
            this.lblEdges.AutoSize = true;
            this.lblEdges.Location = new System.Drawing.Point(12, 68);
            this.lblEdges.Name = "lblEdges";
            this.lblEdges.Size = new System.Drawing.Size(37, 13);
            this.lblEdges.TabIndex = 4;
            this.lblEdges.Text = "Edges";
            // 
            // cboType
            // 
            this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(99, 12);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(109, 21);
            this.cboType.TabIndex = 5;
            // 
            // lblCardinality
            // 
            this.lblCardinality.AutoSize = true;
            this.lblCardinality.Location = new System.Drawing.Point(12, 94);
            this.lblCardinality.Name = "lblCardinality";
            this.lblCardinality.Size = new System.Drawing.Size(81, 13);
            this.lblCardinality.TabIndex = 6;
            this.lblCardinality.Text = "Max. Cardinality";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "Type";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(68, 133);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(67, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(141, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            // 
            // numVertices
            // 
            this.numVertices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numVertices.Location = new System.Drawing.Point(99, 40);
            this.numVertices.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numVertices.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numVertices.Name = "numVertices";
            this.numVertices.Size = new System.Drawing.Size(109, 20);
            this.numVertices.TabIndex = 8;
            this.numVertices.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numEdges
            // 
            this.numEdges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numEdges.Location = new System.Drawing.Point(99, 66);
            this.numEdges.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numEdges.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEdges.Name = "numEdges";
            this.numEdges.Size = new System.Drawing.Size(109, 20);
            this.numEdges.TabIndex = 9;
            this.numEdges.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numCard
            // 
            this.numCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numCard.Location = new System.Drawing.Point(99, 92);
            this.numCard.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numCard.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCard.Name = "numCard";
            this.numCard.Size = new System.Drawing.Size(109, 20);
            this.numCard.TabIndex = 10;
            this.numCard.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // NewHypergraphDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(220, 168);
            this.Controls.Add(this.numCard);
            this.Controls.Add(this.numEdges);
            this.Controls.Add(this.numVertices);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblCardinality);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblEdges);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewHypergraphDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Hypergraph";
            ((System.ComponentModel.ISupportInitialize)(this.numVertices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEdges;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblCardinality;
        private System.Windows.Forms.Label lblType;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown numVertices;
        private System.Windows.Forms.NumericUpDown numEdges;
        private System.Windows.Forms.NumericUpDown numCard;
    }
}