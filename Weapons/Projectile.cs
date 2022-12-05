using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final.Weapons
{
    public class Projectile : Control
    {
        public PictureBox pic = new();
        public List<Image> shotImages = new();
        GameView view;
        Timer flyTimer = new();
        private enum Facing { Up, Down, Left, Right }
        private Facing facing;
        public int X { get; set; }
        public int Y { get; set; }
        int damage = 1;

        public Projectile(int x, int y, Enum getFacing, GameView game)
        {
            //This all could be put in an inherited class including sword

            facing = (Facing)getFacing;
            view = game;
            X = x;
            Y = y;

            flyTimer.Interval = 30;
            flyTimer.Tick += FlyTimer_Tick;
            flyTimer.Enabled = true;

            shotImages.Add(new Bitmap("Usable Sprites/Weapons/Arrow01.png"));
            shotImages.Add(new Bitmap("Usable Sprites/Weapons/Arrow02.png"));
            shotImages.Add(new Bitmap("Usable Sprites/Weapons/Arrow03.png"));
            shotImages.Add(new Bitmap("Usable Sprites/Weapons/Arrow04.png"));
            //shotImages.Add(new Bitmap("Usable Sprites/Weapons/RockShot.png"));

            pic.Height = 48;
            pic.BackColor = Color.Transparent;
            pic.BackgroundImage = new Bitmap("Usable Sprites/Environment/Ground.png");

            switch (facing)
            {
                case Facing.Up:
                    pic.Width = 24;
                    X += 12;
                    Y -= 40;
                    pic.Image = shotImages[0];
                    break;
                case Facing.Down:
                    pic.Width = 24;
                    X += 12;
                    Y += 40;
                    pic.Image = shotImages[2];
                    break;
                case Facing.Left:
                    pic.Width = 48;
                    X -= 40;
                    pic.Image = shotImages[3];
                    break;
                case Facing.Right:
                    pic.Width = 48;
                    X += 40;
                    pic.Image = shotImages[1];
                    break;
            }

            pic.Left = X;
            pic.Top = Y;
        }

        ~Projectile()
        {            
        }

        private void FlyTimer_Tick(object? sender, EventArgs e)
        {
            Fly();
            Hit(facing);
        }

        private void Fly()
        {
            switch (facing)
            {
                case Facing.Up:
                    Y -= 15;
                    break;

                case Facing.Down:
                    Y += 15;
                    break;

                case Facing.Left:
                    X -= 15;
                    break;

                case Facing.Right:
                    X += 15;
                    break;
            }

            pic.Left = X;
            pic.Top = Y;

            if (X > view.Width - 24 || X < -24 || Y > view.Height - 24 || Y < -24)
                Remove();
        }

        public void Hit(Enum getFacing)
        {
            foreach (Character octo in Character.characters)
                if (octo is Enemy)
                    if (pic.Bounds.IntersectsWith(octo.pic.Bounds))
                    {
                        octo.isHit(getFacing, damage);
                        Remove();
                    }
        }

        public void Remove()
        {
            flyTimer.Enabled = false;
            view.Controls.Remove(this);
            pic.Dispose();
            Dispose();
        }
    }
}
