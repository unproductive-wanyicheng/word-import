using System;
using System.Collections.Generic;
using System.Text;

namespace HtTool.Service
{
    /// <summary>
    /// 各个函数的系数、参数计算
    /// </summary>
    public class CurvePara
    {
        /// <summary>
        /// 测线、测点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 回归曲线参数
        /// </summary>
        public double A { get; set; }
        /// <summary>
        /// 回归曲线参数
        /// </summary>
        public double B { get; set; }
        public double Ia { get; set; }
        /// <summary>
        /// 相关指数
        /// </summary>
        public double R { get; set; }

        /// <summary>
        /// 多项式回归函数的系数
        /// </summary>
        public int Ik { get; set; }

        /// <summary>
        /// 多项式回归函数的系数（斜率？）
        /// </summary>
        public double[] K = { 0, 0, 0, 0, 0, 0, 0 };

    }
}
