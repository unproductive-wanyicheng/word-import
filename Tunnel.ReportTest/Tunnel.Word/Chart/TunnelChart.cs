using System;
using System.Collections.Generic;
using System.Drawing;
using C1.Win.C1Chart;
using HtTool.Service;
using HtTool.Service.Monitor;

namespace Tunnel.Word.Chart
{
    public class TunnelChart
    {
        public List<PointData> PointDatas { get; set; }

        /// <summary>
        /// 所有截面
        /// </summary>
        public List<Section> Sections = new List<Section>();

        /// <summary>
        /// 当前选中截面
        /// </summary>
        public Section CurrentSection { get; set; }

        /// <summary>
        /// 对数参数
        /// </summary>
        public List<CurvePara> LogParas { get; set; }

        /// <summary>
        /// 指数参数
        /// </summary>
        public List<CurvePara> ExpParas { get; set; }

        /// <summary>
        /// 双曲线参数
        /// </summary>
        public List<CurvePara> TangParas { get; set; }

        /// <summary>
        /// 多项式参数
        /// </summary>
        public List<CurvePara> FunctionParas { get; set; }
        public C1Chart c1Chart1 = new C1Chart();

        int iAB = 1;  //缺省时应选择这些曲线
        int iAC = 1;
        int iBC = 1;

        int iAD = 1;
        int iAE = 1;
        int iDE = 1;
        /// <summary>
        /// 回归函数类型
        /// </summary>
        public int iFunctionType = 1;

        public int iLineType = 1;//缺省时画位移时间曲线

        public int iShowLine = 0;//是否显示测线
        public int iShowFunction = 0;//是否显示回归函数

        public int iAnalysis = 0; //是否是点击分析进入的
        public int iResult = 0;

        /// <summary>
        /// 选中的线加粗
        /// </summary>
        int selectedIndex = -1;


        Color selectionColor = Color.Red;
        // Series colors
        Color[] clrs = { Color.Blue, Color.DarkGreen, Color.Brown, Color.Aqua, Color.Yellow, Color.Salmon, Color.MediumPurple, Color.Orange, Color.Tomato, Color.RoyalBlue, Color.Magenta, Color.PaleGreen };

        Random rnd = new Random();

        double[] k1 = { 0, 0, 0, 0, 0, 0, 0 }; //多项式回归函数的系数
        int ik1 = 0;
        double[] k2 = { 0, 0, 0, 0, 0, 0, 0 };
        int ik2 = 0;
        double[] k3 = { 0, 0, 0, 0, 0, 0, 0 };
        int ik3 = 0;
        double[] k4 = { 0, 0, 0, 0, 0, 0, 0 };
        int ik4 = 0;
        double[] k5 = { 0, 0, 0, 0, 0, 0, 0 };
        int ik5 = 0;
        double[] k6 = { 0, 0, 0, 0, 0, 0, 0 };
        int ik6 = 0;

        double a1, a2, a3, a4, a5, a6, b1, b2, b3, b4, b5, b6; //回归曲线参数
        int ia1 = 0;
        int ia2 = 0;
        int ia3 = 0;
        int ia4 = 0;
        int ia5 = 0;
        int ia6 = 0;

        double r1ab = 0; double r1ac = 0; double r1bc = 0; double r1ad = 0; double r1ae = 0; double r1de = 0; //相关指数
        double r2ab = 0; double r2ac = 0; double r2bc = 0; double r2ad = 0; double r2ae = 0; double r2de = 0; //相关指数
        double r3ab = 0; double r3ac = 0; double r3bc = 0; double r3ad = 0; double r3ae = 0; double r3de = 0; //相关指数
        double r4ab = 0; double r4ac = 0; double r4bc = 0; double r4ad = 0; double r4ae = 0; double r4de = 0; //相关指数

        public Engine Engine = new Engine();

        public string _dstt = "";
        public string DataSeriesTooltipText
        {
            get { return _dstt; }
            set
            {
                if (_dstt != value)
                {
                    _dstt = value;

                    // Set tooltip for all series
                    foreach (ChartDataSeries ds in c1Chart1.ChartGroups[0].ChartData.SeriesList)
                        ds.TooltipText = _dstt;
                }
            }
        }

