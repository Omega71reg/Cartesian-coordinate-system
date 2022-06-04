using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int xMin = -10;
        private int xMax = 10;
        private int yMin = -10;
        private int yMax = 10;

        private int dx, dy;
        private int x0, y0;

        private int arrowSize = 7;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxXmin.Text, out xMin)) MessageBox.Show("Некорректное значение X min");
            if (!int.TryParse(textBoxXmax.Text, out xMax)) MessageBox.Show("Некорректное значение X max");
            if (!int.TryParse(textBoxYmin.Text, out yMin)) MessageBox.Show("Некорректное значение Y min");
            if (!int.TryParse(textBoxYmax.Text, out yMax)) MessageBox.Show("Некорректное значение Y max");
        

            panel1.Refresh();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            dx = panel1.Width / (xMax - xMin);
            dy = panel1.Height / (yMax - yMin);
            x0 = -dx * xMin;
            y0 = dy * yMax;
            
            // сетка вертикальная
            for(int x = xMin; x <= xMax; x++)
            {
                e.Graphics.DrawLine(Pens.LightGray, x0 + x * dx, 0, x0 + x * dx, panel1.Height);
            }
            // сетка горизонтальная
            for (int y = yMin; y <= yMax; y++)
            {
                e.Graphics.DrawLine(Pens.LightGray, 0, y0 - y * dy, panel1.Width, y0 - y * dy);
            }
            
            // ось абсцисс
            e.Graphics.DrawLine(Pens.Black, 0, y0, panel1.Width, y0);
            // ось ординат
            e.Graphics.DrawLine(Pens.Black, x0, 0, x0, panel1.Height);

            // стрелки
            e.Graphics.FillPolygon(Brushes.Black, new PointF[] { new PointF(panel1.Width, y0), new PointF(panel1.Width-arrowSize, y0 - arrowSize), new PointF(panel1.Width- arrowSize, y0 + arrowSize) });
            e.Graphics.FillPolygon(Brushes.Black, new PointF[] { new PointF(x0,0), new PointF(x0 - arrowSize, arrowSize), new PointF(x0 + arrowSize, arrowSize) });

            // подписи
            Font f = new Font("Arial", 10);
            e.Graphics.DrawString("Y", f, Brushes.Black, x0 + 10, 0);
            e.Graphics.DrawString("X", f, Brushes.Black, panel1.Width - 25, y0 + 10);
        }
    }
}
