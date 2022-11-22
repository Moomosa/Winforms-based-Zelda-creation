using System.Media;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public partial class Form1 : Form
    {
        public static Random rng = new Random();

        public static Form gameField;
        Link link;
        SoundPlayer player = new();
        Timer gameTimer = new();
        public static List<Image> swordList = new();
        private int spawnTimerCount = 0;

        public Form1()
        {
            InitializeComponent();
            gameField = this;
            player.SoundLocation = @"Sounds/Intro.wav";
            //player.PlayLooping();     //undo this commenting later
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Enabled = false;
            gameField.KeyDown += GameField_KeyDown;
            gameField.KeyUp += GameField_KeyUp;

            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword01.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword02.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword03.png"));
            swordList.Add(new Bitmap("Usable Sprites/Weapons/Sword04.png"));
        }


        private void GameField_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                link.leftStrength = 1;
            if (e.KeyCode == Keys.Right)
                link.rightStrength = 1;
            if (e.KeyCode == Keys.Up)
                link.upStrength = 1;
            if (e.KeyCode == Keys.Down)
                link.downStrength = 1;

            if (e.KeyCode == Keys.Z && !link.isAttacking)
                link.Attack();
        }

        private void GameField_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                link.leftStrength = 0;
            if (e.KeyCode == Keys.Right)
                link.rightStrength = 0;
            if (e.KeyCode == Keys.Up)
                link.upStrength = 0;
            if (e.KeyCode == Keys.Down)
                link.downStrength = 0;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (picTitle.Enabled == true && e.KeyChar == (char)Keys.Enter)
            {
                picTitle.Enabled = false;
                pnlTop.Visible = true;
                gameField.Controls.Remove(picTitle);
                player.Stop();
                Character.characters.Add(link = new Link(360, 480));
                gameTimer.Enabled = true;
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            link.Movement();
            if (spawnTimerCount == 100 && Character.characters.Count <= 4)
            {
                Enemy octo = new Enemy(rng.Next(gameField.Width - 50) / 48 * 48, rng.Next(gameField.Height - 50 + 196) / 48 * 48);
                Character.characters.Add(octo);
                spawnTimerCount = 0;
            }
            else if (spawnTimerCount == 100 && Character.characters.Count >= 4)
                spawnTimerCount = 0;
            spawnTimerCount++;
        }
    }
}