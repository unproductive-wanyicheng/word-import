using System;
using System.Collections.Generic;
using System.Text;
using HtTool.Service.Monitor;

namespace HtTool.Service.Contracts
{
    [Serializable]
    public class ReportSectionInfo
    {
        public Guid Id { set; get; }
        public string SectionName { set; get; }

        public Guid ProjectId { set; get; }

        public Guid TunnelId { set; get; }
        /// <summary>
        /// 左右幅、斜井
        /// </summary>
        public string LrDesc { set; get; }
        /// <summary>
        /// 进出口、端口
        /// </summary>
        public string TunnelDesc { set; get; }

        /// <summary>
        /// 仪器常数
        /// </summary>
        public double GStrEquipConst { set; get; }
        /// <summary>
        /// 预留变形量 Uo
        /// </summary>
        public double GStrU0 { set; get; }
        /// <summary>
        /// 最大变形速度
        /// </summary>
        public double MaxDefSpeed { set; get; }

        /// <summary>
        /// 最大变形加速度
        /// </summary>
        public double MaxDefAcc { set; get; }
        /// <summary>
        /// 最后一次提交时间
        /// </summary>
        public DateTime LastTime { set; get; }
        /// <summary>
        /// 测面状态
        /// </summary>
        public int Status { set; get; }
        /// <summary>
        /// 当前分析结果
        /// </summary>
        public string AnResult { set; get; }

        public List<SectionLine> Lines { set; get; }

        public List<ReportLine> ReportLines { get; set; }

        public int? SurroundLevel { get; set; }

        /// <summary>
        /// 累积位移
        /// </summary>
        public double TotalDisValue { set; get; }

        /// <summary>
        /// 拱顶累积位移
        /// </summary>
        public double HTotalDisValue { set; get; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        public int Pileno { get; set; }

        public string EquipId { get; set; }

    }

    [Serializable]
    public class SectionLine
    {
        public Guid Id { set; get; }

        public string LineName { set; get; }
        public Guid SectionId { set; get; }
        public string SectionName { set; get; }
        public string SectionType { set; get; }
        /// <summary>
        /// 测量日期
        /// </summary>
        public DateTime? RecordTime { set; get; }

        public string RecordTimeStr { get; set; }
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 第一次读数
        /// </summary>
        public double Result1 { set; get; }
        /// <summary>
        /// 第二次读数
        /// </summary>
        public double Result2 { set; get; }
        /// <summary>
        /// 第三次读数
        /// </summary>
        public double Result3 { set; get; }
        /// <summary>
        /// 平均值
        /// </summary>
        public double AvgResult { set; get; }
        /// <summary>
        /// 加仪器常数后测值
        /// </summary>
        public double EquipResult { set; get; }
        /// <summary>
        /// 温度
        /// </summary>
        public double Temperature { set; get; }
        /// <summary>
        /// 温度改正后测值
        /// </summary>
        public double TempResult { set; get; }
        /// <summary>
        /// 与上次相比位移
        /// </summary>
        public double LastDiffResult { set; get; }
        /// <summary>
        /// 位移速率
        /// </summary>
        public double DisSpeed { set; get; }
        /// <summary>
        /// 累计位移
        /// </summary>
        public double CumSpeed { set; get; }

    }

    [Serializable]
    public class ReportLine
    {
        public string LineName { get; set; }

        public string SectionType { get; set; }

        public int Days { get; set; }

        /// <summary>
        /// 位移速率
        /// </summary>
        public double DisSpeed { set; get; }
        /// <summary>
        /// 累计位移
        /// </summary>
        public double CumSpeed { set; get; }

        /// <summary>
        /// 位移时间chart数据
        /// </summary>
        private ChartPoint[] _disChartData = new ChartPoint[0];
        public ChartPoint[] DisChartData
        {
            set { _disChartData = value; }
            get { return _disChartData; }
        }
        /// <summary>
        /// 速度时间chart数据
        /// </summary>
        private ChartPoint[] _velChartData = new ChartPoint[0];
        public ChartPoint[] VelChartData
        {
            set { _velChartData = value; }
            get { return _velChartData; }
        }
    }
}
