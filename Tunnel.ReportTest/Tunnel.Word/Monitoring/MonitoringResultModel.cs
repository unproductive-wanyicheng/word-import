using System.Collections.Generic;

namespace Tunnel.Word.Monitoring
{
    /// <summary>
    /// 监测结果模型
    /// </summary>
    public class MonitoringResultModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ResultName { get; set; }
        /// <summary>
        /// 开始描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public List<IListModel> ResultDataList { get; set; }

        /// <summary>
        /// 总结
        /// </summary>
        public string Summary { get; set; }
    }


    /// <summary>
    /// 必测项目监测结果
    /// </summary>
    public class MustMonitoringResultModel : MonitoringResultModel, IMonitoringResult
    {
        
    }

    /// <summary>
    /// 选测项目监测结果
    /// </summary>
    public class SelectionMonitoringResultModel : IMonitoringResult
    {
        public string ItemName { get; set; }
        public List<MonitoringResultModel> ResultListModel { get; set; }
    }
}
