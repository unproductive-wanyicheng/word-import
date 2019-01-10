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
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            chart1.Legends[0].Position = new ElementPosition(50, 10, 10, 30);
            chart1.Legends[0].BackColor = Color.Transparent;
            chart1.Titles.Add("喷射混凝土厚度(cm)");
            chart1.Titles[0].TextOrientation = TextOrientation.Horizontal;
            chart1.Titles[0].Docking = Docking.Left;
            //chart1.Titles[0]
            chart1.Titles[0].Position = new ElementPosition(0, 10, 20, 10);
            chart1.Series.Remove(chart1.Series["Series1"]);
            chart1.Series.Add("张三");
            chart1.Series.Add("李四");
            List<int> x = new List<int>() { 1, 2, 3, 4, 5 };

            List<int> y1 = new List<int>() { 10, 20, 30, 40, 50 };
            List<int> y2 = new List<int>() { 15, 15, 15, 15, 15 };

            chart1.Series["张三"].Points.DataBindXY(x, y1);
            chart1.Series["李四"].Points.DataBindXY(x, y2);

            chart1.Series["张三"].ChartType = SeriesChartType.Line;
            chart1.Series["张三"].Color = Color.FromArgb(0, 0, 128);

            chart1.Series["李四"].ChartType = SeriesChartType.Line;
            chart1.Series["李四"].Color = Color.Red;

            TLabel a = new TLabel()
            {
                Angle = -90
            };
            a.Text = "11111111111111111";
            a.Visible = true;
            this.Controls.Add(a);
        }
    }
}
