namespace HypergraphProject
{
    partial class InfoForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblPictograms = new System.Windows.Forms.Label();
            this.lnkBitbucket = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(268, 192);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(331, 72);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "This program was made as coding project for the ST: Algorithmic Hypergraph Theory" +
    " class, Kent State University, Spring 2015. The task is to visualise a hypertree" +
    " based on its underlying tree.";
            // 
            // lblPictograms
            // 
            this.lblPictograms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPictograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPictograms.Location = new System.Drawing.Point(12, 95);
            this.lblPictograms.Name = "lblPictograms";
            this.lblPictograms.Size = new System.Drawing.Size(331, 37);
            this.lblPictograms.TabIndex = 2;
            this.lblPictograms.Text = "Pictograms are based on pictograms by Daniel Bruce.\r\nCC BY-SA 4.0";
            // 
            // lnkBitbucket
            // 
            this.lnkBitbucket.AutoSize = true;
            this.lnkBitbucket.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkBitbucket.Location = new System.Drawing.Point(12, 146);
            this.lnkBitbucket.Name = "lnkBitbucket";
            this.lnkBitbucket.Size = new System.Drawing.Size(292, 16);
            this.lnkBitbucket.TabIndex = 3;
            this.lnkBitbucket.TabStop = true;
            this.lnkBitbucket.Text = "https://bitbucket.org/Seneferu/hyergraph-project";
            this.lnkBitbucket.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBitbucket_LinkClicked);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 227);
            this.Controls.Add(this.lnkBitbucket);
            this.Controls.Add(this.lblPictograms);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblPictograms;
        private System.Windows.Forms.LinkLabel lnkBitbucket;
    }
}