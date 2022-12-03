using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final.Maps
{
    public partial class Map02 : UserControl
    {
        PictureBox fairy = new();
        List<Image> pics = new();
        Timer animTimer = new();
        int animCounter = 0;

        public Map02()
        {
            InitializeComponent();

            pics.Add(new Bitmap("Usable Sprites/Extra/Fairy1.png"));
            pics.Add(new Bitmap("Usable Sprites/Extra/Fairy2.png"));
            fairy.Image = pics[0];
            fairy.Height = 48;
            fairy.Width = 48;
            fairy.SizeMode = PictureBoxSizeMode.StretchImage;
            fairy.Location = new Point(336, 240);
            fairy.Tag = "Fairy";
            Controls.Add(fairy);

            animTimer.Interval = 20;
            animTimer.Tick += AnimTimer_Tick;
            animTimer.Enabled = false;
        }

        private void AnimTimer_Tick(object? sender, EventArgs e)
        {
            if (animCounter == 0) fairy.Image = pics[0];
            if (animCounter == 4) fairy.Image = pics[1];
            animCounter++;
            if (animCounter == 8) animCounter = 0;
        }
    }
}