        public void DataChartLoad()
        {
            DataSeriesTooltipText = "Series: {#TEXT}\r\nx = {#XVAL}\r\ny = {#YVAL}";
            c1Chart1.Header.TooltipText = "This is header tooltip.\r\nSecond line.";
            c1Chart1.Header.Text = "This is header header";
            c1Chart1.Footer.Text = "This is footer footer";

            c1Chart1.ToolTip.Enabled = true;

            FirstInit();

        }
        private void FirstInit()
        {

            iAB = 1;
            iAC = 1;
            iBC = 1;

            if (CurrentSection != null && CurrentSection.lines.Count == 6)
            {
                iAD = 1;
                iAE = 1;
                iDE = 1;
            }
            else
            {
                iAD = 0;
                iAE = 0;
                iDE = 0;
            }


            iFunctionType = 1;



            iLineType = 1;




            iShowLine = 1;



            iShowFunction = 0;


        }
        /// <summary>
        /// 根据初始值初始化控件
        /// </summary>
        public void InitChart()
        {
            if (CurrentSection.lines.Count == 6)
            {
                iAB = 1;
                iAC = 1;
                iBC = 1;
                iAD = 1;
                iAE = 1;
                iDE = 1;
            }
            else
            {
                iAB = 1;
                iAC = 1;
                iBC = 1;
                iAD = 0;
                iAE = 0;
                iDE = 0;
            }


            iShowLine = 1;

            iShowFunction = 0;

            selectedIndex = -1;

            RedrawFunction();

        }

