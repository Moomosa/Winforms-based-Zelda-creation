namespace _225_Final
{
    partial class Overworld
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
            this.BigMap = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // BigMap
            // 
            this.BigMap.BackColor = System.Drawing.Color.Transparent;
            this.BigMap.BackgroundImage = global::_225_Final.Properties.Resources.Ground;
            this.BigMap.ColumnCount = 4;
            this.BigMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BigMap.Location = new System.Drawing.Point(0, 0);
            this.BigMap.Margin = new System.Windows.Forms.Padding(0);
            this.BigMap.Name = "BigMap";
            this.BigMap.RowCount = 4;
            this.BigMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.BigMap.Size = new System.Drawing.Size(3072, 2112);
            this.BigMap.TabIndex = 9;
            // 
            // Overworld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::_225_Final.Properties.Resources.Ground;
            this.Controls.Add(this.BigMap);
            this.Name = "Overworld";
            this.Size = new System.Drawing.Size(3072, 2112);
            this.ResumeLayout(false);

        }

        #endregion

        public TableLayoutPanel BigMap;
    }
}
