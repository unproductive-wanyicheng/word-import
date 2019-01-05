using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 埋设断面表
    /// </summary>
    public class BuriedSectionModel : TableDataModel
    {
        /// <summary>
        /// 埋设断面表
        /// </summary>
        public BuriedSectionModel() { }
        public List<BuriedSectionDataModel> BuriedSectionDatas { get; set; }
    }

    public class BuriedSectionDataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 断面里程
        /// </summary>
        public string SectionMileage { get; set; }
        /// <summary>
        /// 围岩级别
        /// </summary>
        public string SurroundingRockLevel { get; set; }
        /// <summary>
        /// 拱顶下沉累计值
        /// </summary>
        public string CrownSettlement { get; set; }
        /// <summary>
        /// 周边位移累计值
        /// </summary>
        public string DisplacementAcc { get; set; }
        /// <summary>
        /// 埋设时间
        /// </summary>
        public string BurialTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
