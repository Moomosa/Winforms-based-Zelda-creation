using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public class Character : Control
    {
        public static List<Character> characters = new();
        public PictureBox pic = new PictureBox();
        protected Timer animationTimer = new();
        protected Timer hitTimer = new();
        protected Vector2 inputDirection = new();
        protected Vector2 initialPosition = new();

        public enum Facing { Up, Down, Left, Right }
        public Facing facing;
        public int X { get; set; }
        public int Y { get; set; }
        protected int animCounter = 0;
        public int rightStrength = 0;
        public int leftStrength = 0;
        public int upStrength = 0;
        public int downStrength = 0;
        protected int hitCounter = 0;
        protected int health;
        protected bool isMoving = false;
        protected bool struck = false;
        protected float percentMovedToNextTile = 0.0f;


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

            Form1.gameField.Controls.Add(pic);
            animationTimer.Enabled = false;
            animationTimer.Interval = 60;
            animationTimer.Tick += AnimationTimer_Tick;

            hitTimer.Enabled = false;
            hitTimer.Interval = 20;
            hitTimer.Tick += HitTimer_Tick;
        }


        public virtual void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            Animation();

            animCounter++;
            if (animCounter == 9)
                animCounter = 0;

        }

        public virtual void isHit(Enum getFacing, int damage)
        {
            if ((Facing)getFacing == Facing.Down) facing = Facing.Up;
            if ((Facing)getFacing == Facing.Up) facing = Facing.Down;
            if ((Facing)getFacing == Facing.Left) facing = Facing.Right;
            if ((Facing)getFacing == Facing.Right) facing = Facing.Left;
            animCounter = 0;
            hitCounter = 0;
            animationTimer.Interval = 20;
            hitTimer.Enabled = true;
            isMoving = false;
            health -= damage;

        }
        public virtual void HitTimer_Tick(object? sender, EventArgs e)
        {
            switch (facing)
            {
                case Facing.Up:
                    pic.Top += 16;
                    break;

                case Facing.Down:
                    pic.Top -= 16;
                    break;

                case Facing.Left:
                    pic.Left += 16;
                    break;

                case Facing.Right:
                    pic.Left -= 16;
                    break;
            }
            hitCounter++;
            Animation();
            if (hitCounter == 8)
            {
                X = pic.Left;
                Y = pic.Top;
                hitTimer.Enabled = false;
                animationTimer.Interval = 60;
                struck = false;
            }

        }

        public virtual void Animation()
        {

        }

        public virtual void Death()
        {
                animationTimer.Stop();
                Character.characters.Remove(this);
                DeleteFromForm();
        }

        public void DeleteFromForm()
        {
            Form1.gameField.Controls.Remove(pic);

            Dispose();
        }
    }
}