        /// <summary>
        /// 重绘chart
        /// </summary>
        public void RedrawFunction()
        {
            if (!string.IsNullOrEmpty(CurrentSection.EquipId))
                SetXyRtChart();
            else
                SetXyChart();


            c1Chart1.ChartGroups[0].ChartData.FunctionsList.Clear();
            //ChartData cd2 = c1Chart2.ChartGroups[0].ChartData;


            if ((iShowLine == 1) && (iShowFunction == 0))
                c1Chart1.ChartArea.AxisY.Text = "累计位移(mm)";
            else if ((iShowLine == 0) && (iShowFunction == 1))
            {
                if (iLineType == 1)
                    c1Chart1.ChartArea.AxisY.Text = "位移(mm)";
                if (iLineType == 2)
                    c1Chart1.ChartArea.AxisY.Text = "速度(mm/d)";
                if (iLineType == 3)
                    c1Chart1.ChartArea.AxisY.Text = "加速度(mm/d2)";
            }
            else if ((iShowLine == 1) && (iShowFunction == 1))
            {
                if (iLineType == 1)
                    c1Chart1.ChartArea.AxisY.Text = "位移(mm)";
                else
                    c1Chart1.ChartArea.AxisY.Text = "";

            }
            else
                c1Chart1.ChartArea.AxisY.Text = "";


            if (iShowLine == 1) //是否显示测线
            {
                ShowData();    //显示测线函数
            }



            if (iShowFunction == 1) //是否显示回归曲线
            {
                //对每种回归曲线，先计算所有参数，然后根据选择确定显示哪一条回归曲线
                if (PointDatas.Count > 0)
                {
                    for (int j = 0; j < PointDatas.Count; j++)
                    {
                        var pdata = PointDatas[j];
                        if (iFunctionType == 1) //对数
                        {
                            LogParas = MonitorService.CalculateLogParameters(PointDatas);

                            if (iLineType == 1)  //累计位移
                            {
                                if (pdata.IsShow)
                                {
                                    var logpara = LogParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(logpara.A + "+" + logpara.B + "*Math.Log(x)", pdata.Name, pdata.Data);
                                }
                            }
                            if (iLineType == 2) //速度
                            {
                                if (pdata.IsShow)
                                {
                                    var logpara = LogParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(logpara.B + "/x", pdata.Name, pdata.Data);
                                }

                            }
                            if (iLineType == 3) //加速度
                            {
                                if (pdata.IsShow)
                                {
                                    var logpara = LogParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(logpara.B + "/(-x*x)", pdata.Name, pdata.Data);
                                }

                            }

                        }
                        if (iFunctionType == 2)//指数函数
                        {
                            ExpParas = MonitorService.CalculateExpParameters(PointDatas);

                            if (iLineType == 1) //累计位移
                            {
                                if (pdata.IsShow)
                                {
                                    var expPara = ExpParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(expPara.A + "*Math.Exp(" + expPara.B + "*x)", pdata.Name, pdata.Data);
                                }

                            }
                            if (iLineType == 2) //速度
                            {
                                if (pdata.IsShow)
                                {
                                    var expPara = ExpParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(expPara.A + "*" + expPara.B + "*Math.Exp(" + expPara.B + "*x)", pdata.Name, pdata.Data);
                                }

                            }
                            if (iLineType == 3) //加速度
                            {
                                if (pdata.IsShow)
                                {
                                    var expPara = ExpParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(expPara.A + "*" + expPara.B + "*" + expPara.B + "*Math.Exp(" + expPara.B + "*x)", pdata.Name, pdata.Data);
                                }

                            }
                        }
                        if (iFunctionType == 3)//双曲函数
                        {
                            TangParas = MonitorService.CalculateTangParameters(PointDatas);

                            if (iLineType == 1) //位移
                            {
                                if (pdata.IsShow)
                                {
                                    var tangPara = TangParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(tangPara.A + "+" + tangPara.B + "/x", pdata.Name, pdata.Data);
                                }
                            }
                            if (iLineType == 2)//速度
                            {
                                if (pdata.IsShow)
                                {
                                    var tangPara = TangParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(tangPara.B + "/(-x*x)", pdata.Name, pdata.Data);
                                }
                            }
                            if (iLineType == 3)//加速度
                            {
                                if (pdata.IsShow)
                                {
                                    var tangPara = TangParas.Find(p => p.Name == pdata.Name);
                                    AddNewFunction(tangPara.B + "/(0.5*x*x*x)", pdata.Name, pdata.Data);
                                }
                            }

                        }
                        if (iFunctionType == 4) //多项式函数
                        {
                            FunctionParas = MonitorService.CalculateFunctionParameters(c1Chart1, PointDatas);

                            if (iLineType == 1) //位移
                            {
                                if (pdata.IsShow)
                                {
                                    var functionPara = FunctionParas.Find(p => p.Name == pdata.Name);
                                    FunctionPoint(functionPara.K, pdata.Name, pdata.Data);
                                }
                            }
                            if (iLineType == 2)//速度
                            {
                                if (pdata.IsShow)
                                {
                                    var functionPara = FunctionParas.Find(p => p.Name == pdata.Name);
                                    FunctionPointV(functionPara.K, pdata.Name, pdata.Data);
                                }

                            }
                            if (iLineType == 3)//加速度
                            {
                                if (pdata.IsShow)
                                {
                                    var functionPara = FunctionParas.Find(p => p.Name == pdata.Name);
                                    FunctionPointA(functionPara.K, pdata.Name, pdata.Data);
                                }
                            }

                        }
                    }
                }


            }

        }

