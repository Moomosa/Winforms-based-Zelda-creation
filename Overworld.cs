using _225_Final.Maps;
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
    public partial class Overworld : UserControl
    {
        public int mapX { get; set; } = 1;
        public int mapY { get; set; } = 3;

        Map03 map03 = new();
        Map13 map13 = new();
        Map23 map23 = new();
        Map33 map33 = new();

        Map02 map02 = new();
        Map12 map12 = new();
        Map22 map22 = new();
        Map32 map32 = new();


        public Overworld()
        {
            InitializeComponent();

            BigMap.Controls.Add(map03, 0, 3);
            BigMap.Controls.Add(map13, 1, 3);
            BigMap.Controls.Add(map23, 2, 3);
            BigMap.Controls.Add(map33, 3, 3);

            BigMap.Controls.Add(map02, 0, 2);
            BigMap.Controls.Add(map12, 1, 2);
            BigMap.Controls.Add(map22, 2, 2);
            BigMap.Controls.Add(map32, 3, 2);


            map03.Dock = DockStyle.Fill;
            map13.Dock = DockStyle.Fill;
            map23.Dock = DockStyle.Fill;
            map33.Dock = DockStyle.Fill;

            map02.Dock = DockStyle.Fill;
            map12.Dock = DockStyle.Fill;
            map22.Dock = DockStyle.Fill;
            map32.Dock = DockStyle.Fill;



            ClientSize = new Size(768, 528);
        }
    }
}
