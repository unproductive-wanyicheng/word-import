using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 埋设情况
    /// </summary>
    public class BurialSituationModel : TableDataModel
    {
        /// <summary>
        /// 埋设情况
        /// </summary>
        public BurialSituationModel() { }
        public List<BurialSituationDataModel> SituationDataModels { get; set; }
    }

    public class BurialSituationDataModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 断面数量
        /// </summary>
        public string NumberSections { get; set; }
        /// <summary>
        /// 埋设里程
        /// </summary>
        public string BuriedMileage { get; set; }
        /// <summary>
        /// 埋设时间
        /// </summary>
        public string BurialTime { get; set; }
        /// <summary>
        /// 围岩级别
        /// </summary>
        public string SurroundingRockLevel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
