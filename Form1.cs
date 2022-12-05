using System.Media;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.LinkLabel;
using Timer = System.Windows.Forms.Timer;
using WMPLib;

namespace _225_Final
{
    public partial class Form1 : Form
    {
        public static Random rng = new Random();

        public static WindowsMediaPlayer music = new();
        WindowsMediaPlayer sounds = new();
        Overworld world = new();
        Link link;
        GameView view;
        string intro = @"Sounds/Intro.wav";
        string mainMusic = @"Sounds/Overworld.wav";
        string swordSound = @"Sounds/Sword.wav";
        string arrowShot = @"Sounds/Arrow.wav";
        Bitmap mainHud = new Bitmap("Usable Sprites/HUD/MainHud.png");

        public Form1()
        {
            InitializeComponent();

            music.URL = intro;
            music.settings.setMode("loop", true);
            music.controls.play();

        }


        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (picTitle.Enabled == true && e.KeyChar == (char)Keys.Enter)
            {
                pnlTop.Visible = true;
                view = new(world);
                view.Location = new Point(0, 192);
                Controls.Add(view);
                link = view.link;
                Controls.Remove(picTitle);
                music.URL = mainMusic;
                link.healthChange += HealthCheck;
            }
            else { }
        }

        private void HealthCheck(int currentHealth)
        {
            picCurrentHealth.Width = (int)(currentHealth * 11.5);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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
            {
                link.Attack();
                sounds.URL = swordSound;
                sounds.controls.play();
            }
            if (e.KeyCode == Keys.K && !link.isAttacking)
            {
                link.Shoot();
                sounds.URL = arrowShot;
                sounds.controls.play();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
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