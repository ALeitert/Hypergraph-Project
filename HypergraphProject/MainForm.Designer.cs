namespace HypergraphProject
{
    partial class MainForm
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
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblVertexNo = new System.Windows.Forms.ToolStripLabel();
            this.txtVertexNo = new System.Windows.Forms.ToolStripTextBox();
            this.lblEdgeNo = new System.Windows.Forms.ToolStripLabel();
            this.txtEdgeNo = new System.Windows.Forms.ToolStripTextBox();
            this.lblMaxCardinality = new System.Windows.Forms.ToolStripLabel();
            this.txtMaxCardinality = new System.Windows.Forms.ToolStripTextBox();
            this.btnGenerate = new System.Windows.Forms.ToolStripDropDownButton();
            this.acyclicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hypertreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arbitraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDual = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrixControl = new HypergraphProject.Interface.MatrixControl();
            this.pnlMatrixContainer = new System.Windows.Forms.Panel();
            this.toolMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlMatrixContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMain
            // 
            this.toolMain.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator1,
            this.lblVertexNo,
            this.txtVertexNo,
            this.lblEdgeNo,
            this.txtEdgeNo,
            this.lblMaxCardinality,
            this.txtMaxCardinality,
            this.btnGenerate,
            this.toolStripSeparator2,
            this.btnDual});
            this.toolMain.Location = new System.Drawing.Point(0, 24);
            this.toolMain.Name = "toolMain";
            this.toolMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolMain.Size = new System.Drawing.Size(780, 29);
            this.toolMain.TabIndex = 0;
            this.toolMain.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.Image = global::HypergraphProject.Properties.Resources.New;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(57, 26);
            this.btnNew.Text = "New";
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::HypergraphProject.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(62, 26);
            this.btnOpen.Text = "Open";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::HypergraphProject.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 26);
            this.btnSave.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // lblVertexNo
            // 
            this.lblVertexNo.Name = "lblVertexNo";
            this.lblVertexNo.Size = new System.Drawing.Size(51, 26);
            this.lblVertexNo.Text = "Vertices:";
            // 
            // txtVertexNo
            // 
            this.txtVertexNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVertexNo.Name = "txtVertexNo";
            this.txtVertexNo.Size = new System.Drawing.Size(50, 29);
            // 
            // lblEdgeNo
            // 
            this.lblEdgeNo.Name = "lblEdgeNo";
            this.lblEdgeNo.Size = new System.Drawing.Size(44, 26);
            this.lblEdgeNo.Text = " Edges:";
            // 
            // txtEdgeNo
            // 
            this.txtEdgeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEdgeNo.Name = "txtEdgeNo";
            this.txtEdgeNo.Size = new System.Drawing.Size(50, 29);
            // 
            // lblMaxCardinality
            // 
            this.lblMaxCardinality.Name = "lblMaxCardinality";
            this.lblMaxCardinality.Size = new System.Drawing.Size(69, 26);
            this.lblMaxCardinality.Text = " Max. Card.:";
            // 
            // txtMaxCardinality
            // 
            this.txtMaxCardinality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxCardinality.Name = "txtMaxCardinality";
            this.txtMaxCardinality.Size = new System.Drawing.Size(50, 29);
            // 
            // btnGenerate
            // 
            this.btnGenerate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acyclicToolStripMenuItem,
            this.hypertreeToolStripMenuItem,
            this.arbitraryToolStripMenuItem});
            this.btnGenerate.Image = global::HypergraphProject.Properties.Resources.Generate;
            this.btnGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(89, 26);
            this.btnGenerate.Text = "Generate";
            // 
            // acyclicToolStripMenuItem
            // 
            this.acyclicToolStripMenuItem.Name = "acyclicToolStripMenuItem";
            this.acyclicToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.acyclicToolStripMenuItem.Text = "α-Acyclic";
            // 
            // hypertreeToolStripMenuItem
            // 
            this.hypertreeToolStripMenuItem.Name = "hypertreeToolStripMenuItem";
            this.hypertreeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.hypertreeToolStripMenuItem.Text = "Hypertree";
            // 
            // arbitraryToolStripMenuItem
            // 
            this.arbitraryToolStripMenuItem.Name = "arbitraryToolStripMenuItem";
            this.arbitraryToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.arbitraryToolStripMenuItem.Text = "Arbitrary";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // btnDual
            // 
            this.btnDual.Image = global::HypergraphProject.Properties.Resources.Dual;
            this.btnDual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDual.Name = "btnDual";
            this.btnDual.Size = new System.Drawing.Size(57, 26);
            this.btnDual.Text = "Dual";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(780, 24);
            this.mnuMain.TabIndex = 1;
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            // 
            // matrixControl
            // 
            this.matrixControl.BackColor = System.Drawing.Color.White;
            this.matrixControl.Dimension = new System.Drawing.Size(0, 0);
            this.matrixControl.Location = new System.Drawing.Point(3, 3);
            this.matrixControl.Name = "matrixControl";
            this.matrixControl.Size = new System.Drawing.Size(150, 150);
            this.matrixControl.TabIndex = 2;
            // 
            // pnlMatrixContainer
            // 
            this.pnlMatrixContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMatrixContainer.AutoScroll = true;
            this.pnlMatrixContainer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlMatrixContainer.Controls.Add(this.matrixControl);
            this.pnlMatrixContainer.Location = new System.Drawing.Point(12, 56);
            this.pnlMatrixContainer.Name = "pnlMatrixContainer";
            this.pnlMatrixContainer.Size = new System.Drawing.Size(756, 410);
            this.pnlMatrixContainer.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 478);
            this.Controls.Add(this.pnlMatrixContainer);
            this.Controls.Add(this.toolMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "MainForm";
            this.Text = "ST: Algorithmic Hypergraph Theory";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlMatrixContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolMain;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblVertexNo;
        private System.Windows.Forms.ToolStripTextBox txtVertexNo;
        private System.Windows.Forms.ToolStripLabel lblEdgeNo;
        private System.Windows.Forms.ToolStripTextBox txtEdgeNo;
        private System.Windows.Forms.ToolStripLabel lblMaxCardinality;
        private System.Windows.Forms.ToolStripTextBox txtMaxCardinality;
        private System.Windows.Forms.ToolStripDropDownButton btnGenerate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem acyclicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hypertreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arbitraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnDual;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private Interface.MatrixControl matrixControl;
        private System.Windows.Forms.Panel pnlMatrixContainer;
    }
}

