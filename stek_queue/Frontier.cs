using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stek_queue
{
    internal class Frontier
    {
        public PictureBox Show(int x, int y, Point location)
        {
            var strelka = new Bitmap("Frontier.png");
            var result = new PictureBox();
            result.Size = new Size(x, y);
            result.SizeMode = PictureBoxSizeMode.StretchImage;
            result.Image = strelka;
            result.Location = location;
            return result;
        }
        public PictureBox Show(int x, int y, Point location,int cur)
        {
            var strelka = new Bitmap($"Frontier{cur}.png");
            var result = new PictureBox();
            result.Size = new Size(x, y);
            result.SizeMode = PictureBoxSizeMode.StretchImage;
            result.Image = strelka;
            result.Location = location;

            return result;
        }
    }
}
