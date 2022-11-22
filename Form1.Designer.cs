namespace _225_Final
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.picTitle = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // picTitle
            // 
            this.picTitle.Image = global::_225_Final.Properties.Resources.ZeldaTitle;
            this.picTitle.Location = new System.Drawing.Point(0, -1);
            this.picTitle.Name = "picTitle";
            this.picTitle.Size = new System.Drawing.Size(752, 682);
            this.picTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTitle.TabIndex = 1;
            this.picTitle.TabStop = false;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Black;
            this.pnlTop.Location = new System.Drawing.Point(0, -1);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(752, 192);
            this.pnlTop.TabIndex = 2;
            this.pnlTop.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::_225_Final.Properties.Resources.Ground;
            this.ClientSize = new System.Drawing.Size(752, 681);
            this.Controls.Add(this.picTitle);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zelda";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picTitle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox picTitle;
        private Panel pnlTop;
    }
}