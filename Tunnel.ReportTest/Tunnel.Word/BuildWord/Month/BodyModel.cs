using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunnel.Word.Model;
using Tunnel.Word.Monitoring;

namespace Tunnel.Word.BuildWord.Month
{
    public class BodyModel
    {
        /// <summary>
        /// 页眉
        /// </summary>
        [WordMark(MarkName = "headerText",Alignment = MarkAlignment.Header )]
        public string HeaderText { get; set; }

        [WordMark(MarkName = "headerText1", Alignment = MarkAlignment.Header)]
        public string HeaderText1 { get; set; }
        
        /// <summary>
        /// 页眉编号
        /// </summary>
        [WordMark(MarkName = "headerTextNum", Alignment = MarkAlignment.Header)]
        public string HeaderTextNum { get; set; }
        /// <summary>
        /// T0101_任务来源
        /// 多段落用\r\n
        /// </summary>
        [WordMark(MarkName = "T0101_任务来源")]
        public string TaskSource { get; set; }

        /// <summary>
        /// T0102_隧道工程概况
        /// 多段落用\r\n
        /// </summary>
        [WordMark(MarkName = "T0102_隧道工程概况")]
        public string EngineeringSurvey { get; set; }


        /// <summary>
        /// T0103_隧道施工情况及断面埋设
        /// ===========后期再统一处理==============
        /// </summary>
        [WordMark(MarkName = "T0103_隧道检测情况")]

        public string ConstructionSituation { get; set; }

        [WordMark(MarkName = "T0103_BuildData", MarkType = MarkType.BuildData)]
        public List<IListModel> ConstructionSituationList { get; set; }

        /// <summary>
        /// T06_监测结果 必测项目
        /// </summary>
        [WordMark(MarkName = "T0601_必测项目", MarkType = MarkType.BuildData)]

        public List<IListModel> MustMonitoringResults { get; set; }
        /// <summary>
        /// T06_监测结果 选测项目描述
        /// </summary>
        [WordMark(MarkName = "T0602_描述")]
        public string MonitorSummery { get; set; }
        /// <summary>
        /// 附件名字1
        /// </summary>
        [WordMark(MarkName = "附件名字1")]
        public string FujianName { get; set; }
        /// <summary>
        /// T06_监测结果 选测项目
        /// </summary>
        [WordMark(MarkName = "T0602_选测项目", MarkType = MarkType.BuildData)]

        public List<IListModel> SelectionMonitoringResults { get; set; }

        /// <summary>
        /// T07_结论
        /// </summary>
        [WordMark(MarkName = "T07_结论")]

        public string Conclusion { get; set; }

        /// <summary>
        /// T08_建议
        /// </summary>
        [WordMark(MarkName = "T08_建议")]

        public string Proposal { get; set; }

        /// <summary>
        /// T0701_初支厚度结论
        /// </summary>
        [WordMark(MarkName = "T0701_初支厚度结论")]

        public string ChuzhiChuzhiConclusion { get; set; }

        /// <summary>
        /// T0702_初支缺陷结论
        /// </summary>
        [WordMark(MarkName = "T0702_初支缺陷结论")]

        public string ChuzhiQuexianConclusion { get; set; }

        /// <summary>
        /// T0703_初支钢支撑结论
        /// </summary>
        [WordMark(MarkName = "T0703_初支钢支撑结论")]

        public string ChuzhiGangzhichengConclusion { get; set; }

        /// <summary>
        /// T0602_初支缺陷里程段
        /// </summary>
        [WordMark(MarkName = "T0602_初支缺陷里程段")]

        public string ChuzhiQuexianMileage { get; set; }

        /// <summary>
        /// T0603_初支缺陷结论
        /// </summary>
        [WordMark(MarkName = "T0603_初支缺陷结论")]

        public string ChuzhiQuexianConclusion1 { get; set; }

        /// <summary>
        /// T0401_初支参数表
        /// 2019年1月7日
        /// </summary>

        [WordMark(MarkName = "T0401_初支参数表1", MarkType = MarkType.BuildData)]
        public List<IListModel> ChuzhiParamsTable1 { get; set; }

        [WordMark(MarkName = "T0401_初支参数表", MarkType = MarkType.BuildData)]
        public List<IListModel> ChuzhiParamsTable { get; set; }

        /// <summary>
        /// T0401_二衬参数表
        /// 2019年1月7日
        /// </summary>

        [WordMark(MarkName = "T0401_二衬参数表", MarkType = MarkType.BuildData)]
        public List<IListModel> ErchenParamsTable1 { get; set; }


        /// <summary>
        /// T0601_初支厚度表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0601_初支厚度表", MarkType = MarkType.BuildData)]
        public List<IListModel> ChuzhiThicknessTable { get; set; }

        /// <summary>
        /// T0601_二衬厚度描述
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0601_二衬厚度描述")]
        public string ErchenThicknessDesc { get; set; }

        /// <summary>
        /// T0602_二衬厚度表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0602_二衬厚度表", MarkType = MarkType.BuildData)]
        public List<IListModel> ErchenThicknessTable { get; set; }

        /// <summary>
        /// T0603_二衬缺陷描述
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0603_二衬缺陷描述")]
        public string ErchenDefectDesc { get; set; }

        /// <summary>
        /// T0604_二衬缺陷表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0604_二衬缺陷表", MarkType = MarkType.BuildData)]
        public List<IListModel> ErchenDefectTable { get; set; }

        /// <summary>
        /// T0605_二衬间距描述
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0605_二衬间距描述")]
        public string ErchenSpaceDesc { get; set; }

        /// <summary>
        /// T0606_二衬间距表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0606_二衬间距表", MarkType = MarkType.BuildData)]
        public List<IListModel> ErchenSpaceTable { get; set; }

        /// <summary>
        /// T0604_初支缺陷检查表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0604_初支缺陷检查表", MarkType = MarkType.BuildData)]
        public List<IListModel> ChuzhisDefectTable { get; set; }

        /// <summary>
        /// T0605_初支钢支撑检查表
        /// 2019年1月7日
        /// </summary>
        [WordMark(MarkName = "T0605_初支钢支撑检查表", MarkType = MarkType.BuildData)]
        public List<IListModel> ChuzhisSteelTable { get; set; }

        /// <summary>
        /// T0701_二衬厚度结论
        /// </summary>
        [WordMark(MarkName = "T0701_二衬厚度结论")]

        public string ErchenThicknessConclusion { get; set; }

        /// <summary>
        /// T0702_二衬缺陷结论
        /// </summary>
        [WordMark(MarkName = "T0702_二衬缺陷结论")]

        public string ErchenDefectConclusion { get; set; }

        /// <summary>
        /// T0703_二衬间距结论
        /// </summary>
        [WordMark(MarkName = "T0703_二衬间距结论")]

        public string ErchenSpaceConclusion { get; set; }
    }
}
