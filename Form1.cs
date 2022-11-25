using System.Media;
using System.Runtime.CompilerServices;
using Timer = System.Windows.Forms.Timer;

namespace _225_Final
{
    public partial class Form1 : Form
    {
        public static Random rng = new Random();

        Map gameMap;
        SoundPlayer player = new();

        public Form1()
        {
            InitializeComponent();
            player.SoundLocation = @"Sounds/Intro.wav";
            //player.PlayLooping();     //undo this commenting later
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (picTitle.Enabled == true && e.KeyChar == (char)Keys.Enter)
            {
                pnlTop.Visible = true;
                gameMap = new Map(0, pnlTop.Bottom);
                Controls.Add(gameMap);
                gameMap.Focus();
                Controls.Remove(picTitle);
                player.Stop();
            }
            else { }
        }
    }
}