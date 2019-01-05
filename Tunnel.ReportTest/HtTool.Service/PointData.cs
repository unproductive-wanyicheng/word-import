using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using HtTool.Service.Contracts;
using HtTool.Service.Monitor;

namespace HtTool.Service
{
    /// <summary>
    /// 点集合数据
    /// </summary>
    public class PointData
    {
        /// <summary>
        /// 测线、测点名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 测线、测点类型
        /// </summary>
        public string Type { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public PointF[] Data { set; get; }


        /// <summary>
        /// 时间差数据
        /// </summary>
        private MPoint[] _mdata = new MPoint[0];

        public MPoint[] MData
        {
            set { _mdata = value; }
            get { return _mdata; }
        }

        //public ChartPoint[] ChartData { get; set; }
       


        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { set; get; }

        /// <summary>
        /// 位移数组
        /// </summary>
        public double[] DShift = new Double[0];

        /// <summary>
        /// 速度数组
        /// </summary>
        public double[] DVel = new Double[0];

        /// <summary>
        /// 加速度数组
        /// </summary>
        public double[] DAcc = new Double[0];

        /// <summary>
        /// 数据
        /// </summary>
        private List<SectionLine> _linesData = new List<SectionLine>();
        public List<SectionLine> LinesData
        {
            set { _linesData = value; }
            get { return _linesData; }
        }

        
    }
}
