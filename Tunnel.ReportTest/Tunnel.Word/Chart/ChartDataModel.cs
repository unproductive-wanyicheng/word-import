using System.Collections.Generic;
using HtTool.Service.Monitor;

namespace Tunnel.Word.Chart
{
    /// <summary>
    /// 图表模型
    /// </summary>
    public class ChartDataModel:ITunnelChart
    {
        /// <summary>
        /// 图表模型
        /// </summary>
        public ChartDataModel() { }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width = 700;
        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height = 230;

        /// <summary>
        /// 图片名称
        /// </summary>
        public string PictureName { get; set; }
        public string SectionName { get; set; }
        public string EquipCost { get; set; }

        public string EquipId { get; set; }

       

        public string U0 { get; set; }

        public string Title { get; set; }

        public List<ChartLineDataModel> LindDataModels { get; set; }
    }
}
