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
        [WordMark(MarkName = "T0103_隧道施工情况及断面埋设")]

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
        
        
    }
}
