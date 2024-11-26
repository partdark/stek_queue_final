using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.LinkLabel;

namespace stek_queue
{
    internal static class settings
    {



        static Size size = new Size(890, 543);
        static FormStartPosition position = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation;
        public static Color backColor = SystemColors.Control;
       static string settingFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\LIFOFIFO\\settings.dck";


        static public void Reader(Form form)
        {
            if (!File.Exists(settingFile))
            {
                WriterDefault(form);
                return;
            }
            try
            {
                var s = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\LIFOFIFO\\settings.dck");

                string[] parts = s[0].Split(':', ',');

                int width = int.Parse(parts[1].Substring(parts[1].IndexOf('=') + 1));

                int height = int.Parse(parts[2].Substring(parts[2].IndexOf('=') + 1, parts[2].IndexOf('}') - parts[2].IndexOf('=') - 1));
                Size size = new Size(width, height);
                form.Size = size;

                parts = s[1].Split(':', ';');
                string startPositionString = parts[1];
                FormStartPosition startPosition;
                if (Enum.TryParse(startPositionString, out startPosition))
                {
                    form.StartPosition = startPosition;
                }


                string color = s[2].Substring(s[2].IndexOf('[') + 1, s[2].IndexOf(']') - s[2].IndexOf('[') - 1);
                if (color.Contains("="))
                {
                    string[] channels = color.Split(',');

                    int a = Int32.Parse(channels[0].Substring(channels[0].IndexOf('=') + 1));
                    int r = Int32.Parse(channels[1].Substring(channels[1].IndexOf('=') + 1));
                    int b = Int32.Parse(channels[2].Substring(channels[2].IndexOf('=') + 1));
                    int g = Int32.Parse(channels[3].Substring(channels[3].IndexOf('=') + 1));
                    form.BackColor = Color.FromArgb(a, r, b, g);
                }
                else form.BackColor = backColor;
                string positionY = s[3].Substring(s[3].IndexOf(':') + 1);
                string positionX = s[4].Substring(s[4].IndexOf(':') + 1);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = new Point(Int32.Parse(positionX), Int32.Parse(positionY));
            }
            catch
            {
                WriterDefault(form);
                return;
            }

        }

        static public void WriterDefault(Form form)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + $"\\LIFOFIFO");
            StreamWriter f = new StreamWriter(settingFile);


            f.WriteLine($"Size:{size};");
            f.WriteLine($"position:{position};");
            f.WriteLine($"backColor:{backColor}");
            f.WriteLine($"positionY:{40}");
            f.WriteLine($"positionX:{40}");
            f.Close();
        }

        static public void Writer(Form form)
        {
            StreamWriter f = new StreamWriter(settingFile);
            f.WriteLine($"Size:{form.Size};");
            f.WriteLine($"position:{form.StartPosition};");
            f.WriteLine($"backColor:{form.BackColor}");
            f.WriteLine($"positionY:{form.Top}");
            f.WriteLine($"positionX:{form.Left}");
            f.Close();

        }

    }

}
