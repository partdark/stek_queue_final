
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Umover_queue
{
    public class Umover
    {

        public int t = 0;
        public int s = 0;


        public bool CheckStatus(List<bool> _done) //проверка на наличие незаконченных асинхронных методов
        {
            if (_done.Contains(false))
            {
                var threadMessage = new Thread(() => MessageBox.Show("Подождите завершения задачи."));
                threadMessage.Start();
                return true;
            }
            return false;
        }
        public void ClearUmover(Panel panel, System.Windows.Forms.Label label, List<Button> umover, List<string> umover_string, List<bool> umover_done) //очистка стека с полным удалением всей информации
        {

            panel.Controls.Clear();
            umover.Clear();
            umover_string.Clear();
            umover_done.Clear();
            label.Text = "Стек не создан";
        }

        public async void creator(string[] str, Panel panel, List<Button> umover, List<string> umover_string, List<bool> umover_done) //создание элемента и расположение на элементе panel
        {
            var c = umover.Count;
            for (int i = 0; i < str.Length; i++)
            {
                umover_string.Add(str[i]);
                umover_done.Add(false);
                umover.Add(new Button
                {
                    Location = new Point(1, 100),
                    Text = str[i],
                    Size = new Size(panel.Size.Width / 14, panel.Size.Height / 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left,
                    Font = new Font("Microsoft Sans Serif", panel.Size.Height / 22),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Black,
                    ForeColor = Color.White,


                }
                    );
                panel.Controls.Add(umover[umover.Count - 1]);
            }
            animations(panel, umover, umover_string, umover_done, c);
        }

        public async void loader(string[] str, Panel panel, List<Button> umover, List<string> umover_string, List<bool> umover_done) //создание элемента и расположение на элементе panel
        {

            for (int i = 0; i < str.Length; i++)
            {
                umover_string.Add(str[i]);
                umover_done.Add(true);
                umover.Add(new Button
                {
                    Location = new Point(1, 100),
                    Text = str[i],
                    Size = new Size(panel.Size.Width / 14, panel.Size.Height / 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left,
                    Font = new Font("Microsoft Sans Serif", panel.Size.Height / 22),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Black,
                    ForeColor = Color.White,


                }
                    );
                panel.Controls.Add(umover[umover.Count - 1]);
            }
            Pos(panel, umover);
        }
        public async void animations(Panel panel, List<Button> umover, List<string> umover_string, List<bool> umover_done, int c) //задание конечных значений для элементов
        {
            var x = panel.Size.Width;
            var y = panel.Size.Height;
            var count = umover.Count;
            for (var i = 0; i < umover.Count; i++)
            {

                move(umover[i].PointToScreen(Point.Empty), new Point((int)(x - (x / (count + 1) * (i + 1))) - umover[i].Size.Width, (int)(y / 2) - umover[i].Size.Height), umover[i], i, panel,
                    umover, umover_string, umover_done);
                if (i >= c)
                    await Task.Delay(800);

            }
        }


        public async void move(Point from, Point to, Button button, int i, Panel panel, List<Button> umover,
            List<string> umover_string, List<bool> umover_done) //движение элементов к конечным значениям
        {
            umover_done[i] = false;
            while (Math.Abs(button.Location.Y - to.Y) > panel.Size.Height / 10)
            {
                if (button.Location.Y < to.Y)

                    await Task.Delay(t);

                button.Location = new Point(button.Location.X, button.Location.Y + (button.Location.Y < to.Y ? +s : -s));

            }
            while (Math.Abs(button.Location.Y - to.Y) > 2)
            {
                if (button.Location.Y < to.Y)

                    await Task.Delay(1);

                button.Location = new Point(button.Location.X, button.Location.Y + (button.Location.Y < to.Y ? +1 : -1));

            }


            while (Math.Abs(button.Location.X - to.X) > panel.Size.Width / 15)
            {
                await Task.Delay(t);
                button.Location = new Point(button.Location.X + (button.Location.X < to.X ? +s : -s), button.Location.Y);

            }

            while (Math.Abs(button.Location.X - to.X) > 2)
            {
                await Task.Delay(1);
                button.Location = new Point(button.Location.X + (button.Location.X < to.X ? +1 : -1), button.Location.Y);

            }
            umover_done[i] = true;
            var x = panel.Size.Width;
            var y = panel.Size.Height;
            var count = umover.Count;
            await Task.Delay((i + 1) * 200);
            button.Location = new Point((int)(x - (x / (count + 1) * (i + 1))) - umover[i].Size.Width, (int)(y / 2) - umover[i].Size.Height);
        }

        public void Pos(Panel panel, List<Button> umover) //изменения положения элементов при изменении размера окна
        {
            var x = panel.Size.Width;
            var y = panel.Size.Height;
            if (x > 0 && y > 0)
            {
                var count = umover.Count;
                for (var i = umover.Count - 1; i >= 0; i--)
                {
                    umover[i].Size = new Size(panel.Size.Width / 14, panel.Size.Height / 10);
                    umover[i].Location = new Point((int)(x - (x / (count + 1) * (i + 1))) - umover[i].Size.Width, (int)(y / 2) - umover[i].Size.Height);
                    umover[i].Font = new Font("Microsoft Sans Serif", panel.Size.Height / 22);
                }
            }
        }

        public async Task rdAn(int a, Panel panel, List<Button> umover, List<string> umover_string, List<bool> umover_done, Form form) //удаление элементов из стека
        {


            await Task.Delay(1);
            {

                while (umover_done.Contains(false))
                {

                    await Task.Delay(1);
                }
                for (int i = 0; i < a; i++)
                {
                    panel.Controls.Remove(umover[umover.Count - 1]);
                    umover.RemoveAt(umover.Count - 1);
                    umover_done.RemoveAt(umover_done.Count - 1);
                    umover_string.RemoveAt(umover_string.Count - 1);

                }

                animationsD(panel, umover, umover_string, umover_done);
            }
        }
        public void animationsD(Panel panel, List<Button> umover, List<string> umover_string, List<bool> umover_done) //анимации для движения созданных элементов (которые до нажатия кнопки создания уже существовали)
        {
            var x = panel.Size.Width;
            var y = panel.Size.Height;
            var count = umover.Count;
            for (var i = umover.Count - 1; i >= 0; i--)
                move(umover[i].PointToScreen(Point.Empty), new Point((int)(x - (x / (count + 1) * (i + 1))) - umover[i].Size.Width,
                    (int)(y / 2) - umover[i].Size.Height), umover[i], i, panel, umover, umover_string, umover_done);
        }
        public async void moveDown(Point from, Point to, Button button, int i, int eof, Panel panel,
            List<Button> umover, List<string> umover_string, List<bool> umover_done) // анимации удаления элементов
        {
            umover_done[i] = false;


            while (Math.Abs(button.Location.Y - to.Y) > panel.Size.Height / 10)
            {
                if (button.Location.Y < to.Y)

                    await Task.Delay(t);

                button.Location = new Point(button.Location.X, button.Location.Y + (button.Location.Y < to.Y ? +s : -s));

            }

            while (Math.Abs(button.Location.X - to.X) > panel.Size.Width / 15)
            {
                await Task.Delay(t);
                button.Location = new Point(button.Location.X + (button.Location.X < to.X ? +s : -s), button.Location.Y);

            }

            umover_done[i] = true;

        }
    }
    public class Stek : Umover
    {



        public void ClearStek(Panel panel, System.Windows.Forms.Label label, List<Button> stek, List<string> stek_string, List<bool> stek_done) //очистка стека с полным удалением всей информации
        {

            panel.Controls.Clear();
            stek.Clear();
            stek_string.Clear();
            stek_done.Clear();
            label.Text = "Стек не создан";
        }
    }

    internal class Queue : Umover
    {

        public void ClearQueue(Panel panel, System.Windows.Forms.Label label, List<Button> queue, List<string> queue_string, List<bool> queue_done)
        {

            panel.Controls.Clear();
            queue.Clear();
            queue_string.Clear();
            queue_done.Clear();
            label.Text = "Очередь не создана";
        }
        public bool Check(List<bool> _done)
        {
            if (_done.Contains(false))
            {
                var threadMessage = new Thread(() => MessageBox.Show("Подождите завершения задачи."));
                threadMessage.Start();
                return true;
            }
            return false;
        }

        public async Task rdAn(int a, Panel panel, List<Button> queue, List<string> queue_string, List<bool> queue_done, Form form)
        {
            await Task.Delay(1);
            {

                while (queue_done.Contains(false))
                {

                    await Task.Delay(1);
                }
                for (int i = 0; i < a; i++)
                {
                    panel.Controls.Remove(queue[0]);
                    queue.RemoveAt(0);
                    queue_done.RemoveAt(0);
                    queue_string.RemoveAt(0);

                }

                animationsD(panel, queue, queue_string, queue_done);
            }
        }

    }


    internal class Deque : Umover
    {

        public void ClearDeque(Panel panel, System.Windows.Forms.Label label, List<Button> Deque, List<string> Deque_string, List<bool> Deque_done)
        {

            panel.Controls.Clear();
            Deque.Clear();
            Deque_string.Clear();
            Deque_done.Clear();
            label.Text = "Дек не создан";
        }
        public bool check(List<bool> _done)
        {
            if (_done.Contains(false))
            {
                var threadMessage = new Thread(() => MessageBox.Show("Подождите завершения задачи."));
                threadMessage.Start();
                return true;
            }
            return false;
        }

        public async void creator(string[] str, Panel panel, List<Button> Deque, List<string> Deque_string, List<bool> Deque_done, bool a)
        {
            var c = Deque.Count;
            for (int i = 0; i < str.Length; i++)
            {
                Deque_string.Insert(0, str[i]);
                Deque_done.Insert(0, false);
                Deque.Insert(0, new Button
                {
                    Location = new Point(panel.Width - 15 - panel.Size.Width / 14, 100),
                    Text = str[i],
                    Size = new Size(panel.Size.Width / 14, panel.Size.Height / 10),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left,
                    Font = new Font("Microsoft Sans Serif", panel.Size.Height / 22),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Black,
                    ForeColor = Color.White,


                }
                    );
                panel.Controls.Add(Deque[0]);
            }
            animations(panel, Deque, Deque_string, Deque_done, false, c);
        }
        public async void animations(Panel panel, List<Button> Deque, List<string> Deque_string, List<bool> Deque_done, bool a, int c)
        {
            var x = panel.Size.Width;
            var y = panel.Size.Height;
            var count = Deque.Count;
            for (var i = Deque.Count - 1; i >= 0; i--)
            {

                move(Deque[i].PointToScreen(Point.Empty), new Point((int)(x - (x / (count + 1) * (i + 1))) - Deque[i].Size.Width, (int)(y / 2) - Deque[i].Size.Height), Deque[i], i, panel, Deque, Deque_string, Deque_done);
                if (i >= c)
                    await Task.Delay(800);

            }
        }

    }

}
