using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using HtTool.Service;
using HtTool.Service.Monitor;

namespace Tunnel.Word.Chart
{
    public class TunnelChartService
    {
        protected TunnelChart _tunnelChart = new TunnelChart();

        public void InitChart(ChartDataModel dataModel = null)
        {
            if (dataModel != null)
            {
                _tunnelChart.Sections = ConvertToSections(new List<ChartDataModel>() { dataModel });
            }
            _tunnelChart.DataChartLoad();
        }
        public void InitChart(List<ChartDataModel> dataModel)
        {
            if (dataModel != null)
            {
                _tunnelChart.Sections = ConvertToSections(dataModel);
            }
            _tunnelChart.DataChartLoad();
        }

        public void BuildChart(string sectionName = "")
        {
            if (_tunnelChart.Sections != null && _tunnelChart.Sections.Count > 0)
            {
                if (string.IsNullOrEmpty(sectionName))
                {
                    _tunnelChart.CurrentSection = _tunnelChart.Sections.First();
                }
                else
                {
                    _tunnelChart.CurrentSection = _tunnelChart.Sections.FirstOrDefault(s => s.name == sectionName);
                }
                if (_tunnelChart.CurrentSection != null)
                {
                    _tunnelChart.PointDatas = GetPointDatasBySection(_tunnelChart.CurrentSection);
                }
                _tunnelChart.InitChart();
            }

        }

        public void SetChartData(ChartDataModel dataModel)
        {
            _tunnelChart.Sections = ConvertToSections(new List<ChartDataModel>() { dataModel });
        }
        public void SetChartData(List<ChartDataModel> dataModel)
        {
            _tunnelChart.Sections = ConvertToSections(dataModel);
        }


        public Image GetImage(ImageFormat imgfmt, Size sz)
        {
            return _tunnelChart.c1Chart1.GetImage(imgfmt, sz);
        }

        public void SaveImage(string filename, ImageFormat imgFmt, Size sz)
        {
            _tunnelChart.c1Chart1.SaveImage(filename, imgFmt, sz);
        }

        private List<Section> ConvertToSections(List<ChartDataModel> dataModels)
        {
            List<Section> sections = new List<Section>();

            if (dataModels != null)
            {
                foreach (var dataModel in dataModels)
                {
                    Section section = new Section(dataModel.SectionName);
                    section.gStrEquipConst = dataModel.EquipCost;
                    section.gStrU0 = dataModel.U0;
                    section.EquipId = dataModel.EquipId;
                    foreach (var lineDataModel in dataModel.LindDataModels)
                    {
                        Line line = new Line(lineDataModel.LineName);
                        line.dataTable = LineModelConvertToDt(lineDataModel.LineModels);
                        //line.ChartData = lineDataModel.ChartData;
                        line.name = lineDataModel.LineName;
                        section.lines.Add(line);
                    }
                    sections.Add(section);
                }
            }
            return sections;
        }

        private DataTable LineModelConvertToDt(List<ChartLineModel> lineModels)
        {
            var tb = new DataTable();
            tb.Columns.Add(new DataColumn("测量日期", typeof(string)));
            tb.Columns.Add(new DataColumn("第一次读数", typeof(string)));
            tb.Columns.Add(new DataColumn("第二次读数", typeof(string)));
            tb.Columns.Add(new DataColumn("第三次读数", typeof(string)));
            tb.Columns.Add(new DataColumn("平均值", typeof(string)));
            tb.Columns.Add(new DataColumn("加仪器常数后测值mm", typeof(string)));
            tb.Columns.Add(new DataColumn("温度℃", typeof(string)));
            tb.Columns.Add(new DataColumn("温度改正后测值", typeof(string)));
            tb.Columns.Add(new DataColumn("与上次相比位移mm", typeof(string)));
            tb.Columns.Add(new DataColumn("位移速率mm/d", typeof(string)));
            tb.Columns.Add(new DataColumn("累计位移mm", typeof(string)));

            foreach (var lineModel in lineModels)
            {
                DataRow dr = tb.NewRow();
                dr[0] = lineModel.DateMeasurement;
                dr[1] = lineModel.FistData;
                dr[2] = lineModel.SecondData;
                dr[3] = lineModel.ThirdData;
                dr[4] = lineModel.AverageValue;
                dr[5] = lineModel.AfterConstant;
                dr[6] = lineModel.Temperature;
                dr[7] = lineModel.TemperatureCorrection;
                dr[8] = lineModel.ComparedLast;
                dr[9] = lineModel.DisplacementRate;
                dr[10] = lineModel.CumulativeDisplacement;
                tb.Rows.Add(dr);
            }

            return tb;
        }
        private List<PointData> GetPointDatasBySection(Section section)
        {
            var list = new List<PointData>();
            if (section.lines.Count > 0)
            {
                foreach (var line in section.lines)
                {
                    var pd = new PointData
                    {
                        Name = line.name.Trim(),
                        Data = AddNewPoint(line,!string.IsNullOrEmpty(section.EquipId)),
                        IsShow = true
                    };
                    list.Add(pd);
                }
            }

            return list;
        }
        private PointF[] AddNewPoint(Line line,bool isRt)
        {
            string strStart = "";
            string strEnd = "";
            float xmin = 0;
            float xmax = 0;

            int npts = 0;

            if (line.dataTable == null || line.dataTable.Rows.Count == 0)
            {
                return new PointF[0];
            }

            var flex22 = line.dataTable.Rows;

            if (flex22[0][0].ToString() != "")
            {
                strStart = flex22[0][0].ToString(); //得到开始时间                  
            }

            for (int i = 0; i < flex22.Count; i++)
            {
                npts += 1;
                strEnd = flex22[i][0].ToString();//得到结束时间
            }


            //if (strStart != "" && strEnd != "")
            //    xmax = (float)BaseConfig.TotleTime(strStart, strEnd);
            //else
            //    xmax = 0;

            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), npts);
            // float dx = (float)12.5; //李总要求修改前

            float dx = (float)1.0; //修改后

            for (int i = 0; i < flex22.Count; i++)
            {
                data[i].X = xmin + dx * (float)BaseConfig.TotleTime(flex22[0][0].ToString(), flex22[i][0].ToString(),isRt); //时间差
                data[i].Y = (float)Convert.ToDouble(flex22[i][10]);//累积位移
            }

            return data;
        }
    }
}
