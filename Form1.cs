using System.Media;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.LinkLabel;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public partial class Form1 : Form
    {
        public static Random rng = new Random();

        Overworld world = new();
        Link link;
        SoundPlayer player = new();
        GameView view;

        public Form1()
        {
            InitializeComponent();
            player.SoundLocation = @"Sounds/Intro.wav";
            AutoScroll = false;
            //player.PlayLooping();     //undo this commenting later
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
                player.Stop();
            }
            else { }
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
                link.Attack();

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