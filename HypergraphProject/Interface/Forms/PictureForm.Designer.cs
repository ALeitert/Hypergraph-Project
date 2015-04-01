namespace HypergraphProject
{
    partial class PictureForm
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
            this.pnlWorkspace = new System.Windows.Forms.Panel();
            this.picDrawing = new System.Windows.Forms.PictureBox();
            this.toolMain = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.dlgSaveImage = new System.Windows.Forms.SaveFileDialog();
            this.pnlWorkspace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).BeginInit();
            this.toolMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWorkspace
            // 
            this.pnlWorkspace.AutoScroll = true;
            this.pnlWorkspace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlWorkspace.Controls.Add(this.picDrawing);
            this.pnlWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWorkspace.Location = new System.Drawing.Point(0, 29);
            this.pnlWorkspace.Name = "pnlWorkspace";
            this.pnlWorkspace.Size = new System.Drawing.Size(284, 232);
            this.pnlWorkspace.TabIndex = 4;
            // 
            // picDrawing
            // 
            this.picDrawing.Location = new System.Drawing.Point(0, 0);
            this.picDrawing.Name = "picDrawing";
            this.picDrawing.Size = new System.Drawing.Size(100, 50);
            this.picDrawing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDrawing.TabIndex = 0;
            this.picDrawing.TabStop = false;
            // 
            // toolMain
            // 
            this.toolMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolMain.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.toolMain.Location = new System.Drawing.Point(0, 0);
            this.toolMain.Name = "toolMain";
            this.toolMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolMain.Size = new System.Drawing.Size(284, 29);
            this.toolMain.TabIndex = 3;
            this.toolMain.Text = "ToolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = global::HypergraphProject.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 26);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dlgSaveImage
            // 
            this.dlgSaveImage.Filter = "Portable Network Graphics (*.png)|*.png";
            // 
            // PictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pnlWorkspace);
            this.Controls.Add(this.toolMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PictureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PictureForm";
            this.pnlWorkspace.ResumeLayout(false);
            this.pnlWorkspace.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawing)).EndInit();
            this.toolMain.ResumeLayout(false);
            this.toolMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox picDrawing;
        internal System.Windows.Forms.Panel pnlWorkspace;
        internal System.Windows.Forms.ToolStrip toolMain;
        internal System.Windows.Forms.ToolStripButton btnSave;
        internal System.Windows.Forms.SaveFileDialog dlgSaveImage;
    }
}