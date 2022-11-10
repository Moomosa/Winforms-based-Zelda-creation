using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _225_Final
{
    public class Link : Character
    {
        List<Image> linkImage = new();
        private Vector2 inputDirection = new();
        private Vector2 initialPosition = new();
        private Vector2 tileSize = new Vector2(24, 24);
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
            pic.Image = linkImage[4];
            Form1.gameField.Controls.Add(pic);
        }

        public void Movement()
        {
            if (!isMoving)
            {
                processMovement();
            }
            else if (inputDirection != Vector2.Zero)
            {
                //animState.Travel("Walk");
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
            {
                //rightStrength = 0;
                //leftStrength = 0;
                //if (Form1.key.KeyChar == (Char)Keys.Right)
                //    rightStrength = 1;
                //if (Form1.key.KeyChar == (Char)Keys.Left)
                //    leftStrength = 1;

                inputDirection.X = rightStrength - leftStrength;
            }
            if (inputDirection.X == 0)
            {
                //upStrength = 0;
                //downStrength = 0;
                //if (Form1.key.KeyChar == (Char)Keys.Up)
                //    upStrength = 1;
                //if (Form1.key.KeyChar == (Char)Keys.Down)
                //    downStrength = 1;

                inputDirection.Y = downStrength - upStrength;
            }

            if (inputDirection != Vector2.Zero)
            {
                //animTree.Set("parameters/Idle/blend_position", inputDirection);
                //animTree.Set("parameters/Walk/blend_position", inputDirection);
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
            Vector2 desiredStep = new Vector2(inputDirection.X * tileSize.X / 2, inputDirection.Y * tileSize.Y / 2);
            //ray.CastTo = desiredStep;
            //ray.ForceRaycastUpdate();
            if (isMoving)
            {
                percentMovedToNextTile += speed * 0.1f;
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

                    //Position = initialPosition + (tileSize * inputDirection * percentMovedToNextTile);
                }
            }
            else
            {
                isMoving = false;
            }
        }

    }
}
