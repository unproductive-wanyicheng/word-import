using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tunnel.Word.Test;
using System.IO;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class TLabel : Label
        {
            private float _angle;
            [Browsable(true)]
            [Description("The angle to rotate the text"), Category("Appearance"), DefaultValue("0")]
            public float Angle { get { return _angle; } set { _angle = value; Invalidate(); } }

            public TLabel()
            {
                this.AutoSize = false;
                this.BackColor = Color.Transparent;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                float w = Width;
                float h = Height;
                //将graphics坐标原点移到矩形中心点
                g.TranslateTransform(w / 2, h / 2);
                g.RotateTransform(Angle);
                SizeF sz = g.MeasureString(Text, this.Font);
                float x = -sz.Width / 2;
                float y = -sz.Height / 2;
                Brush brush = new SolidBrush(this.ForeColor);
                g.DrawString(Text, this.Font, brush, new PointF(x, y));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //UnitTest3 test = new UnitTest3();
            //UnitTest4 test = new UnitTest4();
            //UnitTest2 test = new UnitTest2();
            //UnitTest1 test = new UnitTest1();
            //test.TestTunnelMonth();
            //test.test();


            // http get请求
            //string strURL = "http://localhost/WinformSubmit.php?tel=11111&name=张三";
            //System.Net.HttpWebRequest request;
            //// 创建一个HTTP请求
            //request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            ////request.Method="get";
            //try
            //{
            //    System.Net.HttpWebResponse response;
            //    response = (System.Net.HttpWebResponse)request.GetResponse();
            //    System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //    string responseText = myreader.ReadToEnd();
            //    myreader.Close();
            //    MessageBox.Show(responseText);
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("连接远程服务器失败!");
            //    Console.Write("finished");
            //}

            

            // 创建图表
            ChartArea area2 = new ChartArea("ChartArea2");
            chart1.ChartAreas.Add(area2);
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            //chart1.ChartAreas[0].AxisX2.Enabled = AxisEnabled.True;
            //chart1.ChartAreas[0].AxisX2.LineColor = Color.Red;
            //chart1.ChartAreas[0].AxisX2.LineDashStyle = ChartDashStyle.Dash;
            //chart1.ChartAreas[0].AxisX2.

            chart1.Legends[0].Position = new ElementPosition(50, 5, 6, 5);
            chart1.Legends[0].BackColor = Color.White;
            chart1.Legends[0].LegendStyle = LegendStyle.Row;
            chart1.Legends[0].Title = "E测线";
            chart1.Legends[0].TitleFont = new Font("黑体", 9);
            //LegendCellColumn legend1 = chart1.Legends[0].CellColumns[1];
            //String s = chart1.Legends[0].LegendItemOrder;
            //Console.WriteLine(s);
            //legend1.Dispose();
            // 不显示x轴
            //chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LineWidth = 3;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 60;
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("黑体", 7);
            //chart1.ChartAreas[0].AxisX.ScaleBreakStyle.BreakLineStyle = BreakLineStyle.Straight;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            chart1.ChartAreas[0].AxisY.LineWidth = 2;
            chart1.ChartAreas[0].AxisY.Maximum = 70;
            chart1.ChartAreas[0].AxisY.Minimum = 10;
            chart1.ChartAreas[0].AxisY.Interval = 10;
            //chart1.Titles.Add("喷射混凝土厚度(cm)");
            //chart1.Titles[0].TextOrientation = TextOrientation.Horizontal;
            //chart1.Titles[0].Docking = Docking.Left;
            //chart1.Titles[0].Position = new ElementPosition(0, 10, 20, 10);
            chart1.Series.Remove(chart1.Series["Series1"]);
            chart1.Series.Add("张三");
            chart1.Series.Add("李四");
            //chart1.Series[1].SmartLabelStyle.CalloutLineDashStyle = ChartDashStyle.Dash;
            //chart1.Series[1].IsXValueIndexed = true;

            List<String> x = new List<String>() { "K10+450", " ", "K10+450", " ", "K10+450", " ", "K10+450", " ", "K10+450", " " };

            List<int> y1 = new List<int>() { 25, 35, 30, 25, 35, 25, 35, 30, 25, 35 };
            List<int> y2 = new List<int>() { 30, 30, 30, 30, 30, 30, 30, 30, 30, 30 };

            chart1.Series["张三"].Points.DataBindXY(x, y1);
            chart1.Series["李四"].Points.DataBindXY(x, y2);

            chart1.Series["张三"].ChartType = SeriesChartType.Spline;
            chart1.Series["张三"].Color = Color.FromArgb(0, 0, 128);

            chart1.Series["李四"].ChartType = SeriesChartType.Line;
            chart1.Series["李四"].Color = Color.Red;

            TLabel a = new TLabel()
            {
                Angle = -90
            };
            a.Text = "喷射混凝土厚度(cm)";
            a.Visible = true;
            a.Width = 30;
            a.Height = 200;
            a.Location = new Point(50, 100);
            this.Controls.Add(a);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Color.Red, 5);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 5f, 5f };
            Graphics gh = this.CreateGraphics();
            gh.DrawLine(pen, 10, 50, 150, 50);
        }
    }
}
