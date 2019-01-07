using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word.Table
{
    /// <summary>
    /// 初支厚度表
    /// </summary>
    public class ChuzhiThicknessModel : TableDataModel
    {
        /// <summary>
        /// 初支厚度表
        /// </summary>
        public ChuzhiThicknessModel() { }
        // 混凝土喷射厚度
        public string ShotcreteThickness { get; set; }
        // 最大厚度
        public string MaxThickness { get; set; }
        // 最小厚度
        public string MinThickness { get; set; }
        // 平均厚度
        public string AverageThickness { get; set; }
        // 合格率
        public string GoodPercent { get; set; }
        // 底部跨多行的文本内容
        public string BottomText { get; set; }
        // 每个里程段的数据
        public List<ChuzhiThicknessDataModel> ChuzhiThicknessDatas { get; set; }
        // 每个里程段的平均数据
        public List<ChuzhiThicknessAveDataModel> ChuzhiThicknessAveDatas { get; set; }
    }

    public class ChuzhiThicknessDataModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Index { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public string ParamsMileage { get; set; }
        /// <summary>
        /// 实测厚度G
        /// </summary>
        public string RealThicknessG { get; set; }
        /// <summary>
        /// 实测厚度E
        /// </summary>
        public string RealThicknessE { get; set; }
        /// <summary>
        /// 实测厚度C
        /// </summary>
        public string RealThicknessC { get; set; }
        /// <summary>
        /// 实测厚度A
        /// </summary>
        public string RealThicknessA { get; set; }
        /// <summary>
        /// 实测厚度B
        /// </summary>
        public string RealThicknessB { get; set; }
        /// <summary>
        /// 实测厚度D
        /// </summary>
        public string RealThicknessD { get; set; }
        /// <summary>
        /// 实测厚度F
        /// </summary>
        public string RealThicknessF { get; set; }
    }

    public class ChuzhiThicknessAveDataModel
    {
        /// <summary>
        /// 实测厚度G
        /// </summary>
        public string RealThicknessG { get; set; }
        /// <summary>
        /// 实测厚度E
        /// </summary>
        public string RealThicknessE { get; set; }
        /// <summary>
        /// 实测厚度C
        /// </summary>
        public string RealThicknessC { get; set; }
        /// <summary>
        /// 实测厚度A
        /// </summary>
        public string RealThicknessA { get; set; }
        /// <summary>
        /// 实测厚度B
        /// </summary>
        public string RealThicknessB { get; set; }
        /// <summary>
        /// 实测厚度D
        /// </summary>
        public string RealThicknessD { get; set; }
        /// <summary>
        /// 实测厚度F
        /// </summary>
        public string RealThicknessF { get; set; }
    }
}

