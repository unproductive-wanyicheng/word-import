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
using System.Drawing.Drawing2D;

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

            // 创建侧线图
            Panel tableP = new Panel();
            tableP.Location = new Point(20, 20);
            tableP.Width = 1054;
            tableP.Height = 660;
            tableP.BackColor = Color.White;
            tableP.Paint += new PaintEventHandler(panel1_Paint1);

            Panel cexianPanel = new Panel();
            cexianPanel.Width = 1025;
            cexianPanel.Height = 616;
            cexianPanel.Location = new Point(25, 2);
            cexianPanel.BackColor = Color.Transparent;

            TLabel title_label = new TLabel()
            {
                Angle = -90
            };
            title_label.Text = "喷射混凝土厚度(cm)";
            title_label.Visible = true;
            title_label.Width = 15;
            title_label.Height = 200;
            title_label.Font = new Font("黑体", 9, FontStyle.Regular);
            title_label.Location = new Point(10, 200);
            //cexianPanel.Controls.Add(title_label);

            TLabel b = new TLabel();

            b.Text = "里 程";
            b.Visible = true;
            b.Width = 100;
            b.Height = 20;
            b.Font = new Font("黑体", 9, FontStyle.Regular);
            b.Location = new Point(450, 595);
            cexianPanel.Controls.Add(b);

            List<String> item = new List<String>() { "G测线", "E测线", "C测线", "A测线", "B测线", "D测线", "F测线" };
            List<String> x = new List<String>() { "K10+450", " ", "K10+450", " ", "K10+450", " ", "K10+450", " ", "K10+450", " ", " ", " ", " ", " ", " ", " ", " " };
            List<String> null_x = new List<String>() { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            List<String> y = new List<String>() { "25", "35", "30", "25", "35", "25", "35", "30", "25", "35", null, null, null, null, null, null, null };
            for(var i = 0; i < item.Count; i++)
            {
                var isLast = i == item.Count - 1 ? true : false;
                cexianPanel.Controls.Add(createChart(item[i],i, isLast, x, null_x, y));
            }

            

            int startW = 2;
            for(var i = 0; i < 11; i++)
            {
                string text = " ";
                Font font = new Font("黑体", 8);
                int width = 0;
                Label label = new Label();
                if (i == 0)
                {
                    text = "贵州省交通建设工程检测中心有限责任公司";
                    width = 270;
                }
                if (i == 1)
                {
                    text = "保泸高速土建S1合同";
                    width = 120;
                }
                if (i == 2)
                {
                    text = "老营特长隧道右幅进口喷射混凝土厚度图";
                    width = 241;
                }
                if (i == 3)
                {
                    text = "制图";
                    width = 60;
                }
                if (i == 3)
                {
                    text = "王宝强";
                    width = 70;
                }
                if (i == 4)
                {
                    text = "复核";
                    width = 50;
                }
                if (i == 5)
                {
                    text = "贾乃亮";
                    width = 60;
                }
                if (i == 6)
                {
                    text = "图号";
                    width = 50;
                }
                if (i == 7)
                {
                    text = "附图1";
                    width = 60;
                }
                if (i == 8)
                {
                    text = "日期";
                    width = 50;
                }
                if (i == 9)
                {
                    text = DateTime.Now.ToString("yyyy.MM.dd");
                    width = 80;
                }
                label.Location = new Point(startW, 620);
                startW += width;
                label.Text = text;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Font = font;
                label.Paint += new PaintEventHandler(panel1_Paint2);
                label.Width = width;
                label.Height = 38;
                tableP.Controls.Add(label);
            }
            tableP.Controls.Add(title_label);
            tableP.Controls.Add(cexianPanel);
            this.Controls.Add(tableP);
            // 绘图
            Bitmap newBitmap = new Bitmap(tableP.Width, tableP.Height);
            tableP.DrawToBitmap(newBitmap, tableP.ClientRectangle);
            Graphics g = Graphics.FromImage(newBitmap);
            g.DrawImage(newBitmap, newBitmap.Width, newBitmap.Height);
            g.Dispose();
            newBitmap.Save(Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "111.png"), System.Drawing.Imaging.ImageFormat.Png);
            newBitmap.Dispose();
        }

        private Chart createChart(String item, int index, bool isLast, List<string> x, List<string> null_x, List<string> y)
        {
            // 创建图表
            Chart A = new Chart();
            A.Width = 1085;
            A.Height = isLast ? 110 : 80;
            A.Location = new Point(-30, 80 * index);
            ChartArea area = new ChartArea("ChartArea");
            A.ChartAreas.Add(area);
            //A.ChartAreas[0].Position = new ElementPosition(0, 0, area.Position.Width, area.Position.Height);
            A.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            A.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
            A.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(191,  191, 191);

            A.Series.Add(item);

            //A.Legends.Add("E测线");
            //A.Legends[0].ForeColor = Color.FromArgb(191, 191, 191);
            //A.Legends[0].Position = new ElementPosition(50, 10, 7, 10);
            //A.Legends[0].TitleAlignment = StringAlignment.Center;
            //A.Legends[0].BackColor = Color.White;
            //A.Legends[0].LegendStyle = LegendStyle.Row;
            //A.Legends[0].Title = "E测线";
            //A.Legends[0].TitleFont = new Font("黑体", 9, FontStyle.Regular);
            // 不显示x轴

            A.ChartAreas[0].AxisX.IsMarginVisible = false;
            A.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            A.ChartAreas[0].AxisX.LabelStyle.Angle = 60;
            A.ChartAreas[0].AxisX.LabelStyle.Font = new Font("黑体", 7, FontStyle.Regular);
            A.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 6;
            A.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            A.ChartAreas[0].AxisX.IntervalOffset = 0;
            //A.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
                A.ChartAreas[0].AxisX.Interval = 1;
            if (isLast)
            {
                A.ChartAreas[0].AxisX.MajorTickMark.Enabled = true;
                A.ChartAreas[0].AxisX.MajorTickMark.Size = 3;
                A.ChartAreas[0].AxisX.LineWidth = 1;
                A.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(51, 51, 51);
            } else
            {
                A.ChartAreas[0].AxisX.LineColor = Color.FromArgb(191, 191, 191);
                A.ChartAreas[0].AxisX.MajorTickMark.Size = 0;
            }


            A.ChartAreas[0].AxisY.LineWidth = 1;
            A.ChartAreas[0].AxisY.Maximum = 70;
            A.ChartAreas[0].AxisY.Minimum = 10;
            A.ChartAreas[0].AxisY.Interval = 10;
            A.ChartAreas[0].AxisY.MajorTickMark.Size = 0.5f;
            A.ChartAreas[0].AxisY.IsLabelAutoFit = true;
            A.ChartAreas[0].AxisY.LabelStyle.Font = new Font("黑体", 7);

            A.Series[item].Points.DataBindXY(isLast ? x : null_x, y);

            A.Series[item].ChartType = SeriesChartType.Spline;
            A.Series[item].Color = Color.FromArgb(0, 0, 128);

            Label title = new Label();
            title.AutoSize = false;
            title.Font = new Font("黑体", 8, FontStyle.Regular);
            title.BackColor = Color.White;
            title.ForeColor = Color.Black;
            title.Width = 50;
            title.Height = 10;
            title.Text = item;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Location = new Point(500, 10);
            A.Controls.Add(title);

            Panel panel = new Panel();
            panel.Width = 540;
            panel.Height = 1;
            panel.Paint += new PaintEventHandler(panel1_Paint);
            panel.Location = new Point(72, 42);
            A.Controls.Add(panel);

            return A;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
            Color.Red, 0, ButtonBorderStyle.Dashed, //左边
                 Color.Red, 0, ButtonBorderStyle.Dashed, //上边
                 Color.Red, 0, ButtonBorderStyle.Dashed, //右边
                 Color.Red, 1, ButtonBorderStyle.Dashed);//底边
        }

        private void panel1_Paint1(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                Color.Black, 2, ButtonBorderStyle.Solid, //左边
                Color.Black, 2, ButtonBorderStyle.Solid, //上边
                Color.Black, 2, ButtonBorderStyle.Solid, //右边
                Color.Black, 2, ButtonBorderStyle.Solid);//底边
        }

        private void panel1_Paint2(object sender, PaintEventArgs e)
        {
            Label panel = sender as Label;
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                Color.Black, 0, ButtonBorderStyle.Solid, //左边
                Color.Gray, 1, ButtonBorderStyle.Solid, //上边
                Color.Black, 1, ButtonBorderStyle.Solid, //右边
                Color.Black, 0, ButtonBorderStyle.Solid);//底边
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