        /// <summary>
        /// 设置chart的基本信息
        /// </summary>
        public void SetXyChart()
        {
            c1Chart1.ChartGroups[0].ChartData.SeriesList.Clear();

            Point pLoc = new Point();
            pLoc.X = -1;
            pLoc.Y = -1;
            c1Chart1.Reset();

            c1Chart1.ChartArea.ResetSizeDefault();
            c1Chart1.ChartArea.ResetStyle();
            //  c1Chart1.Style.sho

            c1Chart1.ChartArea.AxisX.GridMajor.Visible = true;
            c1Chart1.ChartArea.AxisY.GridMajor.Visible = true;

            c1Chart1.ToolTip.Enabled = true;


            c1Chart1.BackColor = Color.White;


            c1Chart1.Legend.Compass = CompassEnum.East;
            c1Chart1.Legend.Location = pLoc;
            c1Chart1.Legend.Visible = true;
            c1Chart1.ToolTip.Enabled = true;

            c1Chart1.ToolTip.PlotElement = PlotElementEnum.Series;
            c1Chart1.ToolTip.SelectAction = SelectActionEnum.MouseOver;

            c1Chart1.ChartGroups[0].ChartData.SeriesList.Clear();

            c1Chart1.ChartGroups[0].ChartType = C1.Win.C1Chart.Chart2DTypeEnum.XYPlot;


            c1Chart1.ChartArea.AxisX.ScrollBar.Visible = false;


            //设置图片的显示方式
            C1Chart chart = c1Chart1;
            Area area = chart.ChartArea;
            Axis ax = area.AxisX;
            ax.AnnotationRotation = -30;

            C1.Win.C1Chart.ValueLabel vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();

            //C1FlexGrid c1Flex = (C1FlexGrid)_tab.TabPages[_tab.SelectedIndex].Controls[0];
            var lines = CurrentSection.lines;
            string sDate = "";
            string eDate = "";

            foreach (var line in lines)
            {
                if (line.dataTable != null && line.dataTable.Rows.Count > 0)
                {
                    var tsDate = line.dataTable.Rows[0][0].ToString();
                    var teDate = line.dataTable.Rows[line.dataTable.Rows.Count - 1][0].ToString();
                    var ti = line.dataTable.Rows.Count - 1;
                    while (string.IsNullOrEmpty(teDate))
                    {
                        ti--;
                        teDate = line.dataTable.Rows[ti][0].ToString();
                    }
                    if (sDate == "" || eDate == "")
                    {
                        sDate = tsDate;
                        eDate = teDate;
                    }
                    else
                    {
                        if (DateTime.Parse(sDate) > DateTime.Parse(tsDate))
                        {
                            sDate = tsDate;
                        }
                        if (DateTime.Parse(teDate) > DateTime.Parse(eDate))
                        {
                            sDate = tsDate;
                        }
                    }
                }
            }

            string strPreDate = "";
            string fmt = "yyyy.MM.dd";
            if (sDate == "" || eDate == "")
            {
                return;
            }
            string strStartDate = sDate;
            string strEndDate = eDate;

            //第一天和最后一天都应该标注

            vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
            vlbl.NumericValue = 0;
            vlbl.Text = DateTime.Parse(strStartDate).ToString(fmt);

            int iDays = Convert.ToDateTime(DateTime.Parse(strEndDate).ToString("yyyy-MM-dd")).Subtract(Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd"))).Days;
            if (iDays < 32) //一个月以内，每天都标注
            {
                for (int i = 1; i < iDays; i++)
                {
                    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                    vlbl.NumericValue = i * 1; //修改前为50
                    vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(i).ToString(fmt);
                }

            }
            else if (iDays < 93) ////三个月以内，10天标注一次
            {
                for (int i = 10; i < iDays; i += 10)
                {
                    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                    vlbl.NumericValue = i * 1; //修改前为50
                    vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(i).ToString(fmt);
                }

            }
            else if (iDays < 186) ////六个月以内，15天标注一次
            {
                for (int i = 15; i < iDays; i += 15)
                {
                    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                    vlbl.NumericValue = i * 1; //修改前为50
                    vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(i).ToString(fmt);
                }
            }
            else if (iDays < 731) ////两年以内，一个月标注一次
            {
                for (int i = 30; i < iDays; i += 30)
                {
                    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                    vlbl.NumericValue = i * 1; //修改前为50
                    vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(i).ToString(fmt);
                }

            }
            else //两年以上，2个月标注一次
            {
                for (int i = 60; i < iDays; i += 60)
                {
                    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                    vlbl.NumericValue = i * 1; //修改前为50
                    vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(i).ToString(fmt);
                }
            }



            ////第一天和最后一天都应该标注

            //vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
            //vlbl.NumericValue = iDays * 1; //修改前为50
            //vlbl.Text = Convert.ToDateTime(DateTime.Parse(strStartDate).ToString("yyyy-MM-dd")).AddDays(iDays).ToString(fmt);

            ////首先得到开始时间这个月还剩多少天
            //int iFirstLeft = DateTime.DaysInMonth(Convert.ToDateTime(strStartDate.Substring(0, 10)).Year, Convert.ToDateTime(strStartDate.Substring(0, 10)).Month) - Convert.ToDateTime(strStartDate.Substring(0, 10)).Day;

            ////下个月第一天
            //string strFirstDay = Convert.ToDateTime(strStartDate.Substring(0, 10)).AddMonths(1).ToString(fmt).Substring(0, 7) + ".01";

            ////判定有没有到最后一个月
            //int iMonthCount = DateTime.Compare(Convert.ToDateTime(strFirstDay), Convert.ToDateTime(strEndDate.Substring(0, 10)));


            //vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
            //vlbl.NumericValue = iFirstLeft * 50;
            //vlbl.Text = strFirstDay;

            //while (iMonthCount < 0)
            //{
            //    strPreDate = strFirstDay;
            //    strFirstDay = Convert.ToDateTime(strPreDate.Substring(0, 10)).AddMonths(1).ToString(fmt).Substring(0, 7) + ".01";

            //    iFirstLeft = Convert.ToDateTime(strFirstDay).Subtract(Convert.ToDateTime(strStartDate.Substring(0, 10))).Days;

            //    vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();

            //    vlbl.NumericValue = iFirstLeft * 50;
            //    vlbl.Text = strFirstDay;

            //    iMonthCount = DateTime.Compare(Convert.ToDateTime(strFirstDay), Convert.ToDateTime(strEndDate.Substring(0, 10)));

            //}         

            chart.ChartArea.AxisX.AnnoMethod = C1.Win.C1Chart.AnnotationMethodEnum.ValueLabels;


        }

        /// <summary>
        /// 设置chart的基本信息
        /// </summary>
        public void SetXyRtChart()
        {
            c1Chart1.ChartGroups[0].ChartData.SeriesList.Clear();

            Point pLoc = new Point();
            pLoc.X = -1;
            pLoc.Y = -1;
            c1Chart1.Reset();

            c1Chart1.ChartArea.ResetSizeDefault();
            c1Chart1.ChartArea.ResetStyle();
            //  c1Chart1.Style.sho

            c1Chart1.ChartArea.AxisX.GridMajor.Visible = true;
            c1Chart1.ChartArea.AxisY.GridMajor.Visible = true;

            c1Chart1.ToolTip.Enabled = true;


            c1Chart1.BackColor = Color.White;


            c1Chart1.Legend.Compass = CompassEnum.East;
            c1Chart1.Legend.Location = pLoc;
            c1Chart1.Legend.Visible = true;
            c1Chart1.ToolTip.Enabled = true;

            c1Chart1.ToolTip.PlotElement = PlotElementEnum.Series;
            c1Chart1.ToolTip.SelectAction = SelectActionEnum.MouseOver;

            c1Chart1.ChartGroups[0].ChartData.SeriesList.Clear();

            c1Chart1.ChartGroups[0].ChartType = C1.Win.C1Chart.Chart2DTypeEnum.XYPlot;


            c1Chart1.ChartArea.AxisX.ScrollBar.Visible = false;


            //设置图片的显示方式
            C1Chart chart = c1Chart1;
            Area area = chart.ChartArea;
            Axis ax = area.AxisX;
            ax.AnnotationRotation = -30;


            //C1FlexGrid c1Flex = (C1FlexGrid)_tab.TabPages[_tab.SelectedIndex].Controls[0];
            var lines = CurrentSection.lines;
            string sDate = "";
            string eDate = "";

            foreach (var line in lines)
            {
                if (line.dataTable != null && line.dataTable.Rows.Count > 0)
                {
                    var tsDate = line.dataTable.Rows[0][0].ToString();
                    var teDate = line.dataTable.Rows[line.dataTable.Rows.Count - 1][0].ToString();
                    var ti = line.dataTable.Rows.Count - 1;
                    while (string.IsNullOrEmpty(teDate))
                    {
                        ti--;
                        teDate = line.dataTable.Rows[ti][0].ToString();
                    }
                    if (sDate == "" || eDate == "")
                    {
                        sDate = tsDate;
                        eDate = teDate;
                    }
                    else
                    {
                        if (DateTime.Parse(sDate) > DateTime.Parse(tsDate))
                        {
                            sDate = tsDate;
                        }
                        if (DateTime.Parse(teDate) > DateTime.Parse(eDate))
                        {
                            sDate = tsDate;
                        }
                    }
                }
            }
            string fmt = "yyyy.MM.dd";
            var start = DateTime.Parse(sDate);
            var timespan = DateTime.Parse(eDate).Subtract(start);
            var j = 1;//时间的小时间隔
            if (timespan.TotalDays > 1 && timespan.TotalDays < 2)
                j = 2;
            else if (timespan.TotalDays >= 2 && timespan.TotalDays < 3)
                j = 3;
            else if (timespan.TotalDays >= 3 && timespan.TotalDays < 4)
                j = 4;
            else if (timespan.TotalDays >= 4&&timespan.TotalDays < 6)
                j = 5;
            else if (timespan.TotalDays >= 6)
                j = 12;
            for (int i = 0; i < timespan.TotalHours; i=i+j)
            {
                ValueLabel vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
                var end = start.AddHours(i+j);
                vlbl.NumericValue = (i + j) *60; //修改前为50
                //if (start.Hour == 0)
                //    vlbl.Text = end.ToString(fmt);
                //else
                    vlbl.Text = end.ToString("yyyy.MM.dd HH:mm");
            }
            //string sDate = "";
            //string eDate = "";

            //foreach (var line in lines)
            //{
            //    if (line.dataTable != null && line.dataTable.Rows.Count > 0)
            //    {
            //        string fmt = "yyyy.MM.dd";
            //        var tsDate = line.dataTable.Rows[0][0].ToString();
            //        var start = DateTime.Parse(tsDate);
            //        for (int i = 0; i < line.dataTable.Rows.Count; i++)
            //        {
            //             tsDate = line.dataTable.Rows[i][0].ToString();
            //           var end= DateTime.Parse(tsDate);
            //            ValueLabel   vlbl = chart.ChartArea.AxisX.ValueLabels.AddNewLabel();
            //            var timespan = end-start;
            //            vlbl.NumericValue = i; //修改前为50
            //            if (start.Hour == 0)
            //                vlbl.Text = end.ToString(fmt);
            //            else
            //                vlbl.Text = end.ToString("HH:mm");
            //        }
            //        chart.ChartArea.AxisX.AnnoMethod = C1.Win.C1Chart.AnnotationMethodEnum.ValueLabels;

            //    }
            //}
            chart.ChartArea.AxisX.AnnoMethod = C1.Win.C1Chart.AnnotationMethodEnum.ValueLabels;
        }


        /// <summary>
        /// 显示测线数据
        /// </summary>
        public void ShowData()    //显示测线函数
        {

            if (PointDatas.Count > 0)
            {

                ChartData cd1 = c1Chart1.ChartGroups[0].ChartData;
                ChartDataSeries ds1;
                var d2 = new DateTime(1970, 1, 1, 0, 0, 0);
                cd1.SeriesList.Clear();
                for (int i = 0; i < PointDatas.Count; i++)
                {
                    ds1 = cd1.SeriesList.AddNewSeries();
                    ds1.LineStyle.Pattern = LinePatternEnum.Solid;
                    ds1.SymbolStyle.Shape = (SymbolShapeEnum)i;
                    ds1.SymbolStyle.Color = Color.Beige;
                    ds1.SymbolStyle.OutlineColor = clrs[i];
                    ds1.SymbolStyle.OutlineWidth = 1;
                    ds1.SymbolStyle.Size = 8;
                    ds1.LegendEntry = false;
                    if (PointDatas[i].IsShow)
                    {
                        ds1.LegendEntry = true;
                        ds1.PointData.CopyDataIn(PointDatas[i].Data);
                        ds1.Label = PointDatas[i].Name + "测线";
                        ds1.Tag = PointDatas[i].Name;
                    }
                }

            }

        }

        /// <summary>
        /// 表达式计算
        /// </summary>
        /// <param name="text"></param>
        /// <param name="txtLabel"></param>
        /// <param name="pf"></param>
        void AddNewFunction(string text, string txtLabel, PointF[] pf)
        {
            C1.Win.C1Chart.ChartDataSeries s = new C1.Win.C1Chart.ChartDataSeries();
            c1Chart1.ChartGroups.Group0.ChartData.SeriesList.Add(s);
            PointF[] pts = Engine.Run(text, pf);
            s.PointData.CopyDataIn(pts);
            s.Label = txtLabel + "回归";
            s.Tag = txtLabel;
            s.FitType = FitTypeEnum.Spline;

        }
        /// <summary>
        /// 多项式函数转换
        /// </summary>
        /// <param name="dk"></param>
        /// <param name="txtLabel"></param>
        /// <param name="pf"></param>
        private void FunctionPoint(double[] dk, string txtLabel, PointF[] pf)
        {
            C1.Win.C1Chart.ChartDataSeries s = new C1.Win.C1Chart.ChartDataSeries();
            c1Chart1.ChartGroups.Group0.ChartData.SeriesList.Add(s);

            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), pf.Length);
            double tempY = 0;

