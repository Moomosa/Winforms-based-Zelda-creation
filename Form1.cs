using System.Media;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public partial class Form1 : Form
    {
        public static Form gameField;
        Link link;
        SoundPlayer player = new();
        Timer gameTimer = new();

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
                gameField.Controls.Remove(picTitle);
                player.Stop();
                Character.characters.Add(link = new Link(360, 480));
                gameTimer.Enabled = true;
            }
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            link.Movement();
        }
    }
}