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
        private Vector2 inputDirection = new();
        private Vector2 initialPosition = new();
        private Vector2 tileSize = new Vector2(24, 24);
        private enum Facing { Up, Down, Left, Right }
        private Facing facing = Facing.Up;
        private Timer animationTimer = new();
        private int animCounter = 0;
        private bool isMoving = false;
        private int health = 6;
        private int speed = 2;
        public int rightStrength = 0;
        public int leftStrength = 0;
        public int upStrength = 0;
        public int downStrength = 0;

        private float percentMovedToNextTile = 0.0f;

        public Link(int x, int y) : base(x, y)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Link"))
            {
                if (!image.Contains(".png"))
                    continue;
                linkImage.Add(new Bitmap(image));
            }
            pic.Image = linkImage[5];
            Form1.gameField.Controls.Add(pic);
            animationTimer.Enabled = false;
            animationTimer.Interval = 60;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        #region Movement
        public void Movement()
        {
            if (!isMoving)
            {
                processMovement();
                Animation();
            }
            else if (inputDirection != Vector2.Zero)
            {
                if (inputDirection == new Vector2(0, -1)) //Up 0,-1                
                    facing = Facing.Up;

                if (inputDirection == new Vector2(0, 1))  //Down 0,1                
                    facing = Facing.Down;

                if (inputDirection == new Vector2(-1, 0)) //Left -1,0                
                    facing = Facing.Left;

                if (inputDirection == new Vector2(1, 0))  //Right 1,0                
                    facing = Facing.Right;

                Animation();
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
        #endregion
        #region Animation
        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            switch (facing)
            {
                case Facing.Up:
                    if (animCounter == 0)
                        pic.Image = linkImage[4];
                    if (animCounter == 4)
                        pic.Image = linkImage[5];
                    break;

                case Facing.Down:
                    if (animCounter == 0)
                        pic.Image = linkImage[0];
                    if (animCounter == 4)
                        pic.Image = linkImage[1];
                    break;

                case Facing.Left:
                    if (animCounter == 0)
                        pic.Image = linkImage[11];
                    if (animCounter == 4)
                        pic.Image = linkImage[12];
                    break;

                case Facing.Right:
                    if (animCounter == 0)
                        pic.Image = linkImage[2];
                    if (animCounter == 4)
                        pic.Image = linkImage[3];
                    break;
            }
            animCounter++;
            if (animCounter == 9)
                animCounter = 0;
        }


        public void Animation()
        {
            if (isMoving)
                animationTimer.Enabled = true;
            else
                animationTimer.Enabled = false;
        }
        #endregion
    }
}
