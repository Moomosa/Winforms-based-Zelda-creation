using _225_Final.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public class GameView : Panel
    {
        public static List<PictureBox> Walls = new();
        public Link link;
        public Overworld world;
        public Control c;
        public Timer mainTimer = new();

        private int spawnTimerCount = 0;
        public int mapX { get; set; } = 1;
        public int mapY { get; set; } = 3;

        public GameView(Overworld overmap)
        {
            mainTimer.Interval = 20;
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Enabled = true;

            Size = new Size(768, 568);
            world = overmap;

            Character.characters.Add(link = new Link(new Point(360, 240), this));
            c = world.BigMap.GetControlFromPosition(mapX, mapY);
            Controls.Add(c);

            foreach (PictureBox pic in c.Controls)
                if ((string)pic.Tag == "Wall")
                    Walls.Add(pic);
        }

        private void MainTimer_Tick(object? sender, EventArgs e)
        {
            link.Movement();
            SpawnEnemy();
            ReachEdge();
        }

        private void SpawnEnemy()       //Creates new enemy where there is no walls
        {
            if (spawnTimerCount == 100 && Character.characters.Count <= 4)
            {
                Point newpoint = new Point(0, 0);
                for (int i = 0; i < Walls.Count; i++)
                    while (true)
                    {
                        newpoint = new Point(Form1.rng.Next(Width - 50) / 48 * 48, Form1.rng.Next(Height - 50) / 48 * 48);
                        Rectangle rectangle = new Rectangle(newpoint, new Size(48, 48));
                        if (!Walls.Any(x => x.Bounds.IntersectsWith(rectangle)))    //I don't fully understand the lambda
                            //break the loop when there isn't an overlapping rectangle found
                            break;
                    }
                Enemy octo = new Enemy(newpoint, this);
                octo.BringToFront();
                Character.characters.Add(octo);
                spawnTimerCount = 0;
            }
            else if (spawnTimerCount == 100 && Character.characters.Count >= 4)
                spawnTimerCount = 0;
            spawnTimerCount++;
        }

        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                link.leftStrength = 1;
            if (e.KeyCode == Keys.D)
                link.rightStrength = 1;
            if (e.KeyCode == Keys.W)
                link.upStrength = 1;
            if (e.KeyCode == Keys.S)
                link.downStrength = 1;

            if (e.KeyCode == Keys.L && !link.isAttacking)
                link.Attack();
        }

        private void View_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                link.leftStrength = 0;
            if (e.KeyCode == Keys.D)
                link.rightStrength = 0;
            if (e.KeyCode == Keys.W)
                link.upStrength = 0;
            if (e.KeyCode == Keys.S)
                link.downStrength = 0;
        }
        public void ReachEdge()
        {
            if (link.X > Width - 24)
            {
                link.X = -24;
                world.BigMap.Controls.Add(c, mapX, mapY);
                mapX++;
                ChangeMap();
            }
            if (link.X < -24)
            {
                link.X = Width - 24;
                world.BigMap.Controls.Add(c, mapX, mapY);
                mapX--;
                ChangeMap();
            }
            if (link.Y > Height - 24)
            {
                link.Y = -24;
                world.BigMap.Controls.Add(c, mapX, mapY);
                mapY++;
                ChangeMap();
            }
            if (link.Y < -24)
            {
                link.Y = Height - 24;
                world.BigMap.Controls.Add(c, mapX, mapY);
                mapY--;
                ChangeMap();
            }
        }

        public void ChangeMap()
        {
            link.pic.Left = link.X;
            link.pic.Top = link.Y;
            link.isMoving = false;
            Controls.Remove(c);
            c = new();
            c = world.BigMap.GetControlFromPosition(mapX, mapY);
            Controls.Add(c);
            for (int i = Character.characters.Count - 1; i >= 1; i--)
            {
                Character.characters[i].health = 0;
                Character.characters[i].Death();
            }

            Walls.Clear();
            foreach (PictureBox pic in c.Controls)
                if ((string)pic.Tag == "Wall")
                    Walls.Add(pic);
        }
    }
}
