using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 数据分析表
    /// </summary>
    public class DataAnalysisModel : TableDataModel
    {
        /// <summary>
        /// 数据分析表
        /// </summary>
        public DataAnalysisModel() { }
        public List<DataAnalysisTypeModel> AnalysisTypeModels { get; set; }
    }

    public class DataAnalysisTypeModel
    {
        /// <summary>
        /// 桩号
        /// </summary>
        public string PileNumber { get; set; }
        /// <summary>
        /// 数据分析
        /// </summary>
        public string DataAnalysis { get; set; }
        public List<DataAnalysisDataModel> DataList { get; set; }
    }

    public class DataAnalysisDataModel
    {
        /// <summary>
        /// 测点
        /// </summary>
        public string MeasuringPoint { get; set; }
        /// <summary>
        /// 监测天数
        /// </summary>
        public string MonitoringDays { get; set; }
        /// <summary>
        /// 累计值
        /// </summary>
        public string CumulativeValue { get; set; }
        /// <summary>
        /// 平均速度
        /// </summary>
        public string AverageVelocity { get; set; }
    }
}
