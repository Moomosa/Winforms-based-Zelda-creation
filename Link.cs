using _225_Final.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public class Link : Character
    {
        private List<Image> linkImage = new();
        Sword sword;
        private Vector2 tileSize = new Vector2(24, 24);
        public bool isAttacking = false;
        private int speed = 5;
        

        public Link(int x, int y) : base(x, y)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Link"))
            {
                if (!image.Contains(".png"))
                    continue;
                linkImage.Add(new Bitmap(image));
            }
            pic.Image = linkImage[5];
            facing = Character.Facing.Up;
            health = 6;
        }

        #region Movement
        public void Movement()
        {
            if (!isMoving)
            {
                processMovement();
                AnimationState();
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

                AnimationState();
                move();
            }
            else
            {
                //animState.Travel("Idle");
                isMoving = false;
            }
        }

        public void processMovement()
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

        public void move()
        {
            if (!isAttacking)
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
        }
        #endregion
        #region Animation
        public override void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            Animation();

            animCounter++;
            if (animCounter == 9)
                animCounter = 0;
        }

        public override void Animation()
        {
            if (!isAttacking)
            {
                switch (facing)
                {
                    case Facing.Up:
                        if (animCounter == 0) pic.Image = linkImage[4];
                        if (animCounter == 4) pic.Image = linkImage[5];
                        break;

                    case Facing.Down:
                        if (animCounter == 0) pic.Image = linkImage[0];
                        if (animCounter == 4) pic.Image = linkImage[1];
                        break;

                    case Facing.Left:
                        if (animCounter == 0) pic.Image = linkImage[11];
                        if (animCounter == 4) pic.Image = linkImage[12];
                        break;

                    case Facing.Right:
                        if (animCounter == 0) pic.Image = linkImage[2];
                        if (animCounter == 4) pic.Image = linkImage[3];
                        break;
                }
            }
            if (isAttacking)
            {
                switch (facing)
                {
                    case Facing.Up:
                        if (animCounter == 0) pic.Image = linkImage[8];
                        if (animCounter == 5) pic.Image = linkImage[4];
                        break;

                    case Facing.Down:
                        if (animCounter == 0) pic.Image = linkImage[6];
                        if (animCounter == 5) pic.Image = linkImage[0];
                        break;

                    case Facing.Left:
                        if (animCounter == 0) pic.Image = linkImage[13];
                        if (animCounter == 5) pic.Image = linkImage[11];
                        break;

                    case Facing.Right:
                        if (animCounter == 0) pic.Image = linkImage[7];
                        if (animCounter == 5) pic.Image = linkImage[2];
                        break;
                }
                if (animCounter == 5)
                {
                    sword.Remove();
                    isAttacking = false;
                    animCounter = 0;

                }

            }

        }

        public void AnimationState()
        {
            if (isMoving || isAttacking)
                animationTimer.Enabled = true;
            else
                animationTimer.Enabled = false;
        }
        #endregion

        public void Attack()
        {
            isAttacking = true;
            animCounter = 0;
            sword = new Sword(X, Y, facing);
            sword.Hit(facing);
        }
    }
}
