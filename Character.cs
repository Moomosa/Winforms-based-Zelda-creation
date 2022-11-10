using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _225_Final
{
    public class Character : Control
    {
        public static List<Character> characters = new();
        public PictureBox pic = new PictureBox();
        public int X { get; set; }
        public int Y { get; set; }
        public Character(int x, int y)
        {
            X = x;
            Y = y;
            pic.Left = X;
            pic.Top = Y;
            pic.Width = 48;
            pic.Height = 48;
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

    }
}
