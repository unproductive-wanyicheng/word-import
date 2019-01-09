using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunnel.Word.Enums;
using Tunnel.Word.Model;

namespace Tunnel.Word.BuildWord.Month
{
    public class ErchenFujianModel
    {
        /// <summary>
        /// 附件标题
        /// </summary>
        [WordMark(MarkName = "附件名字1")]
        public string FujianHeaderName { get; set; }
        /// <summary>
        /// 附件图
        /// </summary>
        [WordMark(MarkName = "Image1", MarkType = MarkType.Image)]
        public string ImageUrl { get; set; }

        public EnclosureType Type { get; set; }

        public List<ErchenImageModel> Images { get; set; }
    }
}
