using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 进度表模型
    /// </summary>
    public class ScheduleDataModel : TableDataModel
    {
        /// <summary>
        /// 进度表模型
        /// </summary>
        public ScheduleDataModel() { }
        /// <summary>
        /// 表头开始时间
        /// </summary>
        [WordMark(MarkName = "StartDate")]
        public string StartDate { get; set; }
        /// <summary>
        /// 表头结束时间
        /// </summary>
        [WordMark(MarkName = "EndDate")]
        public string EndDate { get; set; }

        /// <summary>
        /// 具体记录
        /// </summary>
        public List<ScheduleDataTypeModel> DataTypeModels { get; set; }
    }

    public class ScheduleDataTypeModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 记录
        /// </summary>
        public List<ScheduleTypeDataListModel> DataList { get; set; }
    }

    public class ScheduleTypeDataListModel
    {
        /// <summary>
        /// 施工工序
        /// </summary>
        public string ConstructionProcess { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set;}
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 阶段进尺
        /// </summary>
        public string StageFootage { get; set; }
        /// <summary>
        /// 累计进尺
        /// </summary>
        public string AccumulativeFootage { get; set; }
    }
}
