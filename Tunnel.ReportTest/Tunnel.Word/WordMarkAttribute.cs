using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tunnel.Word
{
    public class WordMarkAttribute : System.Attribute
    {
        /// <summary>
        /// WORD中 书签的名称 多个用逗号分开
        /// </summary>
        public string MarkName { get; set; }

        private MarkAlignment _alignment = MarkAlignment.Body;
        private MarkType _markType = MarkType.String;

        /// <summary>
        /// WORD中 书签的位置
        /// </summary>
        public MarkAlignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        public MarkType MarkType
        {
            get { return _markType; }
            set { _markType = value; }
        }

        public WordMarkAttribute()
        {
            Width = 0;
            Height = 0;
        }

        public int Width { get; set; }

        public int Height { get; set; }
    }

    /// <summary>
    /// 标签位置
    /// </summary>
    public enum MarkAlignment
    {
        Header=1,
        Body=2,
        Footer=3
    }

    public enum MarkType
    {
        String=1,
        BuildData=2,
        Image=3
    }
}
