using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using HtTool.Service.Contracts;

namespace HtTool.Service.Monitor
{
    public static class BaseConfig
    {
        public static DataTable GetIniThreeLineTable()
        {
            var tb = new DataTable();
            tb.Columns.Add(new DataColumn("测量日期", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第一次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第二次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第三次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AB", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第一次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第二次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第三次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AC", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第一次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第二次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第三次读数）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度BC", typeof(string)));

            return tb;
        }

        public static DataTable GetIniSixLineTable()
        {
            var tb = new DataTable();
            tb.Columns.Add(new DataColumn("测量日期", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AB（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AB", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AC（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AC", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线BC（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度BC", typeof(string)));

            tb.Columns.Add(new DataColumn("测线AD（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AD（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AD（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AD", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AE（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AE（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线AE（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度AE", typeof(string)));
            tb.Columns.Add(new DataColumn("测线DE（第一次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线DE（第二次）", typeof(string)));
            tb.Columns.Add(new DataColumn("测线DE（第三次）", typeof(string)));
            tb.Columns.Add(new DataColumn("温度DE", typeof(string)));


            return tb;
        }

        //public static DataTable UniteLine(List<Line> lines)
        //{
        //    //通过时间确定所有的row
        //    var dateTimeList = new List<string>();
        //    foreach (var line in lines)
        //    {
        //        for (int i = 0; i < line.dataTable.Rows.Count; i++)
        //        {
        //            if (dateTimeList.Contains(line.dataTable.Rows[i][0].ToString()))
        //            {
        //                continue;
        //            }
        //            dateTimeList.Add(line.dataTable.Rows[i][0].ToString());
        //        }
        //    }

        //    var dt = new DataTable();
        //    if (lines.Count == 3)
        //    {
        //        dt = BaseConfig.GetIniThreeLineTable();
        //    }
        //    else if (lines.Count == 6)
        //    {
        //        dt = BaseConfig.GetIniSixLineTable();
        //    }
        //    else
        //    {
        //        dt = BaseConfig.GetIniThreeLineTable();
        //    }
        //    //添加时间
        //    for (int i = 0; i < dateTimeList.Count; i++)
        //    {
        //        var dr = dt.NewRow();
        //        dt.Rows.Add(dr);
        //        dt.Rows[i][0] = dateTimeList[i];
        //    }

        //    //foreach (var line in lines)
        //    //{
        //    //    if (line.name)
        //    //    {

        //    //    }
        //    //    for (int i = 0; i < line.dataTable.Rows.Count; i++)
        //    //    {
        //    //        for (int j = 0; j < line.dataTable.Columns.Count; j++)
        //    //        {
        //    //            dt.Rows[i][j + 1] = line.dataTable.Rows[i][j + 1].ToString();
        //    //        }
        //    //    }
        //    //}

        //}



        /// <summary>
        /// 计算天数
        /// </summary>
        /// <param name="strFirst"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static double TotleTime(string strFirst, string strSecond,bool isRt)
        {
            double iTotle = 0;

            var endDate = DateTime.Parse(strSecond);
            var startDate = DateTime.Parse(strFirst);
            DateTime start =isRt? startDate : Convert.ToDateTime(startDate.ToShortDateString());
            DateTime end = isRt?endDate: Convert.ToDateTime(endDate.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            //int iDays = Convert.ToDateTime(strSecond.Substring(0, 10)).Subtract(Convert.ToDateTime(strFirst.Substring(0, 10))).Days;

            //int iTime = SecondPart(strSecond) - SecondPart(strFirst);


            // iTotle = iDays * 4 + iTime;  //李总要求修改，只计算天数
            iTotle =isRt?sp.TotalMinutes:sp.TotalDays;

            return iTotle;
        }

        private static int SecondPart(string strFull)
        {
            string strResult = "";

            int iResult = 0;

            switch (strFull.Length)
            {
                case 14:
                    strResult = strFull.Substring(13, 1);
                    break;
                case 15:
                    strResult = strFull.Substring(13, 2);
                    break;
                case 16:
                    strResult = strFull.Substring(13, 3);
                    break;

            }

            switch (strResult)
            {
                case "I":
                    iResult = 1;
                    break;
                case "II":
                    iResult = 2;
                    break;
                case "III":
                    iResult = 3;
                    break;
                case "IV":
                    iResult = 4;
                    break;

            }

            return iResult;

        }

        /// <summary>
        /// 根据每一条测线的数据生成point数组
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static PointF[] AddNewPoint(Line line,bool isRt)
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
                if (flex22[i][6] == null || flex22[i][6].ToString() == "")
                    continue;
                else
                {
                    npts += 1;
                    strEnd = flex22[i][0].ToString();//得到结束时间
                }
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
                if (flex22[i][6].ToString() == "")//这个地方如果没数据怎么办
                    continue;
                else
                {
                    data[i].X = xmin + dx * (float)BaseConfig.TotleTime(flex22[0][0].ToString(), flex22[i][0].ToString(), isRt); //时间差
                    data[i].Y = (float)Convert.ToDouble(flex22[i][10]);//累积位移
                }
            }

            return data;
        }


        /// <summary>
        /// 计算每个节目的点位（x.y）数据
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static List<PointData> GetPointDatasBySection(Section section)
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
    }
}
