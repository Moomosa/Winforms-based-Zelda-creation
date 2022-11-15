using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _225_Final.Weapons
{
    public class Sword
    {
        public PictureBox swordPic = new PictureBox();
        private enum Facing { Up, Down, Left, Right }
        private Facing facing;
        private int X { get; set; }
        private int Y { get; set; }
        public int damage = 1;

        public Sword(int x, int y, Enum getFacing)
        {
            facing = (Facing)getFacing;
            X = x;
            Y = y;
            swordPic.Height = 48;
            swordPic.BackColor = Color.Transparent;
            swordPic.SizeMode = PictureBoxSizeMode.StretchImage;

            switch (facing)
            {
                case Facing.Up:
                    swordPic.Width = 24;
                    X += 12;
                    Y -= 40;
                    swordPic.Image = Form1.swordList[0];
                    break;
                case Facing.Down:
                    swordPic.Width = 24;
                    X += 12;
                    Y += 40;
                    swordPic.Image = Form1.swordList[2];                    
                    break;
                case Facing.Left:
                    swordPic.Width = 48;
                    X -= 40;
                    swordPic.Image = Form1.swordList[3];                    
                    break;
                case Facing.Right:
                    swordPic.Width = 48;
                    X += 40;
                    swordPic.Image = Form1.swordList[1];
                    break;


            }
            swordPic.Left = X;
            swordPic.Top = Y;

            Form1.gameField.Controls.Add(swordPic);
        }

        public void Remove()
        {
            Form1.gameField.Controls.Remove(swordPic);
            swordPic.Dispose();
        }
    }
}
