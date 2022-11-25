using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _225_Final
{
    public partial class Map : UserControl
    {
        public Map gameMap;
        public Link link;
        public static List<PictureBox> Walls = new();

        private int spawnTimerCount = 0;
        public int mapX { get; set; } = 3;
        public int mapY { get; set; } = 1;
        private int X { get; set; }
        private int Y { get; set; }
        public Map(int x, int y)
        {
            InitializeComponent();
            gameMap = this;
            X = x;
            Y = y;
            gameMap.Left = X;
            gameMap.Top = Y;

            foreach (PictureBox pic in this.Controls)            
                if ((string)pic.Tag == "Wall")
                    Walls.Add(pic);            

            Character.characters.Add(link = new Link(new Point(360, 240), this));
            mainTimer.Enabled = true;
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Walls.Count; i++)
                if (link.pic.Bounds.IntersectsWith(Walls[i].Bounds))
                {
                    link.isMoving = false;
                    link.Wall();
                    break;
                }


            link.Movement();
            SpawnEnemy();
        }

        private void SpawnEnemy()       //Creates new enemy where there is no walls
        {
            if (spawnTimerCount == 100 && Character.characters.Count <= 4)
            {
                Point newpoint = new Point(0, 0);
                for (int i = 0; i < Walls.Count; i++)
                    while (true)
                    {
                        newpoint = new Point(Form1.rng.Next(gameMap.Width - 50) / 48 * 48, Form1.rng.Next(gameMap.Height - 50) / 48 * 48);
                        Rectangle rectangle = new Rectangle(newpoint, new Size(48, 48));
                        if (!Walls.Any(x => x.Bounds.IntersectsWith(rectangle)))    //I don't fully understand the lambda
                            //break the loop when there isn't an overlapping rectangle found
                            break;
                    }
                Enemy octo = new Enemy(newpoint, this);
                Character.characters.Add(octo);
                spawnTimerCount = 0;
            }
            else if (spawnTimerCount == 100 && Character.characters.Count >= 4)
                spawnTimerCount = 0;
            spawnTimerCount++;
        }

        private void Map_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                link.leftStrength = 1;
            if (e.KeyCode == Keys.D)
                link.rightStrength = 1;
            if (e.KeyCode == Keys.W)
                link.upStrength = 1;
            if (e.KeyCode == Keys.S)
                link.downStrength = 1;

            if (e.KeyCode == Keys.Z && !link.isAttacking)
                link.Attack();
        }

        private void Map_KeyUp(object sender, KeyEventArgs e)
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

    }
}
