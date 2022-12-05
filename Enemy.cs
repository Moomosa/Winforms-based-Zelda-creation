using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public class Enemy : Character
    {
        private List<Image> octoImage = new();
        public Timer moveTimer = new();
        public int damage = 1;
        private int speed = 2;
        int moveCounter = 0;


        public Enemy(Point point, GameView view) : base(point, view)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Enemies"))
            {
                if (!image.Contains(".png"))
                    continue;
                octoImage.Add(new Bitmap(image));
            }
            facing = (Facing)Form1.rng.Next(0, 4);
            tileSize = new Vector2(48, 48);
            animationTimer.Enabled = true;
            moveTimer.Enabled = true;
            moveTimer.Interval = 100;
            moveTimer.Tick += MoveTimer_Tick;
            health = 2;
            Tag = "octo";
        }
        #region Movement
        private void MoveTimer_Tick(object? sender, EventArgs e)
        {
            if (moveCounter == 10)
            {
                rightStrength = Form1.rng.Next(2);
                leftStrength = Form1.rng.Next(2);
                upStrength = Form1.rng.Next(2);
                downStrength = Form1.rng.Next(2);
                moveCounter = 0;
            }
            else
            {
                rightStrength = 0;
                leftStrength = 0;
                upStrength = 0;
                downStrength = 0;
            }
            moveCounter++;
            Movement();
            collide(damage);
            Death();
        }

        public void ProcessMovement()
        {
            if (inputDirection.Y == 0)
                inputDirection.X = rightStrength - leftStrength;

            if (inputDirection.X == 0)
                inputDirection.Y = downStrength - upStrength;

            if (inputDirection != Vector2.Zero)
            {
                initialPosition.X = X;
                initialPosition.Y = Y;
                isMoving = true;
            }
            else
            {
                //animState.Travel("Idle");
            }
        }

        public void Movement()
        {
            if (!isMoving)
            {
                ProcessMovement();
            }
            else if (inputDirection != Vector2.Zero)
            {
                if (inputDirection == new Vector2(0, -1)) //Up 0,-1
                {
                    facing = Facing.Up;
                }

                if (inputDirection == new Vector2(0, 1))  //Down 0,1
                {
                    facing = Facing.Down;
                }

                if (inputDirection == new Vector2(-1, 0)) //Left -1,0
                {
                    facing = Facing.Left;
                }

                if (inputDirection == new Vector2(1, 0))  //Right 1,0
                {
                    facing = Facing.Right;
                }

                move();
            }
            else
            {
                //animState.Travel("Idle");
                isMoving = false;
            }
        }

        public override void move()
        {
            base.move();
            if (isMoving)
            {
                percentMovedToNextTile += speed * 0.08f;
                if (percentMovedToNextTile >= 1.0)
                {
                    X = (int)Math.Round(initialPosition.X + (tileSize.X * inputDirection.X));
                    Y = (int)Math.Round(initialPosition.Y + (tileSize.Y * inputDirection.Y));
                    pic.Left = X;
                    pic.Top = Y;
                    percentMovedToNextTile = 0.0f;
                    isMoving = false;
                }
                else
                {
                    X = (int)Math.Round(initialPosition.X + (tileSize.X * inputDirection.X * percentMovedToNextTile));
                    Y = (int)Math.Round(initialPosition.Y + (tileSize.Y * inputDirection.Y * percentMovedToNextTile));
                    pic.Left = X;
                    pic.Top = Y;
                }
            }
            else
                isMoving = false;
        }

        #endregion
        #region Animation
        public override void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            base.AnimationTimer_Tick(sender, e);
        }

        public override void Animation()
        {
            switch (facing)
            {
                case Facing.Up:
                    if (animCounter == 0) pic.Image = octoImage[4];
                    if (animCounter == 4) pic.Image = octoImage[5];
                    break;

                case Facing.Down:
                    if (animCounter == 0) pic.Image = octoImage[0];
                    if (animCounter == 4) pic.Image = octoImage[1];
                    break;

                case Facing.Left:
                    if (animCounter == 0) pic.Image = octoImage[2];
                    if (animCounter == 4) pic.Image = octoImage[3];
                    break;

                case Facing.Right:
                    if (animCounter == 0) pic.Image = octoImage[6];
                    if (animCounter == 4) pic.Image = octoImage[7];
                    break;
            }
        }
        #endregion

        public override void HitTimer_Tick(object? sender, EventArgs e)
        {
            base.HitTimer_Tick(sender, e);
            moveTimer.Enabled = true;
        }

        public override void isHit(Enum getFacing, int damage)
        {
            moveTimer.Enabled = false;
            base.isHit(getFacing, damage);
        }

        public override void Death()
        {
            if (health <= 0 || X > view.Width || X < 0 || Y > view.Height || Y < 0)
            {
                base.Death();
                moveTimer.Stop();
            }
        }

        public override void Shoot()
        {
            base.Shoot();
        }

        public void collide(int damage)
        {
            foreach (Character octo in Character.characters)
                if (octo is Enemy)
                    if (Character.characters[0].pic.Bounds.IntersectsWith(octo.pic.Bounds))
                        Character.characters[0].isHit(Character.characters[0].facing, damage);
        }
    }
}
