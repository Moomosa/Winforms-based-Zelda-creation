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
        public List<Image> swordList = new();

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

            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword01.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword02.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword03.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword04.png"));

            swordPic.Height = 48;
            swordPic.BackColor = Color.Transparent;
            swordPic.BackgroundImage = new Bitmap("Usable Sprites/Environment/Ground.png");
            swordPic.SizeMode = PictureBoxSizeMode.StretchImage;

            switch (facing)     //This checks player facing and places sword in correct place and image
            {
                case Facing.Up:
                    swordPic.Width = 24;
                    X += 12;
                    Y -= 40;
                    swordPic.Image = swordList[0];
                    break;
                case Facing.Down:
                    swordPic.Width = 24;
                    X += 12;
                    Y += 40;
                    swordPic.Image = swordList[2];
                    break;
                case Facing.Left:
                    swordPic.Width = 48;
                    X -= 40;
                    swordPic.Image = swordList[3];
                    break;
                case Facing.Right:
                    swordPic.Width = 48;
                    X += 40;
                    swordPic.Image = swordList[1];
                    break;


            }
            swordPic.Left = X;
            swordPic.Top = Y;
            
            
            //Form1.gameField.Controls.Add(swordPic);
        }

        public void Remove()
        {
            swordPic.Dispose();
        }

        public void Hit(Enum getFacing)
        {
            foreach (Character octo in Character.characters)
                if (octo is Enemy)
                    if (swordPic.Bounds.IntersectsWith(octo.pic.Bounds))
                        octo.isHit(getFacing, damage);
        }
    }
}
