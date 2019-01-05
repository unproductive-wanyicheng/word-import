using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunnel.Word.Model;

namespace Tunnel.Word.BuildWord.Month
{
    public interface IBuildMonth:IBuild
    {
        /// <summary>
        /// 设置目录数据
        /// </summary>
        /// <param name="model"></param>
        void SetCoverModel(CoverModel model);

        /// <summary>
        /// 设置正文数据
        /// </summary>
        /// <param name="model"></param>
        void SetBodyModel(BodyModel model);

        /// <summary>
        /// 设置附件数据
        /// </summary>
        /// <param name="models"></param>
        void SetEnclosureModel(List<EnclosureModel> models);
    }
}
