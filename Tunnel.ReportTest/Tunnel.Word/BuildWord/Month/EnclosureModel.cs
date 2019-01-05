using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunnel.Word.Enums;
using Tunnel.Word.Model;

namespace Tunnel.Word.BuildWord.Month
{
    public class EnclosureModel
    {
        /// <summary>
        /// 附件标题
        /// </summary>
        [WordMark(MarkName = "附件名称")]
        public string HeaderName { get; set; }


        public EnclosureType Type { get; set; }

        /// <summary>
        /// 隧道名称
        /// </summary>
        [WordMark(MarkName = "隧道名称")]
        public string TunnelName { get; set; }

        /// <summary>
        /// 里程桩号
        /// </summary>
        [WordMark(MarkName = "里程桩号")]
        public string PileNumber { get; set; }

        /// <summary>
        /// 观测时间
        /// </summary>
        [WordMark(MarkName = "观测时间")]
        public string ObservationTime { get; set; }

        /// <summary>
        /// 观测人
        /// </summary>
        [WordMark(MarkName = "观测人")]
        public string Observant { get; set; }

        /// <summary>
        /// 地质素描图
        /// </summary>
        [WordMark(MarkName = "地质素描图",MarkType = MarkType.Image,Width = 195)]
        public string GeologicalSketchMap { get; set; }

        /// <summary>
        /// 掌子面照片
        /// </summary>
        [WordMark(MarkName = "掌子面照片", MarkType = MarkType.Image, Width = 195)]
        public string PhotoPalm { get; set; }

        /// <summary>
        /// 地质描述
        /// </summary>
        [WordMark(MarkName = "地质描述")]
        public string GeologicalDescription {get;set;}

        /// <summary>
        /// 支护状况描述
        /// </summary>
        [WordMark(MarkName = "支护状况描述")]
        public string DescriptionSupportCondition { get; set; }

        /// <summary>
        /// 边仰坡及地表裂缝描述
        /// </summary>
        [WordMark(MarkName = "边仰坡及地表裂缝描述")]
        public string SurfaceCrackDescription { get; set; }

        public List<ImageModel> Images { get; set; }
    }
}
