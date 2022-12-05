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
        public delegate void HealthChange(int currentHealth);
        public event HealthChange healthChange;
        private Timer deathTimer = new();
        int deathCounter = 0;
        private List<Image> linkImage = new();
        Sword sword;
        public bool isAttacking = false;
        private int speed = 5;
        string dieTune = @"Sounds/Link_Die.wav";
        string poink = @"Sounds/Poink.wav";
        string gameover = @"Sounds/Game-Over.wav";


        public Link(Point point, GameView view) : base(point, view)
        {
            foreach (string image in Directory.GetFiles("Usable Sprites/Link"))
            {
                if (!image.Contains(".png"))
                    continue;
                linkImage.Add(new Bitmap(image));
            }
            Tag = "player";
            linkImage.Add(new Bitmap("Usable Sprites/Extra/Explode02.png"));
            linkImage.Add(new Bitmap("Usable Sprites/Extra/Explode01.png"));

            pic.Image = linkImage[5];
            facing = Character.Facing.Up;
            tileSize = new Vector2(24, 24);
            health = 6;

            deathTimer.Interval = 16;
            deathTimer.Tick += DeathTimer_Tick;
            deathTimer.Enabled = false;
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
                    if (view.Controls.Contains(sword.swordPic))
                        view.Controls.Remove(sword.swordPic);
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
            view.Controls.Add(sword.swordPic);
            sword.swordPic.BringToFront();
        }

        public override void Shoot()
        {
            isAttacking = true;
            base.Shoot();
        }

        public override void isHit(Enum getFacing, int damage)
        {
            base.isHit(getFacing, damage);
            healthChange.Invoke(health);
            struck = true;
            Death();
        }

        public override void HitTimer_Tick(object? sender, EventArgs e)
        {
            isMoving = false;
            inputDirection = new Vector2(0, 0);
            base.HitTimer_Tick(sender, e);
        }

        public override void Death()
        {
            if (health <= 0)
            {
                for (int i = Character.characters.Count - 1; i >= 1; i--)
                {
                    if (Character.characters[i] is Enemy octo)
                    {
                        octo.moveTimer.Enabled = false;
                        octo.pic.Image = null;
                    }
                    Character.characters[i].animationTimer.Enabled = false;
                }
                Form1.music.controls.stop();
                view.mainTimer.Enabled = false;
                view.mainTimer.Stop();
                deathTimer.Enabled = true;
                Form1.music.URL = dieTune;

                Form1.music.controls.play();


                base.Death();

            }
        }

        private void DeathTimer_Tick(object? sender, EventArgs e)
        {   //Whole animation should be ~2 seconds
            if (deathCounter == 0 || deathCounter == 25 || deathCounter == 45 || deathCounter == 65) pic.Image = linkImage[0];
            if (deathCounter == 10 || deathCounter == 30 || deathCounter == 50 || deathCounter == 70) pic.Image = linkImage[2];
            if (deathCounter == 15 || deathCounter == 35 || deathCounter == 55 || deathCounter == 75) pic.Image = linkImage[4];
            if (deathCounter == 20 || deathCounter == 40 || deathCounter == 60 || deathCounter == 80) pic.Image = linkImage[11];
            if (deathCounter == 85)
            {
                for (int i = 0; i < view.Controls.Count; i++)
                {
                    view.Controls[i].BackColor = Color.Black;
                    view.Controls[i].BackgroundImage = null;
                }
                view.BackColor = Color.Black;
                view.Controls.Remove(view.c);
                pic.Image = linkImage[14];
            }
            if (deathCounter == 100)
            {
                Form1.music.settings.playCount = 1;
                Form1.music.URL = poink;
                pic.Image = linkImage[15];
            }
            if (deathCounter == 105) pic.Image = linkImage[16];
            if (deathCounter == 110)
            {
                Form1.music.controls.stop();
                pic.Image = null;
                pic.BackColor = Color.Transparent;
            }
            if (deathCounter == 115)
            {
                Form1.music.URL = gameover;
                Label lblGameOver = new()
                {
                    Font = new Font("Nintendo NES Font", 20),
                    Text = "GAME OVER",
                    Size = new Size(255,27)
                };
                lblGameOver.Location = new Point((view.Width / 2) - (lblGameOver.Width / 2), view.Height / 2);
                view.Controls.Add(lblGameOver);
                deathTimer.Enabled = false;
            }
            deathCounter++;

        }
        #endregion

    }
}
