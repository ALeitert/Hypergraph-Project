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
    public partial class PictureForm : Form
    {
        public PictureForm()
        {
            InitializeComponent();
        }

        public Image Image
        {
            get
            {
                return picDrawing.Image;
            }
            set
            {
                picDrawing.Image = value;
                btnSave.Enabled = value != null;

                if (value == null) return;

                this.ClientSize = new Size(picDrawing.Width, toolMain.Height + picDrawing.Height);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSaveImage.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            try
            {
                Image.Save(dlgSaveImage.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "Error during saving.",
                    "Unable to save the image.\n\n" + ex.GetType().FullName + "\n" + ex.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
