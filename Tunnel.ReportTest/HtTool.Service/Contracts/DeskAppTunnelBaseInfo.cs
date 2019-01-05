using System;
using System.Collections.Generic;
using System.Text;

namespace HtTool.Service.Contracts
{
    [Serializable]
    public class DeskAppTunnelBaseInfo
    {
        public List<TunnelInfo> TunnelList { get; set; }

        public List<string> LrDescList { get; set; }

        public List<string> TunnelDescList { get; set; }
    }

    [Serializable]
    public class TunnelInfo
    {
        /// <summary>
        /// 隧道ID，系统
        /// </summary>
        public System.Guid TunnelId { get; set; }

        /// <summary>
        /// 隧道Code
        /// </summary>
        public string TunnelCode { get; set; }

        /// <summary>
        /// 隧道名称
        /// </summary>
        public string TunnelName { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ShowName { get; set; }

    }
}
