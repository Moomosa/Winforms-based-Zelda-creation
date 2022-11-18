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
        private Vector2 tileSize = new Vector2(48, 48);
        private Timer moveTimer = new();
        public int damage = 1;
        private int speed = 2;
        int moveCounter = 0;


        public Enemy(int x, int y) : base(x, y)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Enemies"))
            {
                if (!image.Contains(".png"))
                    continue;
                octoImage.Add(new Bitmap(image));
            }
            facing = (Facing)Form1.rng.Next(0, 4);
            pic.SendToBack();
            Form1.gameField.Controls.Add(pic);
            animationTimer.Enabled = true;
            moveTimer.Enabled = true;
            moveTimer.Interval = 100;
            moveTimer.Tick += MoveTimer_Tick;
            health = 2;
        }

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
            Death();
        }

        public override void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            Animation();

            animCounter++;
            if (animCounter == 9)
                animCounter = 0;

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

        public override void HitTimer_Tick(object? sender, EventArgs e)
        {
            
            switch (facing)
            {
                case Facing.Up:
                    pic.Top += 5;                    
                    break;

                case Facing.Down:
                    pic.Top -= 5;
                    break;

                case Facing.Left:
                    pic.Left += 5;
                    break;

                case Facing.Right:
                    pic.Left -= 5;
                    break;
            }
            hitCounter++;
            Animation();
            if(hitCounter == 20)
            {
                X = pic.Left;
                Y = pic.Top;
                hitTimer.Enabled = false;
                animationTimer.Interval = 60;
                moveTimer.Enabled = true;
            }
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


        public void move()
        {
            //Vector2 desiredStep = new Vector2(inputDirection.X * tileSize.X / 2, inputDirection.Y * tileSize.Y / 2);
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
        public override void isHit(Enum getFacing, int damage)
        {
            moveTimer.Enabled = false;
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

        public override void Death()
        {
            base.Death();
        }
    }
}