            for (int i = 0; i < pf.Length; i++)
            {
                data[i].X = pf[i].X;
                tempY = dk[0] + dk[1] * data[i].X + dk[2] * data[i].X * data[i].X + dk[3] * data[i].X * data[i].X * data[i].X + dk[4] * data[i].X * data[i].X * data[i].X * data[i].X + dk[5] * data[i].X * data[i].X * data[i].X * data[i].X * data[i].X + dk[6] * data[i].X * data[i].X * data[i].X * data[i].X * data[i].X * data[i].X;
                data[i].Y = (float)tempY;
            }
            s.PointData.CopyDataIn(data);
            s.Label = txtLabel + "回归";
            s.Tag = txtLabel;
            s.FitType = FitTypeEnum.Spline;

        }

        /// <summary>
        /// 多项式函数速度
        /// </summary>
        /// <param name="dk"></param>
        /// <param name="txtLabel"></param>
        /// <param name="pf"></param>
        private void FunctionPointV(double[] dk, string txtLabel, PointF[] pf)
        {
            C1.Win.C1Chart.ChartDataSeries s = new C1.Win.C1Chart.ChartDataSeries();
            c1Chart1.ChartGroups.Group0.ChartData.SeriesList.Add(s);

            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), pf.Length);
            double tempY = 0;

            for (int i = 0; i < pf.Length; i++)
            {
                data[i].X = pf[i].X;
                tempY = dk[1] + 2 * dk[2] * data[i].X + 3 * dk[3] * data[i].X * data[i].X + 4 * dk[4] * data[i].X * data[i].X * data[i].X + 5 * dk[5] * data[i].X * data[i].X * data[i].X * data[i].X + 6 * dk[6] * data[i].X * data[i].X * data[i].X * data[i].X * data[i].X;
                data[i].Y = (float)tempY;
            }
            s.PointData.CopyDataIn(data);
            s.Label = txtLabel + "回归";
            s.Tag = txtLabel;
            s.FitType = FitTypeEnum.Spline;

        }
        /// <summary>
        /// 多项式函数加速度
        /// </summary>
        /// <param name="dk"></param>
        /// <param name="txtLabel"></param>
        /// <param name="pf"></param>
        private void FunctionPointA(double[] dk, string txtLabel, PointF[] pf)
        {
            C1.Win.C1Chart.ChartDataSeries s = new C1.Win.C1Chart.ChartDataSeries();
            c1Chart1.ChartGroups.Group0.ChartData.SeriesList.Add(s);

            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), pf.Length);
            double tempY = 0;

            for (int i = 0; i < pf.Length; i++)
            {
                data[i].X = pf[i].X;
                tempY = 2 * dk[2] + 3 * 2 * dk[3] * data[i].X + 4 * 3 * dk[4] * data[i].X * data[i].X + 5 * 4 * dk[5] * data[i].X * data[i].X * data[i].X + 6 * 5 * dk[6] * data[i].X * data[i].X * data[i].X * data[i].X;
                data[i].Y = (float)tempY;
            }
            s.PointData.CopyDataIn(data);
            s.Label = txtLabel + "回归";
            s.Tag = txtLabel;
            s.FitType = FitTypeEnum.Spline;

        }
    }
}
