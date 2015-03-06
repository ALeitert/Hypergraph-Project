﻿namespace HypergraphProject
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblVertexLabel = new System.Windows.Forms.ToolStripLabel();
            this.lblVertexNumber = new System.Windows.Forms.ToolStripLabel();
            this.lblEdgeLabel = new System.Windows.Forms.ToolStripLabel();
            this.lblEdgesNumber = new System.Windows.Forms.ToolStripLabel();
            this.lblMaxCardinalityLabel = new System.Windows.Forms.ToolStripLabel();
            this.lblMaxCardinalityNumber = new System.Windows.Forms.ToolStripLabel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.matrixControl = new HypergraphProject.Interface.MatrixControl();
            this.pnlMatrixContainer = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuEditAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditAddVertex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditAddEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRemoveVertex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRemoveEdge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditResize = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDual = new System.Windows.Forms.ToolStripButton();
            this.mnuEditExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditConnections = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.pnlMatrixContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMain
            // 
            this.toolMain.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerate,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnEdit,
            this.btnDual,
            this.toolStripSeparator2,
            this.lblVertexLabel,
            this.lblVertexNumber,
            this.lblEdgeLabel,
            this.lblEdgesNumber,
            this.lblMaxCardinalityLabel,
            this.lblMaxCardinalityNumber});
            this.toolMain.Location = new System.Drawing.Point(0, 24);
            this.toolMain.Name = "toolMain";
            this.toolMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolMain.Size = new System.Drawing.Size(780, 29);
            this.toolMain.TabIndex = 0;
            this.toolMain.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // lblVertexLabel
            // 
            this.lblVertexLabel.Name = "lblVertexLabel";
            this.lblVertexLabel.Size = new System.Drawing.Size(51, 26);
            this.lblVertexLabel.Text = "Vertices:";
            // 
            // lblVertexNumber
            // 
            this.lblVertexNumber.Name = "lblVertexNumber";
            this.lblVertexNumber.Size = new System.Drawing.Size(13, 26);
            this.lblVertexNumber.Text = "0";
            this.lblVertexNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEdgeLabel
            // 
            this.lblEdgeLabel.Name = "lblEdgeLabel";
            this.lblEdgeLabel.Size = new System.Drawing.Size(41, 26);
            this.lblEdgeLabel.Text = "Edges:";
            // 
            // lblEdgesNumber
            // 
            this.lblEdgesNumber.Name = "lblEdgesNumber";
            this.lblEdgesNumber.Size = new System.Drawing.Size(13, 26);
            this.lblEdgesNumber.Text = "0";
            this.lblEdgesNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaxCardinalityLabel
            // 
            this.lblMaxCardinalityLabel.Name = "lblMaxCardinalityLabel";
            this.lblMaxCardinalityLabel.Size = new System.Drawing.Size(69, 26);
            this.lblMaxCardinalityLabel.Text = " Max. Card.:";
            // 
            // lblMaxCardinalityNumber
            // 
            this.lblMaxCardinalityNumber.Name = "lblMaxCardinalityNumber";
            this.lblMaxCardinalityNumber.Size = new System.Drawing.Size(13, 26);
            this.lblMaxCardinalityNumber.Text = "0";
            this.lblMaxCardinalityNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.matrixControl.Location = new System.Drawing.Point(0, 0);
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
            // btnGenerate
            // 
            this.btnGenerate.Image = global::HypergraphProject.Properties.Resources.Generate;
            this.btnGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(57, 26);
            this.btnGenerate.Text = "New";
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
            // btnEdit
            // 
            this.btnEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditConnections,
            this.toolStripMenuItem3,
            this.mnuEditAdd,
            this.mnuEditRemove,
            this.toolStripMenuItem1,
            this.mnuEditResize,
            this.toolStripMenuItem2,
            this.mnuEditExecute});
            this.btnEdit.Image = global::HypergraphProject.Properties.Resources.Edit;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(62, 26);
            this.btnEdit.Text = "Edit";
            // 
            // mnuEditAdd
            // 
            this.mnuEditAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditAddVertex,
            this.mnuEditAddEdge});
            this.mnuEditAdd.Name = "mnuEditAdd";
            this.mnuEditAdd.Size = new System.Drawing.Size(152, 22);
            this.mnuEditAdd.Text = "Add";
            // 
            // mnuEditAddVertex
            // 
            this.mnuEditAddVertex.Name = "mnuEditAddVertex";
            this.mnuEditAddVertex.Size = new System.Drawing.Size(106, 22);
            this.mnuEditAddVertex.Text = "Vertex";
            // 
            // mnuEditAddEdge
            // 
            this.mnuEditAddEdge.Name = "mnuEditAddEdge";
            this.mnuEditAddEdge.Size = new System.Drawing.Size(106, 22);
            this.mnuEditAddEdge.Text = "Edge";
            // 
            // mnuEditRemove
            // 
            this.mnuEditRemove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditRemoveVertex,
            this.mnuEditRemoveEdge});
            this.mnuEditRemove.Name = "mnuEditRemove";
            this.mnuEditRemove.Size = new System.Drawing.Size(152, 22);
            this.mnuEditRemove.Text = "Remove";
            // 
            // mnuEditRemoveVertex
            // 
            this.mnuEditRemoveVertex.Name = "mnuEditRemoveVertex";
            this.mnuEditRemoveVertex.Size = new System.Drawing.Size(106, 22);
            this.mnuEditRemoveVertex.Text = "Vertex";
            // 
            // mnuEditRemoveEdge
            // 
            this.mnuEditRemoveEdge.Name = "mnuEditRemoveEdge";
            this.mnuEditRemoveEdge.Size = new System.Drawing.Size(106, 22);
            this.mnuEditRemoveEdge.Text = "Edge";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuEditResize
            // 
            this.mnuEditResize.Name = "mnuEditResize";
            this.mnuEditResize.Size = new System.Drawing.Size(152, 22);
            this.mnuEditResize.Text = "Resize...";
            // 
            // btnDual
            // 
            this.btnDual.Image = global::HypergraphProject.Properties.Resources.Dual;
            this.btnDual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDual.Name = "btnDual";
            this.btnDual.Size = new System.Drawing.Size(57, 26);
            this.btnDual.Text = "Dual";
            // 
            // mnuEditExecute
            // 
            this.mnuEditExecute.Enabled = false;
            this.mnuEditExecute.Name = "mnuEditExecute";
            this.mnuEditExecute.Size = new System.Drawing.Size(152, 22);
            this.mnuEditExecute.Text = "Execute";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuEditConnections
            // 
            this.mnuEditConnections.Name = "mnuEditConnections";
            this.mnuEditConnections.Size = new System.Drawing.Size(152, 22);
            this.mnuEditConnections.Text = "Connections";
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
        private System.Windows.Forms.ToolStripButton btnGenerate;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblVertexLabel;
        private System.Windows.Forms.ToolStripLabel lblEdgeLabel;
        private System.Windows.Forms.ToolStripLabel lblMaxCardinalityLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnDual;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private Interface.MatrixControl matrixControl;
        private System.Windows.Forms.Panel pnlMatrixContainer;
        private System.Windows.Forms.ToolStripDropDownButton btnEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuEditAddVertex;
        private System.Windows.Forms.ToolStripMenuItem mnuEditAddEdge;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRemove;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRemoveVertex;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRemoveEdge;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditResize;
        private System.Windows.Forms.ToolStripLabel lblVertexNumber;
        private System.Windows.Forms.ToolStripLabel lblEdgesNumber;
        private System.Windows.Forms.ToolStripLabel lblMaxCardinalityNumber;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuEditExecute;
        private System.Windows.Forms.ToolStripMenuItem mnuEditConnections;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    }
}

