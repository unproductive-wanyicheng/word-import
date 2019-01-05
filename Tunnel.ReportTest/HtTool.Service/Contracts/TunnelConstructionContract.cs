using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtTool.Service.Contracts
{
    /// <summary>
    /// 施工进度统计表
    /// </summary>
    public class TunnelConstructionContract
    {
        public Guid Id { get; set; }

        public Guid TunnelId { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int IsDel { get; set; }
        /// <summary>
        /// --进口，出口
        /// </summary>
        public string TunnelDesc { get; set; }

        /// <summary>
        ///  --隧道属性表TUNNEL_EXPAND的id
        /// </summary>
        public Guid? ExpandId { get; set; }

        /// <summary>
        /// 左幅、右幅、斜井
        /// </summary>
        public string ExpandName { get; set; }

        /// <summary>
        /// 施工工序字典id
        /// </summary>
        public Guid ProcessId { get; set; }

        /// <summary>
        /// 施工工序
        /// </summary>
        /// <returns></returns>
        public string ProcessName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
       public string Name { get; set; }
        /// <summary>
        /// 施工日期
        /// </summary>
        public DateTime ConstructionDate { get; set; }

        /// <summary>
        /// 起始里程 K1+110
        /// </summary>
        public string StartKStr { get; set; }

        /// <summary>
        /// 截止里程  K1+340
        /// </summary>
        public string EndKStr { get; set; }

        /// <summary>
        /// 起始里程
        /// </summary>
        public double StartK { get; set;}

        /// <summary>
        /// 截止里程
        /// </summary>
        public double EndK { get; set; }
        
    }

    /// <summary>
    /// 隧道开挖累计里程
    /// </summary>
    public class ConstructionLegend
    {
        /// <summary>
        /// 左幅、右幅、斜井
        /// </summary>
        public string LrDesc { get; set; }

        /// <summary>
        /// 里程
        /// </summary>
        public Dictionary<Guid,double> Legends { get; set; }

     
    }
}
