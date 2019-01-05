using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts.ReportFile
{
    /// <summary>
    /// 请求报告所传参数
    /// </summary>
    public  class ParamContract
    {

        /// <summary>
        /// 默认为0(返回二进制流)，1为文件url
        /// </summary>
        public int DownloadMode { get; set; }
        /// <summary>
        /// 自定文件名(默认名为隧道名+检测时间)
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 进口还是出口
        /// </summary>
        public string TunnelDesc { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 隧道id
        /// </summary>
        public Guid TunnelId { get; set; }

        /// <summary>
        /// 检测范围
        /// </summary>
        public List<MonitorRange> Ranges { get; set; } 
    }

    public class MonitorRange
    {
        /// <summary>
        /// 左幅、右幅
        /// </summary>
        public string LrDesc { get; set; }

        /// <summary>
        /// 开始(左幅:ZK，右幅：K)
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// 结束(左幅:ZK，右幅：K)
        /// </summary>
        public string End { get; set; }
    }
}
