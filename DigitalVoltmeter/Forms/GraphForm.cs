using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter.Forms
{
    public partial class GraphForm : Form
    {
        private double level = 0; double alpha = 0, beta = 0, gamma = 0, pi = Math.PI / 180;
        private Point startPosition;
        private Point movePosition = new Point(200, 150);

        public List<Point3D> Area { get; set; }

        public GraphForm()
        {
            InitializeComponent();
            MouseWheel += GraphForm_MouseWheel;
            RefreshGraph();
        }

        class Polygon
        {
            internal Point[] p = null;
            internal int[] z = null;

            public Polygon(Point3D point1, Point3D point2, Point3D point3, Point3D point4)
            {
                p = new Point[4]; z = new int[4];
                p[0] = new Point((int)point1.X, (int)point1.Y); z[0] = (int)point1.Z;
                p[1] = new Point((int)point2.X, (int)point2.Y); z[1] = (int)point2.Z;
                p[2] = new Point((int)point3.X, (int)point3.Y); z[2] = (int)point3.Z;
                p[3] = new Point((int)point4.X, (int)point4.Y); z[3] = (int)point4.Z;
            }

            public Polygon(Point3D point1, Point3D point2, Point3D point3)
            {
                p = new Point[3]; z = new int[3];
                p[0] = new Point((int)point1.X, (int)point1.Y); z[0] = (int)point1.Z;
                p[1] = new Point((int)point2.X, (int)point2.Y); z[1] = (int)point2.Z;
                p[2] = new Point((int)point3.X, (int)point3.Y); z[2] = (int)point3.Z;
            }

            public Polygon(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3, float X4, float Y4, float Z4)
            {
                p = new Point[4]; z = new int[4];
                p[0] = new Point((int)X1, (int)Y1); z[0] = (int)Z1;
                p[1] = new Point((int)X2, (int)Y2); z[1] = (int)Z2;
                p[2] = new Point((int)X3, (int)Y3); z[2] = (int)Z3;
                p[3] = new Point((int)X4, (int)Y4); z[3] = (int)Z4;
            }

            public Polygon(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3)
            {
                p = new Point[3]; z = new int[3];
                p[0] = new Point((int)X1, (int)Y1); z[0] = (int)Z1;
                p[1] = new Point((int)X2, (int)Y2); z[1] = (int)Z2;
                p[2] = new Point((int)X3, (int)Y3); z[2] = (int)Z3;
            }
        }

        #region x, y, R, sin, cos, RotationMatrix
        double sin(double x)
        {
            return Math.Sin(x * pi);
        }
        double cos(double x)
        {
            return Math.Cos(x * pi);
        }
        double l1()
        {
            return cos(alpha) * cos(gamma) - cos(beta) * sin(alpha) * sin(gamma);
        }
        double m1()
        {
            return sin(alpha) * cos(gamma) + cos(beta) * cos(alpha) * sin(gamma);
        }
        double n1()
        {
            return sin(beta) * sin(gamma);
        }
        double l2()
        {
            return -cos(alpha) * sin(gamma) + cos(beta) * sin(alpha) * cos(gamma);
        }
        double m2()
        {
            return -sin(alpha) * sin(gamma) + cos(beta) * cos(alpha) * cos(gamma);
        }
        double n2()
        {
            return sin(beta) * cos(gamma);
        }
        double l3()
        {
            return sin(beta) * sin(alpha);
        }
        double m3()
        {
            return -sin(beta) * cos(alpha);
        }
        double n3()
        {
            return cos(beta);
        }
        #endregion

        private List<double[]> SortArt(List<Polygon> polygons)
        {
            List<double[]> sort = new List<double[]>();
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].p.Length == 4)
                    sort.Add(new double[2] { (polygons[i].z[0] + polygons[i].z[1] + polygons[i].z[2] + polygons[i].z[3]) / 4, i });
                else sort.Add(new double[2] { (polygons[i].z[0] + polygons[i].z[1] + polygons[i].z[2]) / 3, i });
            sort.Sort((x, y) => x[0].CompareTo(y[0]));
            return sort;
        }

        private void RefreshGraph()
        {
            if (Area == null)
                return;
            
            double size = Math.Pow(2, level);
            List<Polygon> polygons = new List<Polygon>();
            Bitmap bmp = new Bitmap(Properties.Resources.background, pictureBox.Size);
            Graphics g = Graphics.FromImage(bmp);
            List<Point3D> pointsFor2D = new List<Point3D>(), circle_down = new List<Point3D>();

            for (int i = 0; i < Area.Count; i++)
            {
                pointsFor2D.Add(new Point3D((int)((Area[i].X * l1() + Area[i].Y * l2() + (Area[i].Z * 1000 * l3())) / size) + movePosition.X,
                                            (int)((Area[i].X * m1() + Area[i].Y * m2() + (Area[i].Z * 1000 * m3())) / size) + movePosition.Y,
                                            (int)((Area[i].X * n1() + Area[i].Y * n2() + (Area[i].Z * 1000 * n3())) / size)));
            }

            //for (int i = 0; i < pointsFor2D.Count; i++)
            //{
            //    polygons.Add(new Polygon(pointsFor2D[i++], pointsFor2D[i++], pointsFor2D[i++], pointsFor2D[i]));
            //}

            for (int i = 0; i < pointsFor2D.Count - 1; i++)
            {
                g.DrawLine(new Pen(Brushes.Black), pointsFor2D[i].X, pointsFor2D[i].Y, pointsFor2D[i + 1].X, pointsFor2D[i + 1].Y);
            }

            /* Как использовать художника
            List<double[]> order = SortArt(polygons);
            for (int i = 0; i < polygons.Count; i++)
            {
                order[i][0] = (order[i][0] < 0) ? 0 : order[i][0];
                    g.FillPolygon(new SolidBrush(Color.FromArgb(alfa.Value, 
                                                                red.Value + (int)((255 - red.Value) * order[i][0]) / 1500,
                                                                green.Value + +(int)((255 - green.Value) * order[i][0] / 1500),
                                                                blue.Value + +(int)((255 - blue.Value) * order[i][0]) / 1500)), 
                                                 polygons[(int)order[i][1]].p);
            }
            */

            pictureBox.Image = bmp;
            pictureBox.Refresh();
        }

        private void GraphForm_MouseWheel(object sender, MouseEventArgs e)
        {
            level += (e.Delta > 0) ? 0.1 : -0.1;
            if (level < -0.5d) level = -0.5d;
            else if (level > 3d) level = 3d;
            RefreshGraph();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((startPosition.Y - e.Y) < 0) beta -= 2;
                else if ((startPosition.Y - e.Y) > 0) beta += 2;
                if ((startPosition.X - e.X) < 0) gamma -= 2;
                else if ((startPosition.X - e.X) > 0) gamma += 2;
                if (beta > 359) beta = 0;
                else if (beta < 0) beta = 359;
                if (gamma > 359) gamma = 0;
                else if (gamma < 0) gamma = 359;
                RefreshGraph();
            }
            if (e.Button == MouseButtons.Right)
            {
                movePosition = e.Location;
                RefreshGraph();
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                startPosition = e.Location;
            if (e.Button == MouseButtons.Right)
            {
                startPosition = e.Location;
                movePosition = e.Location;
                RefreshGraph();
            }
        }
    }
}
