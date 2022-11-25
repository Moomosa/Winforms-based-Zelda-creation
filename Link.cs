using _225_Final.Weapons;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public bool isAttacking = false;
        private int speed = 5;

        public Link(Point point, Map game) : base(point, game)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Link"))
            {
                if (!image.Contains(".png"))
                    continue;
                linkImage.Add(new Bitmap(image));
            }
            pic.Image = linkImage[5];
            facing = Character.Facing.Up;
            tileSize = new Vector2(24, 24);
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
            if (!struck)
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
            }
        }

        public override void move()
        {
            base.move();
            if (!isAttacking)
                if (!struck)
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
            changeMap();
        }

        public void Wall()
        {
            pic.Left = (int)initialPosition.X;
            pic.Top = (int)initialPosition.Y;
            X = pic.Left;
            Y = pic.Top;
        }
        #endregion
        #region Animation
        public override void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            base.AnimationTimer_Tick(sender, e);
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
                    map.Controls.Remove(sword.swordPic);
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
        #region Combat
        public void Attack()
        {
            isAttacking = true;
            animCounter = 0;
            sword = new Sword(X, Y, facing);
            sword.Hit(facing);
            map.Controls.Add(sword.swordPic);
        }

        public override void isHit(Enum getFacing, int damage)
        {
            base.isHit(getFacing, damage);
            struck = true;
        }

        public override void HitTimer_Tick(object? sender, EventArgs e)
        {
            isMoving = false;
            inputDirection = new Vector2(0, 0);
            base.HitTimer_Tick(sender, e);
        }
        #endregion
        public void changeMap()
        {
            if (X > map.Width - 24)
            {
                X = -24;
                map.mapX++;
            }
            if (X < -24)
            {
                X = map.Width - 24;
                map.mapX--;
            }
            if (Y > map.Height - 24)
            {
                Y = -24;
                map.mapY--;
            }
            if (Y < -24)
            {
                Y = map.Height - 24;
                map.mapY++;
            }
        }

    }
}
