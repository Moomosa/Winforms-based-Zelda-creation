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
        Map03 map03 = new();
        Map13 map13 = new();
        Map23 map23 = new();
        Map33 map33 = new();

        Map02 map02 = new();
        Map12 map12 = new();
        Map22 map22 = new();
        Map32 map32 = new();

        Map01 map01 = new();
        Map11 map11 = new();
        Map21 map21 = new();
        Map31 map31 = new();

        Map00 map00 = new();
        Map10 map10 = new();
        Map20 map20 = new();
        Map30 map30 = new();

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

            BigMap.Controls.Add(map01, 0, 1);
            BigMap.Controls.Add(map11, 1, 1);
            BigMap.Controls.Add(map21, 2, 1);
            BigMap.Controls.Add(map31, 3, 1);

            BigMap.Controls.Add(map00, 0, 0);
            BigMap.Controls.Add(map10, 1, 0);
            BigMap.Controls.Add(map20, 2, 0);
            BigMap.Controls.Add(map30, 3, 0);

            foreach (UserControl map in BigMap.Controls)
                map.Dock = DockStyle.Fill;

            ClientSize = new Size(768, 528);
        }
    }
}
