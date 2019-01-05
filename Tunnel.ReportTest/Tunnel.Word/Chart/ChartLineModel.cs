using System.Collections.Generic;

namespace Tunnel.Word.Chart
{
    public  class ChartLineModel
    {
        /// <summary>
        /// 测量日期
        /// </summary>
        public string DateMeasurement { get; set; }

        /// <summary>
        /// 第一次读数
        /// </summary>
        public string FistData { get; set; }
        /// <summary>
        /// 第二次读数
        /// </summary>
        public string SecondData { get; set; }
        /// <summary>
        /// 第三次读数
        /// </summary>
        public string ThirdData { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        public string AverageValue { get; set; }

        /// <summary>
        /// 加仪器常数后测值mm
        /// </summary>
        public string AfterConstant { get; set; }

        /// <summary>
        /// 温度℃
        /// </summary>
        public string Temperature { get; set; }

        /// <summary>
        /// 温度改正后测值
        /// </summary>
        public string TemperatureCorrection { get; set; }
        /// <summary>
        /// 与上次相比位移mm
        /// </summary>
        public string ComparedLast { get; set; }

        /// <summary>
        /// 位移速率
        /// </summary>
        public string DisplacementRate { get; set; }

        /// <summary>
        /// 累计位移mm
        /// </summary>
        public string CumulativeDisplacement { get; set; }
    }

    public class ChartLineDataModel
    {
        public string LineName { get; set; }
        public List<ChartLineModel> LineModels { get; set; }

        //public ChartPoint[] ChartData { get; set; }
    }
}
